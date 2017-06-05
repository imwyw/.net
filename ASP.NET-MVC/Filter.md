# 筛选器的执行
ASP.NET-MVC的筛选器是一种基于AOP(面向方面编程)的设计，我们将一些非业务的逻辑实现在相应的筛选器，并以一种横切( Crosscutting)的方式应用到对应的 Action 方法上。在Action方法执行前后，这些筛选器会自动执行。 ASP.NETMVC 提供了 AuthorizationFilter、ActionFilter、ResultFilter和ExceptionFilter这四种筛选器，它们对应着四个接口IAuthorizationFilter、IActionFilter、IResultFilter 和 IExceptionFilter。经常应用在用户权限验证、系统日志、异常处理、缓存等功能上。

| Filter类型 | 接口 | MVC默认实现 | Description |
|---|---|---|---|
|Authorization|IAuthorizationFilter|AuthorizeAttribute|最先执行，在其他类型的filter和action方法前执行|
|Action|IActionFilter|ActionFilterAttribute|在action方法执行前和执行后执行|
|Result|IResultFilter|ActionFilterAttribute|在result执行前和执行后执行|
|Exception|IExceptionFilter|HandleErrorAttribute|在抛出异常时执行，（异常发生在action/result/filter）|


## 先后顺序
IAuthorizationFilter -> IActionFilter - >IResultFilter ->IExceptionFilter

# Authorization
是所有Filter类型第一个执行的Filter，在Action调用前执行，需要实现IAuthorizationFilter接口。

用于完成授权相关的工作，如果希望在调用Action前做点啥也可以通过自定义Authorize的方式实现。

## 接口含义
``` cs
//请求Action前调用
public virtual void OnAuthorization(AuthorizationContext filterContext);

//是否允许该用户通过验证，返回布尔值，OnAuthorization会调用到该方法
protected virtual bool AuthorizeCore(HttpContextBase httpContext);

//身份验证未通过时执行，即AuthorizeCore方法返回false时调用
protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext);

/***************************************************************************/
```
## 简单验证是否登录
``` cs
//自定义Authorize特性
public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    /// <summary>
    /// 请求action前的验证
    /// </summary>
    /// <param name="filterContext"></param>
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        base.OnAuthorization(filterContext);

        //var statusResult = filterContext.Result as HttpStatusCodeResult;

        //通过AuthorizeCore验证返回判断
        //if (statusResult != null && statusResult.StatusCode == 401)
        //{
        //    //重定向到登录页
        //    filterContext.Result = new RedirectResult("/Login/Login");
        //}

        //通过Session判断
        //if (ContextObjects.CurrentUser == null)
        //{
        //    //重定向到登录页
        //    filterContext.Result = new RedirectResult("/Login/Login");
        //}
    }

    /// <summary>
    /// 当验证没有通过的时候
    /// 或者重写OnAuthorization方法实现
    /// </summary>
    /// <param name="filterContext"></param>
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        base.HandleUnauthorizedRequest(filterContext);
        //重定向到登录页
        filterContext.Result = new RedirectResult("/Login/Login");
    }

    /// <summary>
    /// 由OnAuthorization调用，是否允许该用户通过验证
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        if (HttpContext.Current.Session["xxx-user"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

//调用方法：
//[CustomAuthorize] 在Controller上同样
public class UserMgrController : Controller
{
    [CustomAuthorize]
    [CustomActionFilter(Roles = "superadmin")]
    public ActionResult UserInfo()
    {
        return View();
    }
}
```

# ActionFilter
## 接口含义
``` cs
//执行Action后调用
public virtual void OnActionExecuted(ActionExecutedContext filterContext);

//执行Action前调用，但是在Authorization OnAuthorization方法后调用
public virtual void OnActionExecuting(ActionExecutingContext filterContext);

//在执行操作结果后
public virtual void OnResultExecuted(ResultExecutedContext filterContext);

//在返回执行操作结果前
public virtual void OnResultExecuting(ResultExecutingContext filterContext);
```
## 简单权限
``` cs
public class CustomActionFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// 角色名称
    /// 多个角色使用英文逗号（,）分隔
    /// </summary>
    public string Roles { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);

        //登录页面不做权限判断
        RouteValueDictionary routeDic = filterContext.RouteData.Values;

        if (routeDic["controller"].ToString().ToLower().Equals("Login")
            && routeDic["action"].ToString().ToLower().Equals("Login"))
        {
            return;
        }

        //当前未登录，重定向到登录页
        if (ContextObjects.CurrentUser == null)
        {
            filterContext.Result = new RedirectResult("/Login/Login");
            return;
        }


        //已登录用户，判断权限
        //ContextObjects.CurrentUser.Roles 当登录时一并从数据库读取
        if (!string.IsNullOrEmpty(ContextObjects.CurrentUser.Roles))
        {
            string[] requireRoles = this.Roles.Split(',');
            string[] userRoles = ContextObjects.CurrentUser.Roles.Split(',');

            bool isAuthorize = false;
            foreach (string rRole in requireRoles)
            {
                if (userRoles.Contains(rRole)) { isAuthorize = true; break; }
            }
            if (!isAuthorize)
            {
                ContentResult Content = new ContentResult();
                Content.Content = @"<script type='text/javascript'>alert('权限验证未通过！');
                history.go(-1);
                </script>";
                filterContext.Result = Content;
            }
        }
    }
}

//调用方法：
//[CustomAuthorize] 在Controller上同样
public class UserMgrController : Controller
{
    //只允许 权限为 superadmin 的用户访问该Action
    [CustomAuthorize]
    [CustomActionFilter(Roles = "superadmin")]
    public ActionResult UserInfo()
    {
        return View();
    }
}
```