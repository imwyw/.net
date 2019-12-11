using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected new JsonResult Json(object data)
        {
            return Json(data, JsonRequestBehavior.DenyGet);
        }

        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            if (data == null)
                return base.Json(null);

            if (!Request.AcceptTypes.Contains("application/json"))
                return new JsonConvertResult { Data = data, ContentType = "text/plain", JsonRequestBehavior = behavior };
            else
                return new JsonConvertResult { Data = data, JsonRequestBehavior = behavior };
        }


    }
}