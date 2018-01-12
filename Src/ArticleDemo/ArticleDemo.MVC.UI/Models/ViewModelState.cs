using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleDemo.MVC.UI.Models
{
    public class ViewModelState
    {
        /// <summary>
        /// 指明状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 返回消息摘要
        /// </summary>
        public string Msg { get; set; }
    }
}