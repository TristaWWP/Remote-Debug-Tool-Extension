using System;
using System.Configuration;
using System.Net;

namespace HttpSeverLib
{
    class RemoteHandler : IHandler
    {
        public string Handle(HttpListenerRequest request)
        {
            string remoteZipPath = ConfigurationManager.AppSettings["SaveFilePath"] + "Remote.zip";
            string remoteFilePath = ConfigurationManager.AppSettings["SaveFilePath"] + "Remote";

            FileHelper.ReciveFile(request.InputStream, remoteZipPath, remoteFilePath);
            Console.WriteLine("接收Remote文件完成");
            Logger.Instance.Info("接收Remote文件完成");
            string responseString = ConstantContext.ReceiveRemoteFile;// 收到Remote文件，回复ReceiveRemote指令--RR，客户端收到RR后发送Start指令   
            Console.WriteLine("已收到Remote文件，发送RR指令");
            Logger.Instance.Info("已收到Remote文件，发送RR指令");
            return responseString;
        }
        public bool TrueUrl(string url)
        {
            if (url == @"/Remote/")
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