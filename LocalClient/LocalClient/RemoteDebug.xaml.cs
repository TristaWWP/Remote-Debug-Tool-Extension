using System.Windows;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using EnvDTE;
using EnvDTE80;
using HttpLib;
using Microsoft.VisualStudio.Threading;

namespace LocalClient
{
    /// <summary>
    /// RemoteDebug.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteDebug : System.Windows.Window
    {
        private HttpResponse _httpClient;
        public DTE2 Dte { get; }
        public BuildEvents BuildEvents { get; }
        public AsyncPackage Package { get; }
        public static string PidString { get; set; }
        public RemoteDebug(AsyncPackage package, DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            InitializeComponent();
            Package = package;
            _httpClient = new HttpResponse();
            Dte = dte;
            //Dte = package.GetServiceAsync(typeof(DTE)).Result as DTE2;
            BuildEvents = dte.Events.BuildEvents;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           
            ButtonStart.IsEnabled = false;
            _httpClient.IP = InputIPText.Text;
            _httpClient.Port = InputPort.Text;

            BuildEvents.OnBuildDone += BuildEvents_OnBuildDone;
            var project = await BuildStartAsync();
            Logger.Instance.Info("当前解决方案编译完成");
            if (project == null)
            {
                MessageBox.Show("编译失败，请检查代码");
                Logger.Instance.Info("当前解决方案编译失败");
            }
            else
            {
                var buildPathName =  project.GetFullOutputPath();
                var name = project.Name;
                BuildFilePath.Text = buildPathName;
                active.Text = name.ToString() + ".exe";
                _httpClient.SourceFileName = BuildFilePath.Text;
                _httpClient.Active = active.Text;
                Logger.Instance.Info("当前解决方案编译成功，并输出编译路径和当前活动项目名称");
            }
        }
        private void BuildEvents_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            ButtonStart.IsEnabled = true;
            BuildEvents.OnBuildDone -= BuildEvents_OnBuildDone;//取消订阅
            System.Threading.Tasks.Task.Run(async () =>
            {
                var resultsrting = await _httpClient.ClientTransFile();
                int portLen = int.Parse(resultsrting.Substring(0, 1));
                string portString = resultsrting.Substring(1, portLen);
                PidString = resultsrting.Substring(portLen + 1, resultsrting.Length - portLen - 1);
                AttachPro.AttachProcess(Dte, _httpClient.IP, portString);
                Logger.Instance.Info("附加进程已开始");
            });
        }
        public async System.Threading.Tasks.Task<Project> BuildStartAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var startBuid =  (IVsSolutionBuildManager2)await Package.GetServiceAsync(typeof(SVsSolutionBuildManager));
            IVsHierarchy startupProject;
            startBuid.get_StartupProject(out startupProject);
            if (startupProject == null)
            {
                MessageBox.Show("未检测到启动项，请确保是否已打开解决方案");
                return null;
            }
            else
            {
                startBuid.StartUpdateProjectConfigurations(1, new[] { startupProject }, (uint)VSSOLNBUILDUPDATEFLAGS.SBF_OPERATION_BUILD, 0);//编译
                startupProject.GetProperty((uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_ExtObject, out var obj);
                var project = (obj as Project);
                return project;
            }
        }
    }
}
