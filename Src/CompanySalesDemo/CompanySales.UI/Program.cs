using CompanySales.BLL;
using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用xxxx进销存管理系统");
            UserUI.LoginMenu();
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
                Console.WriteLine("3. 客户管理");
                Console.WriteLine("0. 退出");

                string input = string.Empty;
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        // 自上而下的思想，员工管理的二级菜单
                        EmployeeUI.EmployeeMenu();
                        break;
                    case "2":
                        break;
                    case "3":
                        CustomerUI.CustomerMenu();
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
    }
}
