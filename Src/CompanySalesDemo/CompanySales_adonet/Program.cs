using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales_adonet
{
    class Program
    {
        static readonly string CONNSTR = "server=.;database=companysales;uid=sa;pwd=123456;";

        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用xxxx进销存管理系统");
            ShowMenu();
        }

        static void ShowMenu()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("请选择操作：");

                Console.WriteLine("1. 员工管理");
                Console.WriteLine("2. 部门管理");
                Console.WriteLine("0. 退出");

                string input = string.Empty;
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        // 自上而下的思想，员工管理的二级菜单
                        EmployeeMenu();
                        break;
                    case "2":
                        break;
                    case "0":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("请输入有效选项！");
                        break;
                }
            }


        }

        private static void EmployeeMenu()
        {
            Console.Clear();

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("请选择员工操作：");

                Console.WriteLine("1. 查看员工列表");
                Console.WriteLine("2. 添加员工");
                Console.WriteLine("3. 修改员工");
                Console.WriteLine("4. 删除员工");
                Console.WriteLine("0. 返回");

                string input = string.Empty;
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowEmpList();
                        break;
                    case "2":
                        AddEmp();
                        break;
                    case "3":
                        UpdateEmp();
                        break;
                    case "4":
                        DeleteEmp();
                        break;
                    case "0":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("请输入有效选项！");
                        break;
                }
            }


        }

        private static void AddEmp()
        {
            Console.WriteLine("添加员工，请依次输入员工相关信息！");

            Employee emp = GetEmployeeByConsole();

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
            int res = ExecuteMyDML(sql, sqlParams);
            if (res > 0)
            {
                Console.WriteLine("添加成功！");
                ShowEmpList();
            }
            else
            {
                Console.WriteLine("未添加任何信息！");
            }
        }

        private static void UpdateEmp()
        {

            throw new NotImplementedException();
        }

        private static void DeleteEmp()
        {

            throw new NotImplementedException();
        }

        private static void ShowEmpList()
        {
            SqlConnection conn = new SqlConnection(CONNSTR);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"
SELECT  EmployeeID ,
        EmployeeName ,
        Sex ,
        CONVERT(VARCHAR, BirthDate, 111) BirthDate ,
        CONVERT(VARCHAR, HireDate, 111) HireDate ,
        CAST(Salary AS DECIMAL(10, 1)) Salary ,
        DepartmentID
FROM    dbo.Employee;";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Console.WriteLine("编号\t姓名\t性别\t出生日期\t入职日期\t薪水\t部门");
                foreach (DataRow item in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Console.Write(item[i] + "\t");
                    }
                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 通过控制台的输入，获得一个员工对象
        /// </summary>
        /// <returns></returns>
        static Employee GetEmployeeByConsole()
        {
            Employee emp = new Employee();

            Console.WriteLine("请输入员工编号：");
            emp.EmployeeID = int.Parse(Console.ReadLine());

            Console.WriteLine("请输入员工姓名：");
            emp.EmployeeName = Console.ReadLine();

            Console.WriteLine("请输入员工性别（1-男，2-女）：");
            if (Console.ReadLine() == "1")
            {
                emp.Sex = "男";
            }
            else
            {
                emp.Sex = "女";
            }

            emp.BirthDate = CheckConsoleInput<DateTime>("请输入员工生日（yyyy-mm-dd）：");
            emp.HireDate = CheckConsoleInput<DateTime>("请输入员工入职日期（yyyy-mm-dd）：");

            emp.Salary = CheckConsoleInput<double>("请输入员工薪水：");

            emp.DepartmentID = CheckConsoleInput<int>("请输入员工部门编号：");

            return emp;
        }

        /// <summary>
        /// 进行输入的安全转换
        /// 反射匹配方法
        /// 调用反射方法
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tipMessage"></param>
        /// <returns></returns>
        static T CheckConsoleInput<T>(string tipMessage) where T : struct
        {
            while (true)
            {
                Console.WriteLine(tipMessage);
                string input = Console.ReadLine();

                /* 反射的方式，找到泛型参数T的TryParse方法
                第一个参数（string）：方法的名字
                第二个参数（Type[]）：如有方法重载，需要指定对应的形参类型
                */
                MethodInfo parseMethod = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });

                if (null != parseMethod)
                {
                    // 反射得到方法后，进行invoke调用
                    object[] args = new object[] { input, null };
                    bool success = (bool)parseMethod.Invoke(null, args);
                    if (success)
                    {
                        return (T)args[1];
                    }
                    else
                    {
                        Console.WriteLine("输入的数据格式不正确，请重新输!");
                    }
                }
                else
                {
                    Console.WriteLine("不支持该数据类型的转换!!!");
                }
            }
        }

        /*
        static DateTime CheckConsoleInput(string tipMessage)
        {
            while (true)
            {
                Console.WriteLine(tipMessage);
                string input = Console.ReadLine();
                DateTime result;
                bool success = DateTime.TryParse(input, out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("输入的格式不正确！！");
                }
            }
        }

        static double CheckConsoleInputDouble(string tipMessage)
        {
            while (true)
            {
                Console.WriteLine(tipMessage);
                string input = Console.ReadLine();
                double result;
                bool success = double.TryParse(input, out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("输入的格式不正确！！");
                }
            }
        }
        */

        /// <summary>
        /// 执行DML（增删改）的SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">带有占位符的SQL</param>
        /// <param name="sqlParams">参数化查询的数组</param>
        /// <returns></returns>
        static int ExecuteMyDML(string sql, SqlParameter[] sqlParams)
        {
            SqlConnection conn = new SqlConnection(CONNSTR);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                if (null != sqlParams && sqlParams.Length > 0)
                {
                    cmd.Parameters.AddRange(sqlParams);
                }

                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }
    }
}
