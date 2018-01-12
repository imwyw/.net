using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Controllers
{
    public class View2Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 通过表单提交参数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public ActionResult Login(string userid, string userpwd)
        {
            //以文本形式返回
            return Content(string.Format("该用户名：{0},密码：{1},在{2}尝试登录", userid, userpwd, DateTime.Now));
        }

        /// <summary>
        /// 通过ajax传递参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetDataAjax(SimpleParams param)
        {
            //json序列化后再返回
            return Json(param);
        }
    }

    /// <summary>
    /// 定义一个参数类
    /// </summary>
    public class SimpleParams
    {
        public string Type { get; set; }
        public string Level { get; set; }
    }
}