using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace ArticleDemo.MVC.UI.Core
{
    public class JsonConvertResult : JsonResult
    {
        // newtonsoft.json 序列化 时间格式
        readonly IsoDateTimeConverter FULL_DATE_FORMAT = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        readonly IsoDateTimeConverter DATE_FORMAT = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" };

        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("此请求已被阻止，因为当用在 GET 请求中时，会将敏感信息透漏给第三方网站。若要允许 GET 请求，请将 JsonRequestBehavior 设置为 AllowGet。");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            // If you need special handling, you can call another form of SerializeObject below
            var serializedObject = JsonConvert.SerializeObject(Data, FULL_DATE_FORMAT);
            response.Write(serializedObject);
        }
    }

    /// <summary>
    /// 为了解决以下问题：
    /// newtonsoft序列化后多了字符串引号的问题
    /// 时间默认使用json返回时格式不正确
    /// </summary>
    public class BaseController : Controller
    {
        protected new JsonResult Json(object data)
        {
            return Json(data, JsonRequestBehavior.DenyGet);
        }

        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            if (data == null)
                return base.Json(null);

            if (!Request.AcceptTypes.Contains("application/json"))
                return new JsonConvertResult { Data = data, ContentType = "text/plain", JsonRequestBehavior = behavior };
            else
                return new JsonConvertResult { Data = data, JsonRequestBehavior = behavior };
        }
    }
}