using CoSales.Core;
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
    }
}