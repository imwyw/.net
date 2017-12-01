using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 日志操作类
    /// 单例模式应用
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 不允许外部进行实例化
        /// </summary>
        private LogHelper() { }

        static readonly object locker = new object();

        static LogHelper uniqueInstance;

        public static LogHelper GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new LogHelper();
                    }
                }
            }
            return uniqueInstance;
        }

        /// <summary>
        /// 写日志功能
        /// 每天一个文件如：log_2017-10-10.log
        /// </summary>
        /// <param name="msg">日志摘要</param>
        /// <param name="content">日志详情</param>
        public void Log(string msg, string content)
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

            lock (locker)
            {
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
}
