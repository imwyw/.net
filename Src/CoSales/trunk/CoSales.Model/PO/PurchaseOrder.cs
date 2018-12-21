using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    /// <summary>
    /// 进货订单
    /// </summary>
    [Dapper.Table("T_PURCHASE_ORDER")]
    public class PurchaseOrder
    {
        /// <summary>
        /// 标识为主键
        /// </summary>
        [Dapper.Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int PurchaseOrderNumber { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int ProviderID { get; set; }
        public Provider Provider { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
    }
}
