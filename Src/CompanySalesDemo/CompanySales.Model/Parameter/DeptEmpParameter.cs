using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Parameter
{
    /// <summary>
    /// 组织信息 查询参数类
    /// </summary>
    public class DeptEmpParameter : PageParameter
    {
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
    }
}
