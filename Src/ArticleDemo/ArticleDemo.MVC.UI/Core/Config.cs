using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleDemo.MVC.UI.Core
{
    public class Config
    {
        // newtonsoft.json 序列化 时间格式
        public static readonly IsoDateTimeConverter FULL_DATE_FORMAT = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        public static readonly IsoDateTimeConverter DATE_FORMAT = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" };
    }
}