using CoSales.BLL;
using CoSales.Core;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
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
        /// 根据id获取用户实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(int id)
        {
            User res = UserMgr.Mgr.GetUser(id);
            return Json(res);
        }

        public JsonResult SaveUserInfo(User entity)
        {
            ResultStateDTO result = new ResultStateDTO();

            result.Status = UserMgr.Mgr.UpdateInfo(entity);
            return Json(result);
        }

    }
}