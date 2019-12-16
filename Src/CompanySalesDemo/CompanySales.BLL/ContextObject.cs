using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

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

        /// <summary>
        /// 所有附件资源保存的根路径
        /// 在web.config中配置
        /// </summary>
        private static string baseFilePath;
        public static string BaseFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(baseFilePath))
                {
                    baseFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"];
                }
                return baseFilePath;
            }
        }

        /// <summary>
        /// 用户头像保存位置
        /// </summary>
        public static string UserImagePath
        {
            get
            {
                return Path.Combine(BaseFilePath, "UserImage");
            }
        }
    }
}
