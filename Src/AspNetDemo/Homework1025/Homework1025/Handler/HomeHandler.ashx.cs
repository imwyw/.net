using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace Homework1025.Handler
{
    /// <summary>
    /// HomeHandler 的摘要说明
    /// 使用session需要继承接口IRequiresSessionState
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

        public void Login(HttpContext context)
        {
            string name = context.Request["name"];
            string pwd = context.Request["pwd"];

            User user = UserMgr.GetUser(name, pwd);
            //返回json格式
            if (null != user)
            {
                context.Response.Write("{\"status\":true,\"msg\":\"欢迎使用xxxx\"}");
            }
            else
            {
                context.Response.Write("{\"status\":false,\"msg\":\"不存在该用户或密码错误，请注册或找回密码\"}");
            }
        }

        public void Add(HttpContext context)
        {
            string name = context.Request["name"];
            string pwd = context.Request["pwd"];

            User user = new User();
            user.Name = name;
            user.Pwd = pwd;

            bool res = UserMgr.AddUser(user);
            if (res)
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("false");
            }
        }

        public void GetOnlineCount(HttpContext context)
        {
            int count = int.Parse(context.Application["online"].ToString());
            context.Response.Write("{\"count\":" + count + "}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}