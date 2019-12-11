using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Parameter
{
    /// <summary>
    /// 销售记录查询参数
    /// </summary>
    public class SellOrderParam : PageParameter
    {
        public int SellOrderID { get; set; }
        public string ProductName { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public DateTime? SellOrderDate { get; set; }
    }
}
