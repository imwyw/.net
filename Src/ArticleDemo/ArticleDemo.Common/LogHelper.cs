using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Common
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 不允许外部进行实例化
        /// </summary>
        private LogHelper() { }

        /// <summary>
        /// 写日志功能
        /// 每天一个文件如：log_2017-10-10.log
        /// </summary>
        /// <param name="msg">日志摘要</param>
        /// <param name="content">日志详情</param>
        public static void Log(string msg, string content)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fileName = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            filePath = Path.Combine(filePath, fileName);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("[{0}]:{1}\t{2}", DateTime.Now.ToString(), msg, content);
                }
            }
        }
    }
}
