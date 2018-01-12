using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            TestServiceReference.TestWebServiceSoapClient srv = new TestServiceReference.TestWebServiceSoapClient();
            ViewBag.Hi = srv.HelloWorld();

            IIS_ServiceReference.TestWebServiceSoapClient srvIIS = new IIS_ServiceReference.TestWebServiceSoapClient();
            var str = srvIIS.HelloWorld();

            TestServiceReference.SoapHeaderHelper valid = new TestServiceReference.SoapHeaderHelper();
            valid.UserID = "admin";
            valid.UserPwd = "admin";

            var res = srv.PostData(valid, "jack", "admin");

            GetDataByWcf();

            return View();
        }

        public JsonResult GetWeatherByCityName(string cityName)
        {
            cn.com.webxml.www.WeatherWebService srv = new cn.com.webxml.www.WeatherWebService();
            var res = srv.getWeatherbyCityName(cityName);

            return Json(res);
        }

        /// <summary>
        /// wcf 服务引用的调用
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDataByWcf()
        {
            CalculatorServiceReference.CalculatorServiceClient srv = new CalculatorServiceReference.CalculatorServiceClient();
            var res = srv.Add(2.34, 4.32);

            var res1 = srv.QueryBook(1);
            return Json(res);
        }
    }
}