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
        public PageParameter(int pageindex = 1, int pagesize = 10)
        {
            PageIndex = pageindex;
            PageSize = pagesize;
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
    }
}
