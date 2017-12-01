using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jQueryDemo
{
    /// <summary>
    /// AjaxHandler 的摘要说明
    /// </summary>
    public class AjaxHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("{\"ID\":123,\"NAME\":\"jack\"}");
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