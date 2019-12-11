using CompanySales.Model.Entity;using CompanySales.Model;
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
            string sql = @"
INSERT  INTO dbo.Employee
        ( EmployeeID ,
          EmployeeName ,
          Sex ,
          BirthDate ,
          HireDate ,
          Salary ,
          DepartmentID
        )
VALUES  ( @EmployeeID ,
          @EmployeeName ,
          @Sex ,
          @BirthDate ,
          @HireDate ,
          @Salary ,
          @DepartmentID  
        )";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmployeeID",emp.EmployeeID),
                new SqlParameter("@EmployeeName",emp.EmployeeName),
                new SqlParameter("@Sex",emp.Sex),
                new SqlParameter("@BirthDate",emp.BirthDate),
                new SqlParameter("@HireDate",emp.HireDate),
                new SqlParameter("@Salary",emp.Salary),
                new SqlParameter("@DepartmentID",emp.DepartmentID),
            };

            int res = MySqlHelper.ExecuteMyDML(sql, sqlParams);
            return res > 0;
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

        public static bool Delete(int id)
        {
            string sql = @"
DELETE FROM dbo.Employee WHERE EmployeeID = @EmployeeID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmployeeID",id),
            };

            int res = MySqlHelper.ExecuteMyDML(sql, sqlParams);
            return res > 0;
        }

        public static bool Update(Employee emp)
        {
            string sql = @"
UPDATE  dbo.Employee
SET     EmployeeName = @EmployeeName ,
        Sex = @Sex ,
        BirthDate = @BirthDate ,
        HireDate = @HireDate ,
        Salary = @Salary ,
        DepartmentID = @DepartmentID
WHERE   EmployeeID = @EmployeeID";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmployeeName",emp.EmployeeName),
                new SqlParameter("@Sex",emp.Sex),
                new SqlParameter("@BirthDate",emp.BirthDate),
                new SqlParameter("@HireDate",emp.HireDate),
                new SqlParameter("@Salary",emp.Salary),
                new SqlParameter("@DepartmentID",emp.DepartmentID),
                new SqlParameter("@EmployeeID",emp.EmployeeID),
            };

            int res = MySqlHelper.ExecuteMyDML(sql, sqlParams);
            return res > 0;
        }

        public static Employee GetEmployeeById(int id)
        {
            string sql = @"
SELECT  EmployeeID ,
        EmployeeName ,
        Sex ,
        CONVERT(VARCHAR, BirthDate, 111) BirthDate ,
        CONVERT(VARCHAR, HireDate, 111) HireDate ,
        CAST(Salary AS DECIMAL(10, 1)) Salary ,
        DepartmentID
FROM    dbo.Employee
WHERE EmployeeID = @EmployeeID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmployeeID",id),
            };

            Employee emp = MySqlHelper.ExecuteReaderFirst<Employee>(sql, sqlParams);
            return emp;
        }
    }
}
