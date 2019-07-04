using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace HttpLib
{
    public class HttpResponse
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string SourceFileName { get; set; }
        public string Active { get; set; }
        public HttpResponse()
        {
        }
        string _response = null;
        public async Task<string> ClientTransFile()
        {
            HttpClientLib clientLib = new HttpClientLib();
            HttpClientExtend extendClientLib = new HttpClientExtend();
            try
            {
                var url = $"http://{IP}:{Port}";
                var debugUrl = url + "/Debug/" + Active;
               
                //if (File.Exists(HttpSeverLib.ConstantContext.zipDebugFile))
                //{
                //    File.Delete(HttpSeverLib.ConstantContext.zipDebugFile);
                //}

                var content = extendClientLib.SendFile(SourceFileName, HttpSeverLib.ConstantContext.zipDebugFile);
                Logger.Instance.Info("开始请求服务端连接");
                _response = await clientLib.HttpConnectServer(debugUrl, content);
                Logger.Instance.Info("成功发送Debug文件，并准备开始第二次发送");

                IHandlerClient[] handlerClients = new HttpLib.IHandlerClient[]
               {
                   new ReciveDebugHandler(),
                   new ReciveRemoteHandler(),
                   new PortHandler(),
               };
                foreach (var tempHandle in handlerClients)
                {
                    if (tempHandle.CheckResponse(_response))
                    {
                        _response = await tempHandle.HandleAsync(url);
                    }
                }
                return _response;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "";
            }
        }
    }
}
