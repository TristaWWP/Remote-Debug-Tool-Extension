using System.Threading.Tasks;

namespace HttpLib
{
    /// <summary>
    /// 处理服务端发来的不同响应
    /// </summary>
    interface IHandlerClient
    {
        bool CheckResponse(string response);
        Task<string> HandleAsync(string url);
    }
}
