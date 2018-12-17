using CoSales.BLL;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    public class SignController : Controller
    {
        public object ResultStateDTO { get; private set; }

        // GET: Sign
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Add(User entity)
        {
            ResutlStateDTO result = new ResutlStateDTO();

            return Json(result);
        }
    }
}