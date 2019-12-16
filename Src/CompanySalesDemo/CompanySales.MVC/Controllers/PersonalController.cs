using CompanySales.BLL;
using CompanySales.Model;
using CompanySales.Model.Entity;
using CompanySales.MVC.Base;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    /// <summary>
    /// 个人信息维护，修改密码等
    /// </summary>
    [CustomActionFilter]
    public class PersonalController : BaseController
    {
        #region 视图
        /// <summary>
        /// 个人详情信息
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
        #endregion

        /// <summary>
        /// 根据id获取用户实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(int id)
        {
            User res = UserMgr.GetUserById(id);
            return Json(res);
        }

        ///// <summary>
        ///// 更新用户基本信息，但不包含密码
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        public JsonResult SaveUserInfo(User entity)
        {
            StateModel result = new StateModel();

            result.Status = UserMgr.UpdateInfo(entity);
            return Json(result);
        }

        ///// <summary>
        ///// 更新用户头像
        ///// </summary>
        ///// <returns></returns>
        public JsonResult SaveUserImg()
        {
            // 获取上传的文件
            var files = HttpContext.Request.Files;
            StateModel state = new StateModel();
            if (files.Count == 0)
            {
                state.Code = -1;
                state.Message = "未能接受到上传文件，请重新检查上传";
            }
            else
            {
                HttpPostedFileBase file = files[0];

                // 用户端上传的文件名称
                string fileName = file.FileName;

                // 校验服务端保存的路径是否存在
                if (!Directory.Exists(ContextObject.UserImagePath))
                {
                    Directory.CreateDirectory(ContextObject.UserImagePath);
                }

                // 修改文件名称为用户id，否则容易造成上传文件名称冲突。或者使用guid作为文件名称保存
                string extension = Path.GetExtension(fileName);
                string fullName = Path.Combine(ContextObject.UserImagePath,
                    ContextObject.CurrentUser.UserId + extension);

                // 另存为至服务器上指定路径
                file.SaveAs(fullName);
                state.Data = new { src = fullName };
                if (!UpdateHeadImage(fullName))
                {
                    state.Code = -1;
                    state.Message = "图片上传成功，但更新失败，请重新上传！";
                }
            }

            return Json(state);
        }

        ///// <summary>
        ///// 获取当前用户头像
        ///// </summary>
        ///// <returns></returns>
        public FileResult GetUserImg()
        {
            // 图片信息从数据库中获取，session中获取无法保证是最新的
            User user = UserMgr.GetUserById(ContextObject.CurrentUser.ID);
            // 默认头像设置
            string filePath = System.IO.File.Exists(user.HeadImg) ?
                user.HeadImg : "~/layui/images/face/1.gif";
            string mime = MimeMapping.GetMimeMapping(filePath);
            return File(filePath, mime);
        }

        /// <summary>
        /// 更新当前用户的头像信息
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private bool UpdateHeadImage(string fullName)
        {
            User entity = new User();
            entity.ID = ContextObject.CurrentUser.ID;
            entity.HeadImg = fullName;
            bool res = UserMgr.UpdateImage(entity);
            return res;
        }
    }
}