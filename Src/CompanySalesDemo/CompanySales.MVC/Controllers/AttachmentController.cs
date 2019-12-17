using CompanySales.BLL;
using CompanySales.Model;
using CompanySales.Model.Entity;
using CompanySales.MVC.Base;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    [CustomActionFilter]
    public class AttachmentController : BaseController
    {
        /// <summary>
        /// 根据id删除附件资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveAttachById(int id)
        {
            StateModel res = new StateModel();
            res.Status = AttachmentMgr.DeleteById(id);
            return Json(res);
        }

        /// <summary>
        /// 根据路径获取图片
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public FileResult GetImageByPath(string fullPath)
        {
            string mime = MimeMapping.GetMimeMapping(fullPath);
            return File(fullPath, mime);
        }

        /// <summary>
        /// 获取关联id对应的资源集合
        /// </summary>
        /// <param name="relatedId">关联资源主键ID</param>
        /// <returns></returns>
        public JsonResult GetListByRelatedId(int relatedId)
        {
            List<Attachment> res = AttachmentMgr.GetListByRelatedId(relatedId);
            return Json(res);
        }
    }
}