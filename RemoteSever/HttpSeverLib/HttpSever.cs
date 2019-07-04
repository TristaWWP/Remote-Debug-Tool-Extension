using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpSeverLib
{
    public class HttpSever
    {
        public void HttpConnectClient(string prefixes)
        {
            string responseString = null;
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003以上的系统才支持HttpListener类");
                Logger.Instance.Warn("Windows XP SP2 or Server 2003以上的系统才支持HttpListener类");
                return;
            }
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefixes);
            listener.Start();
            Console.WriteLine("服务器已经启动，开始监听");
            Logger.Instance.Info("服务器已经启动，开始监听");

            IHandler[] handlers = new IHandler[]
            {
                new DebugHandler(),
                new RemoteHandler(),
                new StartEXEHandler(),
            };
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                Console.WriteLine("收到传入请求，开始处理");
                Logger.Instance.Info("收到传入请求，开始处理");
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                var temp = context.Request.RawUrl;
                foreach (var tempHandle in handlers)
                {
                    if (tempHandle.TrueUrl(temp))
                    {
                        responseString = tempHandle.Handle(request);
                    }
                }
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
