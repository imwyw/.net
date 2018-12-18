using CoSales.BLL;
using CoSales.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace CoSales.Controllers
{
    /// <summary>
    /// 主页及相关
    /// </summary>
    [CustomAuth]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainView()
        {
            ViewBag.UserName = ContextObjects.CurrentUser.UserName;
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