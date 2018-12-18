using CoSales.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Core
{
    /// <summary>
    /// 自定义特性
    /// 派生自AuthorizeAttribute（MVC默认的权限验证特性）
    /// </summary>
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 由OnAuthorization调用，是否允许该用户通过验证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // 通过session值来判断当前是否已经通过验证，即处于登录的状态
            //bool isLogin = HttpContext.Current.Session["CurrentUser"] != null;
            bool isLogin = ContextObjects.CurrentUser != null;

            return isLogin;
        }

        /// <summary>
        /// 处理验证未通过的重定向
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // 将页面重定向到 登录的Action
            filterContext.Result = new RedirectResult("/Sign/SignInView");
        }
    }
}