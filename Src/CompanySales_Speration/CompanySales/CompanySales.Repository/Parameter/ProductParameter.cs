using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySales.Repository.Parameter
{
    /// <summary>
    /// 产品查询参数类
    /// </summary>
    public class ProductParameter : PageParameter
    {
        public string ProductName { get; set; }
    }
}
