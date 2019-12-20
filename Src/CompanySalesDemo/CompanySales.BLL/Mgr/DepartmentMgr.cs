using CompanySales.DAL;
using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.BLL
{
    public class DepartmentMgr
    {
        public static bool Add(Department entity)
        {
            return DepartmentDAO.Add(entity);
        }

        public static bool Delete(int id)
        {
            return DepartmentDAO.Delete(id);
        }

        public static List<Department> GetList()
        {
            return DepartmentDAO.GetList();
        }

        public static Pager<DeptEmpInfo> GetDeptEmpInfo(DeptEmpParameter parameter)
        {
            return DepartmentDAO.GetDeptEmpInfo(parameter);
        }
    }
}
