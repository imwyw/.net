using ArticleDemo.BLL;
using ArticleDemo.MVC.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleDemo.MVC.UI.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        //[CustomActionFilter(Roles = "admin")]
        public ActionResult Index()
        {
            //将验证移至 CustomAuthrize 自定义特性
            //if (Session["CurUser"] == null)
            //{
            //    //如果session中不存在该用户则跳转至登录页面，不允许未登录的情况下直接进入
            //    return RedirectToAction("SignInView", "Login");
            //}
            return View();
        }

        /// <summary>
        /// 针对这个特殊的action，可以匿名访问，也就是忽略特性验证
        /// AllowAnonymous 特性 允许匿名访问，即忽略特性验证
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuList()
        {
            //var res = MenuActionMgr.GetMenuList();
            var res = MenuActionMgr.GetMenuListSelf();
            return Json(res);
        }

    }
}