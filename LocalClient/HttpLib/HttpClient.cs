using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.IO.Compression;

namespace HttpLib
{
    public class HttpClientLib
    {
        private HttpClient _client;
        public HttpClientLib()
        {
            _client = new HttpClient();
        }
        public async Task<string> HttpConnectServer(string url, HttpContent dataContent)
        {
            HttpResponseMessage response = await _client.PostAsync(url, dataContent);
            response.EnsureSuccessStatusCode();
            string respnseResult = await response.Content.ReadAsStringAsync();
            return respnseResult;
        }
        public async Task<string> HttpConnectServerGet(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string respnseResult = await response.Content.ReadAsStringAsync();
            return respnseResult;
        }
    }
    public class HttpClientExtend : HttpClientLib
    {
        public HttpClientExtend()
        {
        }
        public HttpContent SendFile(string sourceFlie, string zipfileName)
        {
            //如果存在压缩文件就删除          
            if (File.Exists(zipfileName))
            {
                File.Delete(zipfileName);
            }
                        
            ZipFile.CreateFromDirectory(sourceFlie, zipfileName);            
            FileStream zipstream = File.OpenRead(zipfileName);
            var mystream = new StreamContent(zipstream);
            Logger.Instance.Info("压缩文件成功，并包装成Content，准备发送");
            return mystream;
        }
    }
}
