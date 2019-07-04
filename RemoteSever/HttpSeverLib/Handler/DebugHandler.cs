using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace HttpSeverLib
{
    class DebugHandler : IHandler
    {
        public static string ActiveName { get; set; }
        public string Handle(HttpListenerRequest request)
        {
            string debugZipPath = ConfigurationManager.AppSettings["SaveFilePath"] + "Debug.zip";
            string debugFilePath = ConfigurationManager.AppSettings["SaveFilePath"] + "Debug";
            string remoteFilePath = ConfigurationManager.AppSettings["SaveFilePath"] + "Remote";
            string responseString = null;

            if (Directory.Exists(debugFilePath))//存在文件路径就删除，以便更新
            {
                Directory.Delete(debugFilePath, true);
            }
            if (Directory.Exists(remoteFilePath))//存在文件路径就删除，以便更新
            {
                Directory.Delete(remoteFilePath, true);
            }
            FileHelper.ReciveFile(request.InputStream, debugZipPath, debugFilePath);
            
            Console.WriteLine("接收debug文件完成");
            Logger.Instance.Info("接收debug文件完成");

            //if (Directory.Exists(remoteFilePath))
            //{
            //    responseString = ConstantContext.NoRemoteFile;
            //    Console.WriteLine("已收到debug文件，本机中有Remote工具，不需要发送该文件，直接接收Start指令");
            //    Logger.Instance.Info("已收到debug文件，本机中有Remote工具，不需要发送该文件，直接接收Start指令");
            //}
            //else
            //{
                responseString = ConstantContext.ReceiveDebugFile;// 收到Debug文件，回复ReceiveDebug指令，客户端收到RD后发送Remote文件  
                Console.WriteLine("已收到debug文件，发送RD指令");
                Logger.Instance.Info("已收到debug文件，发送RD指令");
            //}
            return responseString;
        }

        public bool TrueUrl(string url)
        {
            if ((url.Substring(0, 7)) == @"/Debug/")
            {
                ActiveName = url.Substring(url.LastIndexOf(@"/") + 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}