namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Provider")]
    public partial class Provider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProviderID { get; set; }

        [StringLength(50)]
        public string ProviderName { get; set; }

        [StringLength(8)]
        public string ContactName { get; set; }

        [StringLength(100)]
        public string ProviderAddress { get; set; }

        [StringLength(15)]
        public string ProviderPhone { get; set; }

        [StringLength(20)]
        public string ProviderEmail { get; set; }
    }
}
