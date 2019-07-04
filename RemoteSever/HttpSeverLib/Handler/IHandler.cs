using System.Net;

namespace HttpSeverLib
{
    /// <summary>
    /// 响应客户端传来的请求接口
    /// </summary>
    interface IHandler
    {
        bool TrueUrl(string url);
        string Handle(HttpListenerRequest request);
    }
}