using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    [Table("Sell_Order")]
    public partial class SellOrder
    {
        [Key]
        [Column("SellOrderID")]
        public int SellOrderId { get; set; }
        [Column("ProductID")]
        public int? ProductId { get; set; }
        public int? SellOrderNumber { get; set; }
        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? SellOrderDate { get; set; }
    }
}
