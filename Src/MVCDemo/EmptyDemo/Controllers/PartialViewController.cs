using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialAutoComplete()
        {
            ViewBag.Arrstr = "鼠标,显示器,键盘";
            return View();
        }
    }
}