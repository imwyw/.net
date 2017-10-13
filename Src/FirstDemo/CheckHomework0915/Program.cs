using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckHomework0915
{
    class Program
    {

        static void Main(string[] args)
        {

            menu();
            operation();


        }





        static string Strcmp(params string[] args)
        {
            string res = string.Empty;
            foreach (var item in args)
            {
                res += item;
            }
            return res;
        }


        static void menu()
        {
            while (true)
            {
                Console.WriteLine("欢迎来到xxx员工管理系统系统");
                Console.WriteLine("请输入用户名:");
                string name = Console.ReadLine();
                Console.WriteLine("请输入密码");
                string pwd = Console.ReadLine();
                if (name == "admin" && pwd == "admin")
                {
                    Console.WriteLine("登录成功");
                    break;
                }
                else
                {
                    Console.WriteLine("用户名或密码错误，请重新输入！");
                }
            }
        }

        static void add(List<string> wname, List<int> salary, List<DateTime> birth, List<bool> wing)
        {
            Console.WriteLine("请输入员工姓名:");
            wname.Add(Console.ReadLine());
            Console.WriteLine("请输入薪资:");
            salary.Add(int.Parse(Console.ReadLine()));
            Console.WriteLine("请输入出生年月:");
            birth.Add(Convert.ToDateTime(Console.ReadLine()));
            Console.WriteLine("请输入是否在职:1.是   2.否");

            int temp = Convert.ToInt32(Console.ReadLine());
            if (temp == 1)
            {
                wing.Add(true);
            }
            else if (temp == 2)
            {
                wing.Add(false);
            }
        }

        static void display(List<string> wname, List<int> salary, List<DateTime> birth, List<bool> wing)
        {

            if (wname.Count != 0)
            {
                for (int i = 0; i < wname.Count; i++)
                {
                    if (!string.IsNullOrEmpty(wname[i]))
                    {
                        string res = Strcmp("员工姓名:", wname[i], "   ", "员工薪水:", Convert.ToString(salary[i]), "   ", "出生日期:", birth[i].ToString("yyyy-MM-dd"), "   ", "是否在职:", wing[i] ? "是" : "否");
                        Console.WriteLine(res);
                    }
                }
            }
            else
            {
                Console.WriteLine("当前无记录！");
            }
        }

        static void delete(ref List<string> wname, ref List<int> salary, ref List<DateTime> birth, ref List<bool> wing)
        {
            Console.WriteLine("请输入需要删除员工的的姓名");
            string ln = Console.ReadLine();
            if (wname.Contains(ln))
            {
                int index = wname.IndexOf(ln);
                wname.RemoveAt(index);
                salary.RemoveAt(index);
                birth.RemoveAt(index);
                wing.RemoveAt(index);
                Console.WriteLine("删除成功！");
            }
            else
            {
                Console.WriteLine("未找到该员工，删除失败！");
            }
        }
        static void operation()
        {
            List<string> wname = new List<string>();
            List<int> salary = new List<int>();
            List<DateTime> birth = new List<DateTime>();
            List<bool> wing = new List<bool>();

            string[] wingz = new string[100];
            while (true)
            {
                Console.WriteLine("1.添加员工");
                Console.WriteLine("2.查看员工资料");
                Console.WriteLine("3.删除员工资料");
                Console.WriteLine("0.退出系统");
                Console.WriteLine("请选择需要进行的操作:");
                int key = Convert.ToInt32(Console.ReadLine());

                switch (key)
                {
                    case 1:

                        add(wname, salary, birth, wing);


                        break;
                    case 2:

                        display(wname, salary, birth, wing);


                        break;
                    case 3:
                        delete(ref wname, ref salary, ref birth, ref wing);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("请按要求正确操作！");
                        break;
                }
            }
        }
    }
}
