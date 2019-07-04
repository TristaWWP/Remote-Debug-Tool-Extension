using System.IO;

namespace HttpSeverLib
{
    class FileHelper
    {
        /// <summary>
        /// 接收压缩文件,存到指定路径
        /// </summary>
        /// <param name="sourceStream">http传来的正文数据流</param>
        /// <param name="sourceFilePath">接收到压缩文件的路径</param>
        /// <param name="targetFilePath">解压后的文件路径</param>
        public static void ReciveFile(Stream sourceStream, string sourceFilePath, string targetFilePath)
        {
            FileStream fs = new FileStream(sourceFilePath, FileMode.Create);
            sourceStream.CopyTo(fs);
            sourceStream.Close();
            fs.Close();
            System.IO.Compression.ZipFile.ExtractToDirectory(sourceFilePath, targetFilePath);
            File.Delete(sourceFilePath);
        }
    }
}