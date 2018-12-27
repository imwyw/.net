using CoSales.BLL;
using CoSales.Core;
using CoSales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    [CustomAuth]
    public class AttachmentController : BaseController
    {
        /// <summary>
        /// 根据id删除附件资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveAttachById(int id)
        {
            ResultStateDTO res = new ResultStateDTO();
            res.Status = AttachmentMgr.Mgr.DeleteById(id);
            return Json(res);
        }
    }
}