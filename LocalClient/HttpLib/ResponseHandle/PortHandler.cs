using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpLib
{
    class PortHandler : IHandlerClient
    {
        string _tempResponse = null;
        public async Task<string> HandleAsync(string url)
        {
            Logger.Instance.Info("收到服务端发来的端口号和进程号，附加进程即将开始");
            return _tempResponse;
        }
        public bool CheckResponse(string response)
        {
            _tempResponse = response;
            if (IsNumber(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNumber(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
    }
}
