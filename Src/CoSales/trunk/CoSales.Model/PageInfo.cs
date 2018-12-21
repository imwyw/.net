using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model
{
    /// <summary>
    /// 分页信息，和layui-table分页参数相对应
    /// </summary>
    public abstract class PageInfo
    {
        /// <summary>
        /// 第几页
        /// </summary>
        [Dapper.NotMapped]
        public int? page { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        [Dapper.NotMapped]
        public int? limit { get; set; }
        [Dapper.NotMapped]
        public int PageStart
        {
            get
            {
                return (page.GetValueOrDefault(0) - 1) * limit.GetValueOrDefault(0);
            }
        }
        [Dapper.NotMapped]
        public int PageEnd
        {
            get
            {
                return page.GetValueOrDefault(0) * limit.GetValueOrDefault(0) + 1;
            }
        }
        /// <summary>
        /// 是否分页
        /// </summary>
        [Dapper.NotMapped]
        public bool IsPager { get { return page.HasValue; } }
    }
}
