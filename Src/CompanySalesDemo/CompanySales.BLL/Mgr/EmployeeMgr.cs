using CompanySales.DAL;
using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.BLL
{
    public class EmployeeMgr
    {
        public static bool Add(Employee emp)
        {
            // 一些对员工的业务操作

            return EmployeeDAO.Add(emp);
        }

        public static DataTable GetEmployeeData()
        {
            return EmployeeDAO.GetEmployeeData();
        }

        public static bool Delete(int id)
        {
            return EmployeeDAO.Delete(id);
        }

        public static bool Update(Employee emp)
        {
            return EmployeeDAO.Update(emp);
        }

        public static Employee GetEmployeeById(int id)
        {
            return EmployeeDAO.GetEmployeeById(id);
        }

        public static List<Employee> GetList()
        {
            return EmployeeDAO.GetList();
        }
    }
}
