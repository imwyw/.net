using CompanySales.BLL;
using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Parameter;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    [CustomActionFilter]
    public class OrderController : Base.BaseController
    {
        public ActionResult SellOrderList()
        {
            return View();
        }

        public ActionResult PurchaseOrderList()
        {
            return View();
        }

        public JsonResult GetSellOrderList(SellOrderParam parameter)
        {
            Pager<SellOrderDomain> result = OrderMgr.GetSellOrderByPage(parameter);
            return Json(result);
        }
    }
}