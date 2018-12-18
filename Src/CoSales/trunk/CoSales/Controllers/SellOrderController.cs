using CoSales.BLL;
using CoSales.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
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