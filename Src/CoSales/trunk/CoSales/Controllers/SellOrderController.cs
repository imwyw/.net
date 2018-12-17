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
    }
}