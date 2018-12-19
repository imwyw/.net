using CoSales.BLL;
using CoSales.Core;
using CoSales.Model;
using CoSales.Model.PO;
using CoSales.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    /// <summary>
    /// 个人相关信息
    /// </summary>
    [CustomAuth]
    public class PersonalController : BaseController
    {
        /// <summary>
        /// 个人详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BasicInfoView()
        {
            return View();
        }

        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PasswordView()
        {
            return View();
        }

        /// <summary>
        /// 根据id获取用户实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(int id)
        {
            User res = UserMgr.Mgr.GetUser(id);
            return Json(res);
        }

        /// <summary>
        /// 更新用户基本信息，但不包含密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveUserInfo(User entity)
        {
            ResultStateDTO result = new ResultStateDTO();

            result.Status = UserMgr.Mgr.UpdateInfo(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveUserImg()
        {
            // 获取上传的文件
            var files = HttpContext.Request.Files;
            LayStateModel state = new LayStateModel();
            if (files.Count == 0)
            {
                state.code = -1;
                state.msg = "未能接受到上传文件，请重新检查上传";
            }
            else
            {
                HttpPostedFileBase file = files[0];

                // 用户端上传的文件名称
                string fileName = file.FileName;

                // 校验服务端保存的路径是否存在
                if (!Directory.Exists(ContextObjects.UserImagePath))
                {
                    Directory.CreateDirectory(ContextObjects.UserImagePath);
                }

                // 修改文件名称为用户id，否则容易造成上传文件名称冲突。或者使用guid作为文件名称保存
                string extension = Path.GetExtension(fileName);
                string fullName = Path.Combine(ContextObjects.UserImagePath, ContextObjects.CurrentUser.UserID + extension);

                // 另存为至服务器上指定路径
                file.SaveAs(fullName);
                state.data = new { src = fullName };
                if (!UpdateHeadImage(fullName))
                {
                    state.code = -1;
                    state.msg = "图片上传成功，但更新失败，请重新上传！";
                }
            }

            return Json(state);
        }

        /// <summary>
        /// 获取当前用户头像
        /// </summary>
        /// <returns></returns>
        public FileResult GetUserImg()
        {
            // 图片信息从数据库中获取，session中获取无法保证是最新的
            User user = UserMgr.Mgr.GetUser(ContextObjects.CurrentUser.ID);
            string mime = MimeMapping.GetMimeMapping(user.HeadImg);
            return File(user.HeadImg, mime);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public JsonResult ChangePwd(string oldPwd, string newPwd)
        {
            ResultStateDTO result = new ResultStateDTO();
            string msg = "";
            result.Status = UserMgr.Mgr.UpdatePwd(oldPwd, newPwd, out msg);
            result.Message = msg;
            return Json(result);
        }

        /// <summary>
        /// 更新当前用户的头像信息
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private bool UpdateHeadImage(string fullName)
        {
            User entity = new Model.PO.User();
            entity.ID = ContextObjects.CurrentUser.ID;
            entity.HeadImg = fullName;
            bool res = UserMgr.Mgr.UpdateImage(entity);
            return res;
        }

    }
}