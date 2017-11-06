using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace LoginDemo
{
    /// <summary>
    /// HomeHandler 的摘要说明
    /// 需要使用session，一般处理程序需要实现接口IRequiresSessionState/IReadOnlySessionState
    /// IReadOnlySessionState,为只读的session 不可以修改
    /// IRequiresSessionState ，可以修改。
    /// </summary>
    public class HomeHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 处理http请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //PathInfo：/Login或者/GetSession
            string methodName = context.Request.PathInfo.Substring(1);

            //反射获取方法，并invoke执行
            MethodInfo method = this.GetType().GetMethod(methodName);
            method.Invoke(this, new object[] { context });
        }

        /// <summary>
        /// 获取一个值，该值指示其他请求是否可以使用 IHttpHandler 实例。
        /// 如果 IHttpHandler 实例可再次使用，则为 true；否则为 false。 
        /// 默认为false即可
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="context"></param>
        public void Login(HttpContext context)
        {
            string name = context.Request["name"];
            string password = context.Request["password"];
            context.Session["cname"] = name;

            if (password == "1")
            {
                context.Response.Write("<script>alert('登录成功！');</script>");
            }
            else
            {
                context.Response.Write("<script>alert('登录失败！');</script>");
            }
            //跳转到一个静态html页面
            //context.Server.Transfer("Home.html");
        }

        /// <summary>
        /// 获取session
        /// </summary>
        /// <param name="context"></param>
        public void GetSession(HttpContext context)
        {
            context.Response.Write(
                string.Format("<h1>当前session-cname：{0}</h1>", context.Session["cname"]));

            //设置cookie，1hour后过期，js查看使用 document.cookie
            context.Response.Cookies["CurUser"]["cname"] = context.Session["cname"].ToString();
            context.Response.Cookies["CurUser"]["tip"] = "hello";
            context.Response.Cookies["CurUser"].Expires = DateTime.Now.AddHours(1);

            //设置30s后过期的cookie
            context.Response.Cookies["Temp"]["t-name"] = context.Session["cname"].ToString();
            context.Response.Cookies["Temp"].Expires = DateTime.Now.AddSeconds(30);
        }
    }
}