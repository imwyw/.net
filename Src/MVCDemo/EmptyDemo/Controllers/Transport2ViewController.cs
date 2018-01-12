using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Controllers
{
    public class Transport2ViewController : Controller
    {
        // GET: Transport2View
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ViewData的传值
        /// url地址：localhost:xxx/Transport2View/ViewDataDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewDataDemo()
        {
            ViewData["a"] = "我们不一样";
            ViewData["obj"] = new { first = "这么多的年兄弟", second = "有谁能比我更了解你" };
            ViewData["exist"] = true;

            return View();
        }

        /// <summary>
        /// ViewBag的传值
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewBagDemo()
        {
            ViewBag.FirstName = "john";
            ViewBag.Stu = new { first = "丑八怪", second = "能否别把灯打开" };
            
            return View();
        }

        /// <summary>
        /// TempData的设置和取不在同一个http请求中
        /// 也就是不在同一个view中
        /// </summary>
        /// <returns></returns>
        public ActionResult SetTempDataView()
        {
            //session 
            TempData["Remark"] = "读取一次自动销毁!!!";
            return View();
        }
        public ActionResult GetTempDataView()
        {
            return View();
        }
    }
}