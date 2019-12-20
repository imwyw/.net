namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

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

        public int? DepartmentID { get; set; }
    }
}
