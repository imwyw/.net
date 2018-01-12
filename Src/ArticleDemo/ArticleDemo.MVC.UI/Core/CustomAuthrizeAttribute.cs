using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleDemo.MVC.UI.Core
{
    /// <summary>
    /// 实现一个自定义的 权限验证特性类
    /// </summary>
    public class CustomAuthrizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 是否允许该用户通过验证，返回布尔值，OnAuthorization会调用到该方法
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Debug.WriteLine("###############-AuthorizeCore");
            //return base.AuthorizeCore(httpContext);

            //重写是否具有访问权限
            bool isLogin = HttpContext.Current.Session["CurUser"] != null;
            return isLogin;
        }

        /// <summary>
        /// 身份验证未通过时执行，即AuthorizeCore方法返回false时调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Debug.WriteLine("###############-HandleUnauthorizedRequest start");
            //base.HandleUnauthorizedRequest(filterContext);
            //Debug.WriteLine("###############-HandleUnauthorizedRequest end");

            //重写 在无访问权限时的操作，重定向到登录页面
            filterContext.Result = new RedirectResult("/Login/SignInView");
        }
    }
}