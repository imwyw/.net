using CompanySales.BLL;
using CompanySales.Model.Entity;using CompanySales.Model;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    [CusAuth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (null == Session["uname"])
            //{
            //    return RedirectToAction("Login", "Security");
            //}
            //User usr = Session["uname"] as User;
            ViewBag.CurUserID = ContextObject.CurrentUser.UserId + "==" + ContextObject.CurrentUser.Name;
            return View();
        }


    }
}