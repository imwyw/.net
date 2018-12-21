using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_DEPARTMENT")]
    public class Department
    {
        /// <summary>
        /// 标识为主键
        /// </summary>
        [Dapper.Key]
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string Manager { get; set; }
        public string Depart_Description { get; set; }

    }
}
