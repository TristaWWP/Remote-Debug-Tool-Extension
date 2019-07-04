using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpLib
{
    public class Logger
    {
        private static readonly object mutexObj = new Object();
        public static Logger Instance { get; } = new Logger();
        private Logger()
        {

        }
        private void WriteLogs(string dirName, string type, string content)
        {
            string path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            if (!string.IsNullOrEmpty(dirName))
            {
                path = Path.Combine(path, dirName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var processId = Process.GetCurrentProcess().Id;
                path = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd-") + processId.ToString() + ".log");
                if (!File.Exists(path))
                {
                    using (var fileStream = File.Create(path)) ;
                }

                lock (mutexObj)
                {
                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                    {
                        string resContent = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " remotetest : " + type + " --> " + content;
                        sw.WriteLine(resContent);
                        Trace.WriteLine(resContent);
                    }
                }
            }
        }

        private void Log(string type, string content)
        {
            WriteLogs("RemoteDebugClient.logs", type, content);
        }
        public void Debug(string content)
        {
            Log(nameof(Debug), content);
        }
        public void Info(string content)
        {
            Log(nameof(Info), content);
        }

        public void Warn(string content)
        {
            Log(nameof(Warn), content);
        }

        public void Error(string content)
        {
            Log(nameof(Error), content);
        }
    }
}
