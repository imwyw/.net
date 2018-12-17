using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Common
{
    /// <summary>
    /// 日志帮助类
    /// 单例
    /// </summary>
    public class LogHelper
    {
        private LogHelper() { }

        static LogHelper logger;
        static readonly object lockObj = new object();

        //public LogHelper GetInstance()
        public static LogHelper Logger
        {
            get
            {
                if (logger == null)
                {
                    lock (lockObj)
                    {
                        if (logger == null)
                        {
                            logger = new LogHelper();
                        }
                    }
                }
                return logger;
            }
        }

        public void WriteLog(string title, string content)
        {
            // 日志名称，每天一个记录log文件
            string logName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logName);

            if (!File.Exists(logFile))
            {
                File.Create(logFile).Dispose();
            }
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}----{title}----{content}");
            }
        }
    }
}
