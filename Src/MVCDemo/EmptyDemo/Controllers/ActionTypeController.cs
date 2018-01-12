using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Controllers
{
    public class ActionTypeController : Controller
    {
        /// <summary>
        /// 一个请求 返回一个视图
        /// 点击导航实现页面的跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回其他视图
        /// http://localhost:4236/ActionType/IndexByName
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexByName()
        {
            return View("Index");
        }

        /// <summary>
        /// 重定向到其他的Action，交给其他Action处理返回
        /// http://localhost:4236/ActionType/Redirect2Action
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult Redirect2Action()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// ajax请求 返回数据
        /// 比如查询数据返回后绑定到table
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData()
        {
            return Json(new { name = "jack", age = 12 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 跳转至其他URL 比如友情链接功能
        /// </summary>
        /// <returns></returns>
        public RedirectResult Redirect2Url()
        {
            return Redirect("//www.baidu.com");
        }

        public ActionResult LoginView()
        {
            return View();
        }
    }
}