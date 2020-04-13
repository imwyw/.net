using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompanySales.Repository.Common
{
    /// <summary>
    /// 类库项目中读取 appsettings.json 文件
    /// </summary>
    public class ConfigHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            builder.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }

}
