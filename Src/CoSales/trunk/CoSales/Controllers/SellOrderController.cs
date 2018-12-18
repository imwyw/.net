using CoSales.BLL;
using CoSales.Core;
using CoSales.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    [CustomAuth]
    public class SellOrderController : Core.BaseController
    {
        public ActionResult SellOrderListView()
        {
            return View();
        }

        public JsonResult GetSellOrderInfo(SellOrderInfo param)
        {
            var result = SellOrderMgr.Mgr.GetSellOrderInfo(param);
            return Json(result);
        }
    }
}