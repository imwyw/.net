using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC_LogDemo.Core
{
    public class Log4Helper
    {
        static Log4Helper()
        {
            // 初始化配置，从当前项目下读取log4net.config配置文件
            string configFile = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configFile));
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