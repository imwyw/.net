using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class ResultPager<T>
    {
        /// <summary>
        /// 满足去掉分页条件下的记录总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 当前页码下的数据集合
        /// </summary>
        public List<T> Rows { get; set; }
    }
}
