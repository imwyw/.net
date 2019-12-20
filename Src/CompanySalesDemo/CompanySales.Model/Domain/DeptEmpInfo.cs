using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Domain
{
    /// <summary>
    /// 部门员工结构信息，用于展示部门员工结构信息
    /// </summary>
    public class DeptEmpInfo
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Manager { get; set; }
        public string DepartDescription { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Sex { get; set; }

        //[Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }

        //[Column(TypeName = "smalldatetime")]
        public DateTime? HireDate { get; set; }

        //[Column(TypeName = "money")]
        public decimal? Salary { get; set; }
    }
}
