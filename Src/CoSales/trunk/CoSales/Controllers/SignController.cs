using CoSales.BLL;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    public class SignController : Core.BaseController
    {

        public ActionResult SignInView()
        {
            return View();
        }

        public ActionResult SignUpView()
        {
            return View();
        }

        public JsonResult Add(User entity)
        {
            ResultStateDTO result = new ResultStateDTO();

            return Json(result);
        }

        public JsonResult Login(string userId, string password)
        {
            ResultStateDTO state = new ResultStateDTO();
            User res = UserMgr.Mgr.GetUser(userId, password);

            // 登录成功时，保存当前用户信息至Session，并将基本用户信息写入cookie，以便前端的全局访问
            if (null != res)
            {
                ContextObjects.CurrentUser = res;
                HttpContext.Response.Cookies.Add(new HttpCookie("ID", res.ID.ToString()));
                HttpContext.Response.Cookies.Add(new HttpCookie("UserID", res.UserID));
                HttpContext.Response.Cookies.Add(new HttpCookie("UserName", res.UserName));
                state.Status = true;
            }

            return Json(state);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("SignInView");
        }
    }
}