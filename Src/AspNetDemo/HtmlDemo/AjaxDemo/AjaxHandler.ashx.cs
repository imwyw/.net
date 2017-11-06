using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace AjaxDemo
{
    /// <summary>
    /// AjaxHandler 的摘要说明
    /// </summary>
    public class AjaxHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string methdName = context.Request.PathInfo.Substring(1);
            MethodInfo method = this.GetType().GetMethod(methdName);
            method.Invoke(this, new object[] { context });
        }

        public void SetSession(HttpContext context)
        {
            string name = context.Request["name"];
            context.Session["name"] = name;
            context.Response.ContentType = "text/plain";
            context.Response.Write("更新成功");
        }

        public void GetSession(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(context.Session["name"].ToString());
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