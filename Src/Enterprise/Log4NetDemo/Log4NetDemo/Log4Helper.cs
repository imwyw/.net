using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    /// <summary>
    /// log4net实例获取
    /// 单例模式的应用
    /// </summary>
    public class Log4Helper
    {
        /// <summary>
        /// lock对象
        /// </summary>
        private static object lockLog = new object();

        /// <summary>
        ///  日志记录器
        /// </summary>
        private static log4net.ILog _log = null;

        /// <summary>
        /// 日志记录接口
        /// </summary>
        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    // 初始化配置，从当前项目下读取log4net.config配置文件
                    string configFile = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
                    log4net.Config.XmlConfigurator.Configure(new FileInfo(configFile));

                    lock (lockLog)
                    {
                        _log = LogManager.GetLogger("WebLogger");
                    }
                }
                return _log;
            }
        }
    }
}
