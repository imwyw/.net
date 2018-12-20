using CoSales.BLL;
using CoSales.Core;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult ProductListView()
        {
            return View();
        }

        /// <summary>
        /// 分页获取产品信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult GetProductInfo(Product param)
        {
            var result = ProductMgr.Mgr.GetProductInfo(param);
            return Json(result);
        }
    }
}