using CompanySales.BLL;
using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UI
{
    public class EmployeeUI
    {
        public static void EmployeeMenu()
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

            if (EmployeeMgr.Add(emp))
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
            Employee emp = GetEmployeeByConsole(false);
            if (emp == null)
            {
                Console.WriteLine("无法完成更新操作！");
                return;
            }
            if (EmployeeMgr.Update(emp))
            {
                Console.WriteLine("更新成功！");
                ShowEmpList();
            }
            else
            {
                Console.WriteLine("无法更新，请检查！");
            }
        }

        private static void DeleteEmp()
        {
            ShowEmpList();
            int id = ConvertHelper.CheckConsoleInput<int>("请输入要删除的员工编号：");
            if (EmployeeMgr.Delete(id))
            {
                Console.WriteLine("删除成功！");
            }
            else
            {
                Console.WriteLine("无法删除，请查看日志。。。");
            }
        }

        private static void ShowEmpList()
        {
            DataTable dt = EmployeeMgr.GetEmployeeData();
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

        /// <summary>
        /// 通过控制台的输入，获得一个员工对象
        /// </summary>
        /// <returns></returns>
        static Employee GetEmployeeByConsole(bool isAdd = true)
        {
            Employee emp = new Employee();

            string msg = string.Empty;
            if (isAdd)
            {
                msg = "请输入新增员工的编号：";
            }
            else
            {
                ShowEmpList();
                msg = "请输入需要修改员工的编号";
            }
            emp.EmployeeID = ConvertHelper.CheckConsoleInput<int>(msg);

            // 当为更新操作的时候，需要校验编号是否存在，如果存在显示该实体信息
            if (!isAdd)
            {
                Employee oldEmp = EmployeeMgr.GetEmployeeById(emp.EmployeeID);
                if (oldEmp == null)
                {
                    Console.WriteLine("该编号的员工不存在，请重新选择！");
                    return null;
                }
                else
                {
                    Console.WriteLine($"需要修改员工的基本信息：EmployeeID:{oldEmp.EmployeeID},EmployeeName:{oldEmp.EmployeeName}");
                }
            }

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

            emp.BirthDate = ConvertHelper.CheckConsoleInput<DateTime>("请输入员工生日（yyyy-mm-dd）：");
            emp.HireDate = ConvertHelper.CheckConsoleInput<DateTime>("请输入员工入职日期（yyyy-mm-dd）：");

            emp.Salary = ConvertHelper.CheckConsoleInput<decimal>("请输入员工薪水：");

            emp.DepartmentID = ConvertHelper.CheckConsoleInput<int>("请输入员工部门编号：");

            return emp;
        }
    }
}
