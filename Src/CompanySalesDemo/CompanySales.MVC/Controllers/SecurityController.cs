using CompanySales.BLL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    /// <summary>
    /// 用户相关操作
    /// </summary>
    public class SecurityController : Controller
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult SignIn(string uid, string pwd)
        {
            User result = UserMgr.Login(uid, pwd);
            StateModel state = new StateModel();
            if (null != result)
            {
                state.Status = true;
                // 当前用户对象赋值给session
                // Session["uname"] = result;
                ContextObject.CurrentUser = result;

                // 登录成功后，将用户基本信息写至cookie，方便前端使用
                Response.Cookies.Add(new HttpCookie("u_id", result.ID.ToString()));
                Response.Cookies.Add(new HttpCookie("userid", result.UserId));
                Response.Cookies.Add(new HttpCookie("username", result.Name));

                state.Message = "登录成功";
            }
            else
            {
                state.Status = false;
                state.Message = "用户名或密码错误！";
            }
            return Json(state);
        }

        /// <summary>
        /// 注册新的用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult AddUser(User user)
        {
            bool isAdd = UserMgr.AddUser(user);
            StateModel state = new StateModel(isAdd);
            if (!state.Status)
            {
                state.Message = "注册发生异常，请联系管理员！";
            }
            return Json(state);
        }

        public ActionResult Logout()
        {
            // session 销毁
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}