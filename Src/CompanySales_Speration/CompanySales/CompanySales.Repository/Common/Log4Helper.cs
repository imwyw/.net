using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace CompanySales.Repository.Common
{
    /// <summary>
    /// log4net 日志封装类
    /// </summary>
    public class Log4Helper
    {
        /// <summary>
        /// log4net 配置文件
        /// </summary>
        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        /// <summary>
        /// log对象，通过该对象记录日志信息
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Log4Helper));

        static Log4Helper()
        {
            // 加载 log4net 配置信息
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            // 应用 log4net.config 配置
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void Error(object error)
        {
            log.Error(error);
        }

        public static void Info(object info)
        {
            log.Info(info);
        }
    }
}
