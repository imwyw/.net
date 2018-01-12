using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo.Core
{
    public class AutofacHelper
    {
        /// <summary>
        /// 缓存注册容器 
        /// </summary>
        private static IContainer Container = null;

        /// <summary>
        /// 注册并创建实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            if (Container == null)
            {
                // 构建一个配置模块
                IConfigurationBuilder config = new ConfigurationBuilder();
                config.AddXmlFile("Autofac.config");
                var module = new ConfigurationModule(config.Build());

                // 注册模块
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);
                Container = builder.Build();
            }

            return Container.Resolve<T>();
        }
    }
}
