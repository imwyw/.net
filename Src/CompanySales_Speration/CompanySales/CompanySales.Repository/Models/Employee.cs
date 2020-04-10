using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    public partial class Employee
    {
        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [StringLength(50)]
        public string EmployeeName { get; set; }
        [StringLength(2)]
        public string Sex { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? HireDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
    }
}
