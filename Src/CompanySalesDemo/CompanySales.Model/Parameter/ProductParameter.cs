using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Parameter
{
    /// <summary>
    /// 产品查询参数类
    /// </summary>
    public class ProductParameter : PageParameter
    {
        public string ProductName { get; set; }
    }
}
