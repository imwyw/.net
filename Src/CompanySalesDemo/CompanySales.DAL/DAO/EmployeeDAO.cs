using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class EmployeeDAO
    {
        /// <summary>
        /// 添加员工操作
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public static bool Add(Employee emp)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Employee.Add(emp);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 获取员工列表数据表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmployeeData()
        {
            string sql = @"
SELECT  EmployeeID ,
        EmployeeName ,
        Sex ,
        CONVERT(VARCHAR, BirthDate, 111) BirthDate ,
        CONVERT(VARCHAR, HireDate, 111) HireDate ,
        CAST(Salary AS DECIMAL(10, 1)) Salary ,
        DepartmentID
FROM    dbo.Employee;";
            DataTable dt = MySqlHelper.QueryData(sql);
            return dt;
        }

        public static List<Employee> GetList()
        {
            using (SaleContext db = new SaleContext())
            {
                return db.Employee.ToList();
            }
        }

        public static bool Delete(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Employee.Find(id);
                db.Employee.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        public static bool Update(Employee emp)
        {
            using (SaleContext db = new SaleContext())
            {
                var dbEntity = db.Employee.Find(emp.EmployeeID);
                dbEntity.BirthDate = emp.BirthDate;
                dbEntity.DepartmentID = emp.DepartmentID;
                dbEntity.EmployeeName = emp.EmployeeName;
                dbEntity.HireDate = emp.HireDate;
                dbEntity.Salary = emp.Salary;
                dbEntity.Sex = emp.Sex;

                db.SaveChanges();
                return true;
            }
        }

        public static Employee GetEmployeeById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Employee.Find(id);
                return entity;
            }
        }
    }
}
