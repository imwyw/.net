using CoSales.BLL;
using CoSales.Core;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace CoSales.Controllers
{
    /// <summary>
    /// 登录/注册相关操作
    /// </summary>
    public class SignController : BaseController
    {
        public ActionResult SignInView()
        {
            if (null != CheckAutoLoginByCookie())
            {
                return RedirectToAction("MainView", "Home");
            }
            return View();
        }

        public ActionResult SignUpView()
        {
            return View();
        }

        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="isAutoLogin">是否自动登录，即带入cookie中保存的密码</param>
        /// <returns></returns>
        public JsonResult Login(string userId, string password, bool isAutoLogin = false)
        {
            ResultStateDTO state = new ResultStateDTO();
            User res = UserMgr.Mgr.GetUser(userId, password);

            // 登录成功时，保存当前用户信息至Session，并将基本用户信息写入cookie，以便前端的全局访问
            if (null != res)
            {
                ContextObjects.CurrentUser = res;
                SetUserCookie(res);

                if (isAutoLogin)
                {
                    SetAutoLoginCookie(res);
                }
                state.Status = true;
            }

            return Json(state);
        }

        public JsonResult AddUser(User entity)
        {
            ResultStateDTO result = new ResultStateDTO();
            int res = UserMgr.Mgr.Add(entity);
            result.Status = res > 0;

            return Json(result);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            // 销毁会话，而不是简单的页面跳转
            Session.Abandon();

            // 主动退出时不再自动进行登录
            RemoveAutoLoginCookie();
            return RedirectToAction("SignInView");
        }

        #region 私有方法
        static readonly string cookieAutoLogin = "autoLogin";
        /// <summary>
        /// 设置用户信息到cookie
        /// </summary>
        /// <param name="u"></param>
        private void SetAutoLoginCookie(User u)
        {
            HttpCookie loginCoo = new HttpCookie(cookieAutoLogin);
            loginCoo["UserID"] = u.UserID;
            loginCoo["CipherPwd"] = u.Password;

            //有效期三天
            loginCoo.Expires = DateTime.Now.AddDays(3);
            HttpContext.Response.AppendCookie(loginCoo);
        }

        /// <summary>
        /// 设置cookie立即失效
        /// </summary>
        private void RemoveAutoLoginCookie()
        {
            HttpCookie loginCoo = new HttpCookie(cookieAutoLogin);
            loginCoo.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.SetCookie(loginCoo);
        }

        /// <summary>
        /// 从cookie中获取当前用户的登录name和pwd，尝试自动登录
        /// </summary>
        /// <returns></returns>
        private User CheckAutoLoginByCookie()
        {
            User obj = null;
            if (HttpContext.Request.Cookies[cookieAutoLogin] != null)
            {
                string uid = HttpContext.Request.Cookies[cookieAutoLogin]["UserID"];
                string pwd = HttpContext.Request.Cookies[cookieAutoLogin]["CipherPwd"];
                obj = UserMgr.Mgr.GetUser(uid, pwd, true);
                ContextObjects.CurrentUser = obj;

                // 自动登录时需要重新写入cookie，否则客户端无法获取当前用户相关信息
                SetUserCookie(obj);
            }
            return obj;
        }

        /// <summary>
        /// 设置用户cookie 基本信息
        /// </summary>
        /// <param name="entity"></param>
        private void SetUserCookie(User entity)
        {
            HttpContext.Response.AppendCookie(new HttpCookie("ID", entity.ID.ToString()));
            HttpContext.Response.AppendCookie(new HttpCookie("UserID", entity.UserID));
            HttpContext.Response.AppendCookie(new HttpCookie("UserName", entity.UserName));
        }
        #endregion
    }
}