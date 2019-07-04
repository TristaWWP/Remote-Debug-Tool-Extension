using EnvDTE;
using System;
using System.IO;
using System.Linq;

namespace LocalClient
{
    public static class Helper
    {
        /// <summary>
        /// 扩展方法，获取当前项目的编译输出路径
        /// </summary>
        /// <param name="project">当前活动项目</param>
        /// <returns></returns>
        public static string GetFullOutputPath(this Project project)
        {
            if (Path.GetExtension(project.FullName).Equals(".csproj", StringComparison.OrdinalIgnoreCase))//Path.GetExtension获取扩展名，OrdinalIgnoreCase 使用序号排序规则并忽略被比较字符串的大小写，对字符串进行比较。
            {
                return Path.Combine(Path.GetDirectoryName(project.FullName), (string)project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value);
            }
            else
            {
                var outputUrlStr = ((object[])project.ConfigurationManager.ActiveConfiguration.OutputGroups.Item("Built").FileURLs).OfType<string>().First();
                var outputUrl = new Uri(outputUrlStr, UriKind.Absolute);
                return Path.GetDirectoryName(outputUrl.LocalPath);
            }
        }
    }
}
