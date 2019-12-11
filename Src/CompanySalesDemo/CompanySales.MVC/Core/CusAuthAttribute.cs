using CompanySales.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Core
{
    public class CusAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 判断是否具备访问的权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //bool isLogin = null != HttpContext.Current.Session["uname"];
            bool isLogin = null != ContextObject.CurrentUser;
            return isLogin;
        }

        /// <summary>
        /// 当没有访问权限时，应该怎么处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Security/Login");
        }
    }
}