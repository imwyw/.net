using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Common
{
    /// <summary>
    /// 简单的日志记录类
    /// 将异常、调试等信息记录到文本文件
    /// </summary>
    public class MyLogHelper
    {
        public static void Error(string content)
        {
            Write("发生异常", content);
        }

        public static void Info(string content)
        {
            Write("消息", content);
        }

        public static void Error(Exception ex)
        {
            Write("发生异常", ex.Message + ex.StackTrace);
        }

        public static void Write(string title, string content)
        {
            string directoryName = "Logs";
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryName);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string fullName = Path.Combine(basePath, fileName);
            if (!File.Exists(fullName))
            {
                File.Create(fullName).Dispose();
            }

            using (StreamWriter sw = new StreamWriter(fullName, true))
            {
                sw.WriteLine($"【{DateTime.Now.ToString()}】，【标题：{title}】，详情：{content}");
            }
        }
    }
}
