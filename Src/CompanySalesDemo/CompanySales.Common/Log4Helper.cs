using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Common
{
    public class Log4Helper
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Log4Helper()
        {
            // 初始化配置，从当前项目下读取log4net.config配置文件
            string configFile = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            XmlConfigurator.Configure(new FileInfo(configFile));
        }

        public static ILog InfoLog
        {
            get
            {
                return LogManager.GetLogger("loginfo");
            }
        }

        public static ILog ErrorLog
        {
            get
            {
                return LogManager.GetLogger("logerror");
            }
        }
    }
}
