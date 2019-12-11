using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Domain
{
    /// <summary>
    /// 销售记录
    /// </summary>
    public class SellOrderDomain
    {
        public int? ID { get; set; }
        public int? ProductID { get; set; }
        public int? SellOrderNumber { get; set; }
        public int? EmployeeID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime? SellOrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int ProductStockNumber { get; set; }
        public int ProductSellNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public int? DepartmentID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
    }
}
