using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Areas.Super.Controllers
{
    public class HomeController : Controller
    {
        // GET: Super/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}