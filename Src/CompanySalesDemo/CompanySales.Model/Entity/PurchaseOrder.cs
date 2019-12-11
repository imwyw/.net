namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Purchase_order")]
    public partial class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderID { get; set; }

        public int? ProductID { get; set; }

        public int? PurchaseOrderNumber { get; set; }

        public int? EmployeeID { get; set; }

        public int? ProviderID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PurchaseOrderDate { get; set; }
    }
}
