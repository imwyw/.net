using MVC_LogDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_LogDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveLog(string log)
        {
            //.....
            Log4Helper.InfoLog.InfoFormat("业务操作信息。。。。。 {0}，how do u do", log);
            return Json("");
        }

        public JsonResult ErrLog(string err)
        {
            Log4Helper.ErrorLog.ErrorFormat("sorry，发生异常，异常信息：{0}", err);
            return Json("");
        }
    }
}