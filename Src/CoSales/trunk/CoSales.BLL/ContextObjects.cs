using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace CoSales.BLL
{
    /// <summary>
    /// 全局变量的保存
    /// </summary>
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

        /// <summary>
        /// 产品描述相关图片
        /// </summary>
        public static string ProductImagePath
        {
            get
            {
                return Path.Combine(BaseFilePath, "ProductImage");
            }
        }
    }
}
