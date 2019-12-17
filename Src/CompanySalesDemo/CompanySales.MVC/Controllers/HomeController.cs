using CompanySales.BLL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanySales.MVC.Base;

namespace CompanySales.MVC.Controllers
{
    [CusAuth]
    public class HomeController : BaseController
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

        /// <summary>
        /// 重新数据库，执行【InitTables.sql】脚本
        /// </summary>
        /// <returns></returns>
        public JsonResult RebuildData()
        {
            StateModel state = new StateModel();
            state.Status = UserMgr.RebuildData();

            return Json(state);
        }
    }
}