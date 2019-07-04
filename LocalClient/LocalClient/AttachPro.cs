using EnvDTE;
using EnvDTE80;
using HttpLib;
using System.Linq;
using System.Windows;

namespace LocalClient
{
    class AttachPro
    {
        public static Transport DteTransport { get; private set; }
        /// <summary>
        /// 连接远程计算机，并附加到进程
        /// </summary>
        /// <param name="ip">远程计算机ip</param>
        /// <param name="remotePort">远程调试工具的端口</param>
        /// <returns></returns>
        public static bool AttachProcess(DTE2 dte, string ip, string remotePort)
        {
            Debugger2 debug = (Debugger2)dte.Debugger;
            DteTransport = debug.Transports.Item(2);
            Processes processes = debug.GetProcesses(DteTransport, ip + ":" + remotePort);
            Logger.Instance.Info("成功连接到远程计算机，并获取远程目标机上所有进程");
            bool found = false;
            foreach (Process2 process in processes)
            {
                if (process.ProcessID == int.Parse(RemoteDebug.PidString))
                {
                    foreach (Engine engine in DteTransport.Engines)
                    {
                        //{FB0D4648-F776-4980-95F8-BB7F36EBC1EE}这是托管4.5模式下的调试器
                        if (new[] { "{FB0D4648-F776-4980-95F8-BB7F36EBC1EE}",
                    }.Any(temp => string.Equals(engine.ID, temp)))
                        {
                            process.Attach2(new[] { engine });
                        }
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("Selected processes are not running. Try to run your application first.", "Debug Attach History Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}

