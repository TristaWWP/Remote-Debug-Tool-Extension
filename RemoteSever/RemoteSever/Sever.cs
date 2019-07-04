using HttpSeverLib;
using System.Configuration;
using System.Windows.Forms;

namespace RemoteSever
{
    class Sever
    {
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["IP"];
            string port = ConfigurationManager.AppSettings["Port"];
            OpenFireWall tempOpenFW = new OpenFireWall();
            string filePath = tempOpenFW.ProjectPath();
            string name = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
            OpenFireWall.NetFwAddApps(name, filePath);
            if (PortHelper.portInUse(int.Parse(port)))
            {
                MessageBox.Show("该端口被占用，请在配置文件中修改");
                Logger.Instance.Error("该端口被占用，请在配置文件中修改");
            }
            else
            {
                new HttpSever().HttpConnectClient("http://" + ip + ":" + port + "/");
            }
        }

    }
}
