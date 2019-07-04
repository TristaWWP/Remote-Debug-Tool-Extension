using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace HttpLib
{
    class ReciveDebugHandler : IHandlerClient
    {
        public async Task<string> HandleAsync(string url)
        {
            HttpClientLib clientLib = new HttpClientLib();
            HttpClientExtend extendClientLib = new HttpClientExtend();
            var remoteUrl = url + @"/Remote/";
            var test = ((EnvDTE.DTE)ServiceProvider.GlobalProvider.GetService(typeof(EnvDTE.DTE).GUID)).FullName;
            var vsInstallationPath = Path.GetFullPath(Path.Combine(test, @"..\"));
            var remotefilepath = vsInstallationPath + @"Remote Debugger\";
            var contentR = extendClientLib.SendFile(remotefilepath, HttpSeverLib.ConstantContext.zipRemoteFile);
            string response = await clientLib.HttpConnectServer(remoteUrl, contentR);
            Logger.Instance.Info("成功发送Remote文件，并准备开始第三次发送");
            return response;

        }
        public bool CheckResponse(string response)
        {
            return response == HttpSeverLib.ConstantContext.ReceiveDebugFile;
            //服务端已收到Debug文件，继续发送Remote文件
            //if (response == HttpSeverLib.ConstantContext.ReceiveDebugFile)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
