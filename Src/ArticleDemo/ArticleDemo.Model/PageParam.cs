using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Model
{
    /// <summary>
    /// 分页参数类
    /// </summary>
    public class PageParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Skip { get { return (PageIndex - 1) * PageSize; } }
    }
}
