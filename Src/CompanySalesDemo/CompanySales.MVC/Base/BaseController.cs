using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : Controller
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

        /// <summary>
        /// 设置cookie值，默认3天过期
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void AddCookie(string name, string value)
        {
            AddCookie(name, value, DateTime.Now.AddDays(3));
        }

        /// <summary>
        /// 设置cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expiresTime"></param>
        protected void AddCookie(string name, string value, DateTime? expiresTime)
        {
            if (Response.Cookies[name] != null)
            {
                Response.Cookies[name].Expires = expiresTime.GetValueOrDefault(DateTime.Now.AddDays(3));
                Response.Cookies[name].Value = value;
            }
            else
            {
                HttpCookie cookie = new HttpCookie(name, value);
                // 如没有传参则默认3天过期
                cookie.Expires = expiresTime.GetValueOrDefault(DateTime.Now.AddDays(3));

                Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected string GetCookie(string name)
        {
            return Request.Cookies[name].Value;
        }

        /// <summary>
        /// 移除cookie
        /// </summary>
        /// <param name="name"></param>
        protected void RemoveCookie(string name)
        {
            AddCookie(name, null, DateTime.Now.AddDays(-1));
        }
    }
}