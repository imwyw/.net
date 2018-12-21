using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_SELL_ORDER")]
    public class SellOrder
    {
        /// <summary>
        /// 标识为主键
        /// </summary>
        [Dapper.Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int SellOrderNumber { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public DateTime SellOrderDate { get; set; }
    }
}
