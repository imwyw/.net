using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class DepartmentDAO
    {
        public static bool Add(Department entity)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Department.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Department.Find(id);
                db.Department.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        public static List<Department> GetList()
        {
            using (SaleContext db = new SaleContext())
            {
                return db.Department.ToList();
            }
        }

        public static Pager<DeptEmpInfo> GetDeptEmpInfo(DeptEmpParameter parameter)
        {
            StringBuilder sqlLayout = new StringBuilder(@"
SELECT  dep.DepartmentID ,
        dep.DepartmentName ,
        dep.Manager ,
        dep.Depart_Description AS DepartDescription,
        emp.EmployeeID ,
        emp.EmployeeName ,
        emp.Sex ,
        emp.BirthDate ,
        emp.HireDate ,
        emp.Salary ,
        emp.DepartmentID
FROM    dbo.Department dep
        LEFT JOIN dbo.Employee emp ON emp.DepartmentID = dep.DepartmentID
WHERE   1 = 1
");
            // 动态查询条件的拼接
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(parameter.DepartmentName))
            {
                sqlLayout.Append($" AND dep.DepartmentName like @DepartmentName ");
                paramList.Add(new SqlParameter("@DepartmentName", $"%{parameter.DepartmentName}%"));
            }
            if (!string.IsNullOrEmpty(parameter.EmployeeName))
            {
                sqlLayout.Append($" AND emp.EmployeeName like @EmployeeName ");
                paramList.Add(new SqlParameter("@EmployeeName", $"%{parameter.EmployeeName}%"));
            }

            Pager<DeptEmpInfo> result = new Pager<DeptEmpInfo>();

            result.Rows = EFUtility.GetList<DeptEmpInfo>(sqlLayout.ToString(), paramList, parameter);

            // 统计数目，需要判断是否开启分页，区分判断
            if (parameter.IsPage)
            {
                result.Total = EFUtility.GetCount(sqlLayout.ToString(), paramList);
            }
            else
            {
                result.Total = result.Rows.Count;
            }

            return result;
        }
    }
}
