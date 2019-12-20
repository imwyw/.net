using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompanySales.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region 应用程序级别
        /// <summary>
        /// 不是每次请求都调用
        /// 在Web应用程序的生命周期里就执行一次
        /// 网站启动的时候会被调用，适合处理应用程序范围的初始化代码
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 在应用程序关闭时运行的代码，在最后一个HttpApplication销毁之后执行
        /// 比如IIS重启，文件更新，进程回收导致应用程序转换到另一个应用程序域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 会话开始时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_Start(object sender, EventArgs e)
        {
            // SessionID 重新写入session中，防止mvc中同一会话sessionid发生变化的问题
            string key = "SessionID";
            Session[key] = Session.SessionID;
            Response.Cookies[key].Value = Session.SessionID;
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 会话结束或过期时执行
        /// 不管在代码中显式的清空Session或者Session超时自动过期，此方法都将被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 在每一个HttpApplication实例初始化的时候执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Init(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 在应用程序被关闭一段时间之后，在.net垃圾回收器准备回收它占用的内存的时候被调用。
        /// 在每一个HttpApplication实例被销毁之前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Disposed(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 不是每次请求都调用
        /// 所有没有处理的错误都会导致这个方法的执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                //异常信息
                string strExceptionMessage = string.Empty;

                //对HTTP 404做额外处理，其他错误全部当成500服务器错误
                HttpException httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //获取错误代码
                    int httpCode = httpError.GetHttpCode();
                    strExceptionMessage = httpError.Message;
                    if (httpCode == 400 || httpCode == 404)
                    {
                        Response.StatusCode = 404;
                        //跳转到指定的静态404信息页面，根据需求自己更改URL
                        //Response.WriteFile("~/Views/HttpError/404.html");
                        //Server.ClearError();
                        //记录日志信息
                        Common.Log4Helper.ErrorLog.Error(lastError);
                        return;
                    }
                }

                strExceptionMessage = lastError.Message;

                /*-----------------------------------------------------
                 * 此处代码可根据需求进行日志记录，或者处理其他业务流程
                 * ---------------------------------------------------*/
                Common.Log4Helper.ErrorLog.Error(lastError);

                /*
                 * 跳转到指定的http 500错误信息页面
                 * 跳转到静态页面一定要用Response.WriteFile方法                 
                 */
                Response.StatusCode = 500;
                //Response.WriteFile("~/Views/HttpError/500.html");

                //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                //Server.ClearError();
            }
        }
        #endregion

        #region 每次请求都会按照顺序执行以下事件
        /// <summary>
        /// 每次请求时第一个出发的事件，这个方法第一个执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 在执行验证前发生，这是创建验证逻辑的起点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 当安全模块已经验证了当前用户的授权时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_AuthorizeRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 当ASP.NET完成授权事件以使缓存模块从缓存中为请求提供服务时发生，从而跳过处理程序（页面或者是WebService）的执行。
        /// 这样做可以改善网站的性能，这个事件还可以用来判断正文是不是从Cache中得到的。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_ResolveRequestCache(object sender, EventArgs e)
        {
        }

        //------------------------------------------------------------------------
        //在这个时候，请求将被转交给合适程序。例如：web窗体将被编译并完成实例化
        //------------------------------------------------------------------------

        /// <summary>
        /// 读取了Session所需的特定信息并且在把这些信息填充到Session之前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 在合适的处理程序执行请求前调用
        /// 这个时候，Session就可以用了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        //-------------------------------------------------
        //在这个时候，页面代码将会被执行，页面呈现为HTML
        //-------------------------------------------------

        /// <summary>
        /// 当处理程序完成对请求的处理后被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 释放请求状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_ReleaseRequestState(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 为了后续的请求，更新响应缓存时被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_UpdateRequestCache(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// EndRequest是在响应Request时最后一个触发的事件
        /// 但在对象被释放或者从新建立以前，适合在这个时候清理代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_EndRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 向客户端发送Http标头之前被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 向客户端发送Http正文之前被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_PreSendRequestContent(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
