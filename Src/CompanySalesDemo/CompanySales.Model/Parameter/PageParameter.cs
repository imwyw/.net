using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Parameter
{
    /// <summary>
    /// 分页查询时页码基类
    /// </summary>
    public class PageParameter
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="pageindex">当前页码，从1开始</param>
        /// <param name="pagesize">每页显示多少条记录</param>
        /// <param name="isPage">是否分页查询，默认开启分页查询</param>
        public PageParameter(int pageindex = 1, int pagesize = 10, bool isPage = true)
        {
            PageIndex = pageindex;
            PageSize = pagesize;
            IsPage = isPage;
        }
        /// <summary>
        /// 当前页码，默认值为1
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示的数据条数，默认为10
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 跳过多少条记录，用于EF查询时的Skip方法
        /// </summary>
        public int Skip
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
        /// <summary>
        /// 是否开启分页查询，分页关闭则查询所有数据
        /// </summary>
        public bool IsPage { get; set; }
    }
}
