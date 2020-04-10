using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    [Table("Purchase_order")]
    public partial class PurchaseOrder
    {
        [Key]
        [Column("PurchaseOrderID")]
        public int PurchaseOrderId { get; set; }
        [Column("ProductID")]
        public int? ProductId { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }
        [Column("ProviderID")]
        public int? ProviderId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PurchaseOrderDate { get; set; }
    }
}
