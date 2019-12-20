using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanySales.MVC.Models
{
    /// <summary>
    /// 对应layui的tree组件的 数据源
    /// </summary>
    public class LayTreeItem
    {
        public LayTreeItem()
        {
            spread = false;// 默认全部收缩
            @checked = false; // 默认不选中
            disabled = false;// 默认不禁用
            children = new List<LayTreeItem>();
        }
        /// <summary>
        /// 节点标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 节点唯一索引值，用于对指定节点进行各类操作	
        /// 任意唯一的字符或数字
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点字段名
        /// 一般对应表字段名
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 子节点。支持设定选项同父节点
        /// [{title: '子节点1', id: '111'}]
        /// </summary>
        public List<LayTreeItem> children { get; set; }
        /// <summary>
        /// 点击节点弹出新窗口对应的 url。需开启 isJump 参数
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 节点是否初始展开，默认 false
        /// </summary>
        public bool spread { get; set; }
        /// <summary>
        /// 节点是否初始为选中状态（如果开启复选框的话），默认 false
        /// checked是c#中保留关键字，需要加上@
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 节点是否为禁用状态。默认 false
        /// </summary>
        public bool disabled { get; set; }
    }
}