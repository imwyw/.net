using CompanySales.BLL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanySales.MVC.Base;
using CompanySales.Common;
using System.Threading;

namespace CompanySales.MVC.Controllers
{
    /// <summary>
    /// 用户相关操作
    /// </summary>
    public class SecurityController : BaseController
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            // 访问登录页面前判断是否可以进行自动登录，满足条件直接跳转至主页
            if (null != CheckAutoLoginByCookie())
            {
                return RedirectToAction("Index", "Home");
            }
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
        /// 图片验证码 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginImgVerify()
        {
            return View();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="autoLogin">是否自动登录</param>
        /// <returns></returns>
        public ActionResult SignIn(string uid, string pwd, bool autoLogin = false)
        {
            // 模拟耗时，测试前端loading状态
            //Thread.Sleep(2000);

            User result = UserMgr.Login(uid, pwd);
            StateModel state = new StateModel();
            if (null != result)
            {
                state.Status = true;
                // 当前用户对象赋值给session
                // Session["uname"] = result;
                ContextObject.CurrentUser = result;

                SetUserCookie(result);

                if (autoLogin)
                {
                    SetAutoLoginCookie(result);
                }

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

            // 主动注销则删除所有cookie，并不再进行自动登录
            RemoveAllCookie();

            return RedirectToAction("Login");
        }

        #region 传统图片验证码方式
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        public FileResult GetValidateImg()
        {
            // 创建长度为4的验证字符串
            string verificationCode = ValidateCode.CreateVerificationText(4);
            // 将验证码保存至 TempData，读取一次即销毁
            TempData["VerificationCode"] = verificationCode.ToUpper();

            byte[] bytes = ValidateCode.CreateImage(verificationCode, 80, 30);

            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 生成验证码图片对应的base64编码，用于图片显示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetValidateImgBase64()
        {
            // 创建长度为4的验证字符串
            string verificationCode = ValidateCode.CreateVerificationText(4);
            // 将验证码保存至 TempData，读取一次即销毁
            TempData["VerificationCode"] = verificationCode.ToUpper();

            string res = ValidateCode.CreateImageVerifyBase64(verificationCode, 80, 30);
            return Json(res);
        }


        /// <summary>
        /// 登录判断，带验证码的判断
        /// </summary>
        public JsonResult SignByImageVerify(string uid, string pwd, string validateCode)
        {
            StateModel state = new StateModel();

            // 模拟耗时，测试前端loading状态
            //Thread.Sleep(2000);

            //忽略大小写进行判断验证码是否匹配，使用 string.Equals提升效率
            if (!string.Equals(validateCode, TempData["VerificationCode"].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                state.Status = false;
                state.Message = "验证码输入有误";
                return Json(state);
            }

            User res = UserMgr.Login(uid, pwd);
            // 登录成功时，保存当前用户信息至Session，并将基本用户信息写入cookie，以便前端的全局访问
            if (null != res)
            {
                ContextObject.CurrentUser = res;
                state.Status = true;
            }
            else
            {
                state.Message = "用户名或密码有误，请重新登录！";
            }

            return Json(state);
        }
        #endregion

        #region 利用cookie自动登录
        /// <summary>
        /// cookie key值 用户id 
        /// </summary>
        static readonly string cookieUserid = "UserID";
        /// <summary>
        /// cookie key值 用户加密后的密码 
        /// </summary>
        static readonly string cookieCipherPwd = "CipherPwd";
        /// <summary>
        /// 设置用户信息到cookie，用于访问主页时直接判断并登录
        /// </summary>
        /// <param name="entity"></param>
        private void SetAutoLoginCookie(User entity)
        {
            AddCookie(cookieUserid, entity.UserId, DateTime.Now.AddDays(3));
            AddCookie(cookieCipherPwd, entity.Password, DateTime.Now.AddDays(3));
        }

        /// <summary>
        /// 从cookie中获取当前用户的登录name和pwd，尝试自动登录
        /// </summary>
        /// <returns></returns>
        private User CheckAutoLoginByCookie()
        {
            User currentUser = null;
            if (Request.Cookies[cookieUserid] != null &&
                Request.Cookies[cookieCipherPwd] != null)
            {
                string uid = GetCookie(cookieUserid);
                string pwd = GetCookie(cookieCipherPwd);
                currentUser = UserMgr.Login(uid, pwd, true);
                ContextObject.CurrentUser = currentUser;

                // 自动登录时需要重新写入cookie，否则客户端无法获取当前用户相关信息
                SetUserCookie(currentUser);
            }
            return currentUser;
        }

        /// <summary>
        /// 设置用户cookie 基本信息，但不包含密码
        /// </summary>
        /// <param name="entity"></param>
        private void SetUserCookie(User entity)
        {
            AddCookie("int_id", entity.ID.ToString());
            AddCookie("UserId", entity.UserId);
            AddCookie("Name", entity.Name);
            AddCookie("Roles", entity.Roles);
        }
        #endregion
    }
}