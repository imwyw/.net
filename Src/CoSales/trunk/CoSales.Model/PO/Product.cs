using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_PRODUCT")]
    public class Product : PageInfo
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int ProductStockNumber { get; set; }
        public int ProductSellNumber { get; set; }
        public int State { get; set; }
        /// <summary>
        /// 产品状态对应中文名称
        /// </summary>
        public string StateText { get; set; }
        /// <summary>
        /// 产品状态筛选
        /// </summary>
        public List<int> StateList { get; set; }
    }
}
