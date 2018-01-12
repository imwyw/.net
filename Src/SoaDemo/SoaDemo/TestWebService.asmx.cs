using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace SoaDemo
{
    /// <summary>
    /// TestWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class TestWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// 简单验证实例 必须为public
        /// </summary>
        public SoapHeaderHelper simpleValid = new SoapHeaderHelper();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [SoapHeader("simpleValid")]
        public Student PostData(string name, string pwd)
        {
            return new Student() { Name = name, Pwd = pwd };
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }

    /// <summary>
    /// 自定义SOAP头
    /// </summary>
    public class SoapHeaderHelper : SoapHeader
    {
        public string UserID { get; set; }
        public string UserPwd { get; set; }

        /// <summary>
        /// 简单验证消息
        /// out 参数 传递的不是副本，而是对象的引用
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsValid(out string msg)
        {
            if (UserID != "admin" || UserPwd != "admin")
            {
                msg = "无权访问";
                return false;
            }
            msg = string.Empty;
            return true;
        }
    }
}
