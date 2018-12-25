using CoSales.DAL;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.BLL
{
    public class EmployeeMgr
    {
        public static readonly EmployeeMgr Mgr = new EmployeeMgr();

        public List<Employee> GetList()
        {
            var res = EmployeeDAO.DAO.GetList();
            return res;
        }
    }
}
