using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo
{
    /// <summary>
    /// HomeHandler 的摘要说明
    /// </summary>
    public class HomeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];
            string name = context.Request["name"];

            context.Response.ContentType = "text/plain";
            context.Response.Write("{\"ID\":" + id + ",\"NAME\":\"" + name + "\"}");
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