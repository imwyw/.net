namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sell_Order")]
    public partial class SellOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellOrderID { get; set; }

        public int? ProductID { get; set; }

        public int? SellOrderNumber { get; set; }

        public int? EmployeeID { get; set; }

        public int? CustomerID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SellOrderDate { get; set; }
    }
}
