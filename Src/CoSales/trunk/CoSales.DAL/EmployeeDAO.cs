using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.DAL
{
    public class EmployeeDAO
    {
        public static readonly EmployeeDAO DAO = new EmployeeDAO();

        public List<Employee> GetList()
        {
            return DapperHelper.GetList<Employee>().ToList();
        }
    }
}
