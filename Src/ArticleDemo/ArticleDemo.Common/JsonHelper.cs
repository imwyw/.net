using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// C#对象 转换成 JSON字符串
        /// </summary>
        /// <param name="item">C#对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson(object item)
        {
            if (null == item)
            {
                return null;
            }

            // 日期格式化字符串
            string strFormat = "yyyy-MM-dd HH:mm:ss";
            DataContractJsonSerializerSettings setting = new DataContractJsonSerializerSettings();
            setting.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat(strFormat);

            // 序列化对象，增加日期格式化的配置
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType(), setting);

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                string str = Encoding.UTF8.GetString(ms.ToArray());
                // 将其中的 "" 转化成“-”
                //str = str.Replace("\"\"", "\"-\"");

                // 替换Json的Date字符串 该方案仅适用于 1970-1-1后的日期
                //string p = @"\\/Date\((\d+)\+\d+\)\\/";
                //MatchEvaluator matchEvaluator = new MatchEvaluator(delegate (Match m)
                //{
                //    return new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(m.Groups[1].Value))
                //        .ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                //});
                //Regex reg = new Regex(p);
                //str = reg.Replace(str, matchEvaluator);

                return str;
            }
        }
    }
}
