using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace ArticleDemo.UnitTest.UnitHelper
{
    /// <summary>
    /// 测试帮助类
    /// </summary>
    public class MockSession
    {
        /// <summary>
        /// NUnit中模拟Session创建，否则无法使用 HttpContext.Current.Session！！！
        /// 参考：http://www.necronet.org/archive/2010/07/28/unit-testing-code-that-uses-httpcontext-current-session.aspx
        /// </summary>
        public static void NewHttpContextBase()
        {
            //添加System.Web.dll的引用，构建模拟 HttpContext 上下文对象
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                                 new HttpStaticObjectsCollection(), 10, true,
                                                                 HttpCookieMode.AutoDetect,
                                                                 SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                     BindingFlags.NonPublic | BindingFlags.Instance,
                                                     null, CallingConventions.Standard,
                                                     new[] { typeof(HttpSessionStateContainer) },
                                                     null)
                                                .Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
        }
    }
}
