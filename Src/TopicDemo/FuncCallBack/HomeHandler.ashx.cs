using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncCallBack
{
    /// <summary>
    /// HomeHandler 的摘要说明
    /// </summary>
    public class HomeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            string name = context.Request["username"];
            string pwd = context.Request["pwd"];
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