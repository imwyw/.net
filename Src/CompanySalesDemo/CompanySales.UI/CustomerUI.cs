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
    public class CustomerUI
    {
        public static void CustomerMenu()
        {
            Console.Clear();

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("请选择员工操作：");

                Console.WriteLine("1. 查看客户列表");
                Console.WriteLine("2. 添加客户");
                Console.WriteLine("3. 修改客户信息");
                Console.WriteLine("4. 删除客户");
                Console.WriteLine("0. 返回");

                string input = string.Empty;
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowCustomerList();
                        break;
                    case "2":
                        AddCustomer();
                        break;
                    case "3":
                        UpdateCustomer();
                        break;
                    case "4":
                        DeleteCustomer();
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

        private static void AddCustomer()
        {
            Console.WriteLine("添加员工，请依次输入客户相关信息！");

            Customer entity = GetCustomerByConsole();

            if (CustomerMgr.Add(entity))
            {
                Console.WriteLine("添加成功！");
                ShowCustomerList();
            }
            else
            {
                Console.WriteLine("未添加任何信息！");
            }
        }

        private static void UpdateCustomer()
        {
            Customer entity = GetCustomerByConsole(false);
            if (entity == null)
            {
                Console.WriteLine("无法完成更新操作！");
                return;
            }
            if (CustomerMgr.Update(entity))
            {
                Console.WriteLine("更新成功！");
                ShowCustomerList();
            }
            else
            {
                Console.WriteLine("无法更新，请检查！");
            }
        }

        private static void DeleteCustomer()
        {
            ShowCustomerList();
            int id = ConvertHelper.CheckConsoleInput<int>("请输入要删除的客户编号：");
            if (CustomerMgr.Delete(id))
            {
                Console.WriteLine("删除成功！");
            }
            else
            {
                Console.WriteLine("无法删除，请查看日志。。。");
            }
        }

        private static void ShowCustomerList()
        {
            DataTable dt = CustomerMgr.GetCustomerData();
            Console.WriteLine("编号\t公司名称\t联系人\t电话\t地址\t邮箱");
            foreach (DataRow item in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Console.Write(item[i] + "\t");
                }
                Console.WriteLine();
            }
        }

        static Customer GetCustomerByConsole(bool isAdd = true)
        {
            Customer entity = new Customer();

            string msg = string.Empty;
            if (isAdd)
            {
                msg = "请输入新增客户的编号：";
            }
            else
            {
                ShowCustomerList();
                msg = "请输入需要修改客户的编号";
            }
            entity.CustomerId = ConvertHelper.CheckConsoleInput<int>(msg);

            // 当为更新操作的时候，需要校验编号是否存在，如果存在显示该实体信息
            if (!isAdd)
            {
                Customer oldCus = CustomerMgr.GetCustomerById(entity.CustomerId);
                if (oldCus == null)
                {
                    Console.WriteLine("该编号的客户不存在，请重新选择！");
                    return null;
                }
                else
                {
                    Console.WriteLine($"需要修改客户的基本信息：客户名称:{oldCus.CustomerId},公司名称:{oldCus.CompanyName}");
                }
            }

            Console.WriteLine("请输入客户公司名称：");
            entity.CompanyName = Console.ReadLine();

            Console.WriteLine("请输入客户联系人姓名：");
            entity.ContactName = Console.ReadLine();

            Console.WriteLine("请输入客户联系电话：");
            entity.Phone = Console.ReadLine();

            Console.WriteLine("请输入客户联系地址：");
            entity.Address = Console.ReadLine();

            Console.WriteLine("请输入客户联系邮箱地址：");
            entity.EmailAddress = Console.ReadLine();

            return entity;
        }
    }
}
