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
    /// <summary>
    /// 销售订单
    /// </summary>
    [CustomAuth]
    public class SellOrderController : BaseController
    {
        /// <summary>
        /// 销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult SellOrderListView()
        {
            return View();
        }

        /// <summary>
        /// 进货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseOrderListView()
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