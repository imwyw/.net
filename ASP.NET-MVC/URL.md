<!-- TOC -->

- [路由](#路由)
    - [默认路由](#默认路由)
    - [URL的写法](#url的写法)

<!-- /TOC -->
<a id="markdown-路由" name="路由"></a>
# 路由
<a id="markdown-默认路由" name="默认路由"></a>
## 默认路由
默认的路由在RegisterRoutes 方法中定义（/App_Start/RouteConfig.cs）
Web应用程序启动时调用Application_Start 方法，该方法会调用RegisterRoutes 方法
``` cs
public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        
        //添加默认路由  在这里是 /Home/Main
        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Main", id = UrlParameter.Optional }
        );
    }
```
<a id="markdown-url的写法" name="url的写法"></a>
## URL的写法
{controller}/{action}/{id}
``` cs
public class HomeController : Controller
{
	//对于的请求Url为 /Home/Index
	public ActionResult Index()
	{
		//默认寻找路径【/Views/Home/Index.cshtml】文件
		return View();
	}
}
```
项目结构：
![](../assets/asp.net-mvc/路由结构.png)
