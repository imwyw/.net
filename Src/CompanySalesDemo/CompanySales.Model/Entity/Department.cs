namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentID { get; set; }

        [StringLength(30)]
        public string DepartmentName { get; set; }

        [StringLength(8)]
        public string Manager { get; set; }

        [StringLength(50)]
        public string Depart_Description { get; set; }
    }
}
