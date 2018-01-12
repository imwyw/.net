using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmptyDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //默认路由的设置
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "EmptyDemo.Controllers" }
            );
        }
    }
}
