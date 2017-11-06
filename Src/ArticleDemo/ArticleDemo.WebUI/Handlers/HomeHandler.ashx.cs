using ArticleDemo.BLL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace ArticleDemo.WebUI.Handlers
{
    /// <summary>
    /// HomeHandler 的摘要说明
    /// </summary>
    public class HomeHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string methodName = context.Request.PathInfo.Substring(1);
            MethodInfo method = GetType().GetMethod(methodName);
            method.Invoke(this, new object[] { context });
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void Login(HttpContext context)
        {
            string name = context.Request["name"];
            string pwd = context.Request["pwd"];

            User user = new UserMgr().Login(name, pwd);
            if (null != user)
            {
                //将当前用户写入到session中
                context.Session["CurUser"] = user;
                context.Response.Write("{\"status\":true}");
            }
            else
            {
                context.Response.Write("{\"status\":false}");
            }
        }

        public void Add(HttpContext context)
        {
            User user = new User();
            user.Name = context.Request["name"];
            user.Pwd = context.Request["pwd"];
            user.Zh_Name = context.Request["name"];

            bool res = new UserMgr().Add(user);
            if (res)
            {
                context.Response.Write("{\"status\":true}");
            }
            else
            {
                context.Response.Write("{\"status\":false}");
            }
        }
    }
}