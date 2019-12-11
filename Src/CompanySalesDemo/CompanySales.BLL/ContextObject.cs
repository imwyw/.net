using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CompanySales.BLL
{
    public class ContextObject
    {
        /// <summary>
        /// 当前用户信息
        /// 登录后赋值，权限的判断需要使用到
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["Current"] as User;
            }
            set
            {
                HttpContext.Current.Session["Current"] = value;
            }
        }
    }
}
