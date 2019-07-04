using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;

namespace HttpSeverLib
{
    class StartEXEHandler : IHandler
    {
        public string Handle(HttpListenerRequest request)
        {
            string arguments = ConfigurationManager.AppSettings["Arguments"];
            string debugFilePath = ConfigurationManager.AppSettings["SaveFilePath"] + "Debug";
            string remoteFilePath = ConfigurationManager.AppSettings["SaveFilePath"] + "Remote";
            string remoteName = ConfigurationManager.AppSettings["RemoteName"];
            string tempPort = System.Text.RegularExpressions.Regex.Replace(arguments, @"[^0-9]+", "");
            Console.WriteLine("已收到Start指令，发送远程调试工具的端口号");
            Logger.Instance.Info("已收到Start指令，发送远程调试工具的端口号");

            Process.Start(remoteFilePath + @"\" + remoteName, arguments);
            var process = Process.Start(debugFilePath + @"\" + DebugHandler.ActiveName);            
            var processID = process.Id.ToString();
            string responseString = tempPort.Length + tempPort + processID;
            Console.WriteLine("启动远程调试工具和调试程序完成");
            Logger.Instance.Info("启动远程调试工具和调试程序完成");

            return responseString;
        }
        public bool TrueUrl(string url)
        {
            if (url == @"/Start/")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
