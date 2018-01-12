using ArticleDemo.BLL;
using ArticleDemo.Common;
using ArticleDemo.Model;
using ArticleDemo.MVC.UI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleDemo.MVC.UI.Controllers
{
    public class LoginController : Controller
    {
        UserMgrProxy usermgr = new UserMgrProxy();

        #region Views

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SignInView()
        {
            //针对已经保存cookie的用户尝试自动登录，需要在浏览器保存密码到cookie，所以密码不能以明文形式出现，通过md5加密后保存在浏览器端
            User user = CheckAutoLoginByCookie();
            if (null != user)
            {
                ContextObjects.CurrentUser = user;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpView()
        {
            return View();
        }

        /// <summary>
        /// 退出登录，重定向到登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            //return View("SignInView");

            //用户主动退出的情况下 cookie立即失效
            RemoveAutoLoginCookie();

            return RedirectToAction("SignInView");
        }
        #endregion

        #region Datas
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <param name="validateCode">验证码</param>
        /// <param name="chkAuto">是否自动登录</param>
        /// <returns></returns>
        public JsonResult SignIn(string username, string userpwd, string validateCode, bool chkAuto = false)
        {
            Models.ViewModelState model = new Models.ViewModelState();
            User user = usermgr.Login(username, userpwd);

            //使用 string.Equals提升效率
            if (!string.Equals(validateCode, TempData["VerificationCode"].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                model.Status = false;
                model.Msg = "验证码输入有误";
                return Json(model);
            }

            if (null == user)
            {
                model.Status = false;
                model.Msg = "用户名或密码错误";
            }
            else
            {
                //登录成功后设置session
                //Session["CurUser"] = user;
                ContextObjects.CurrentUser = user;
                model.Status = true;

                //设置自动登录的话，需要将用户信息写入cookie
                if (chkAuto)
                {
                    SetAutoLoginCookie(user);
                }
            }

            return Json(model);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult AddUser(User user)
        {
            Models.ViewModelState viewmodel = new Models.ViewModelState();
            viewmodel.Status = usermgr.Add(user);
            return Json(viewmodel);
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateImg()
        {
            //创建长度为4的验证字符串
            string verificationCode = ValidateCode.CreateVerificationText(4);
            TempData["VerificationCode"] = verificationCode.ToUpper();

            byte[] bytes = ValidateCode.CreateImage(verificationCode, 80, 30);

            return File(bytes, @"image/jpeg");
        }
        #endregion

        #region 私有方法
        string cookieAutoLogin = "autoLogin";
        /// <summary>
        /// 设置用户信息到cookie
        /// </summary>
        /// <param name="u"></param>
        private void SetAutoLoginCookie(User u)
        {
            HttpCookie loginCoo = new HttpCookie(cookieAutoLogin);
            loginCoo["Name"] = u.Name.ToString();
            loginCoo["CipherPwd"] = u.Pwd;

            //有效期三天
            loginCoo.Expires = DateTime.Now.AddDays(3);
            HttpContext.Response.SetCookie(loginCoo);
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
                string name = HttpContext.Request.Cookies[cookieAutoLogin]["Name"];
                string pwd = HttpContext.Request.Cookies[cookieAutoLogin]["CipherPwd"];
                obj = usermgr.Login(name, pwd, true);
            }
            return obj;
        }
        #endregion
    }
}