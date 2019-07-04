using System.Threading.Tasks;

namespace HttpLib
{
    class ReciveRemoteHandler : IHandlerClient
    {
        public async Task<string> HandleAsync(string url)
        {
            HttpClientLib clientLib = new HttpClientLib();
            var startUrl = url + "/Start/";
            string response = await clientLib.HttpConnectServerGet(startUrl);
            Logger.Instance.Info("成功发送Start指令,开始准备附加到进程");
            return response;
        }
        public bool CheckResponse(string response)
        {
            if (response == HttpSeverLib.ConstantContext.ReceiveRemoteFile 
                || response == HttpSeverLib.ConstantContext.NoRemoteFile)//服务端已收到或已存在Remote文件，开始发送Start指令
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
