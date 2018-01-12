using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArticleDemo.BLL
{
    public class ContextObjects
    {
        /// <summary>
        /// 静态变量 保存当前用户
        /// </summary>
        public static User CurrentUser
        {
            get { return HttpContext.Current.Session["CurUser"] as User; }
            set { HttpContext.Current.Session["CurUser"] = value; }
        }
    }
}
