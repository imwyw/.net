using ArticleDemo.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArticleDemo.MVC.UI.Core
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 角色名称,具有访问该Action权限的角色
        /// 多个角色使用英文逗号（,）分隔
        /// </summary>
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //登录页面不做权限判断
            RouteValueDictionary routeDic = filterContext.RouteData.Values;

            if (routeDic["controller"].ToString().ToLower().Equals("login")
                && routeDic["action"].ToString().ToLower().Equals("signinview"))
            {
                return;
            }

            //当前未登录，重定向到登录页
            if (ContextObjects.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult("/Login/SignInView");
                return;
            }

            //是否具有访问权限，默认为false
            bool isAuthorize = false;

            //已登录用户，判断权限
            //ContextObjects.CurrentUser.Roles 当登录时从数据库读取
            //若当前用户无角色配置，则无访问权限
            if (!string.IsNullOrEmpty(ContextObjects.CurrentUser.Roles))
            {
                //特性中配置的角色，具有访问该action权限的角色
                string[] requireRoles = Roles.Split(',');

                //当前用户所具有的角色
                string[] userRoles = ContextObjects.CurrentUser.Roles.Split(',');

                //判断是否有匹配角色
                foreach (string rRole in requireRoles)
                {
                    if (userRoles.Contains(rRole)) { isAuthorize = true; break; }
                }
            }

            if (!isAuthorize)
            {
                ContentResult content = new ContentResult();
                content.Content = "对不起，您无访问权限！";
                //content.Content = @"<script type='text/javascript'>alert('对不起，您无访问权限！');</script>";
                filterContext.Result = content;
            }
        }
    }
}