using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_CUSTOMER")]
    public class Customer
    {
        /// <summary>
        /// 标识为主键
        /// </summary>
        [Dapper.Key]
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
    }
}
