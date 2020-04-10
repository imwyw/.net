using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    public partial class Department
    {
        [Key]
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [StringLength(30)]
        public string DepartmentName { get; set; }
        [StringLength(8)]
        public string Manager { get; set; }
        [Column("Depart_Description")]
        [StringLength(50)]
        public string DepartDescription { get; set; }
    }
}
