using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    public class HomeController : Core.BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainView()
        {
            return View();
        }

        /// <summary>
        /// 概览视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SurveyView()
        {
            return View();
        }
    }
}