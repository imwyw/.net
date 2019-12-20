using CompanySales.BLL;
using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UI
{
    public class UserUI
    {
        /// <summary>
        /// 用户安全操作相关操作菜单
        /// </summary>
        public static void LoginMenu()
        {
            while (true)
            {
                Console.WriteLine("请输入对应操作：");
                Console.WriteLine("1-登录(默认)");
                Console.WriteLine("2-注册");

                string choice = Console.ReadLine();
                if (choice == "2")
                {
                    Console.WriteLine("****** 注册用户 ******");
                    User user = GetUserFromConsole();
                    bool addSuccess = UserMgr.AddUser(user);
                }
                else
                {
                    Console.WriteLine("****** 登录 ******");
                    Console.Write("用户名：");
                    string uid = Console.ReadLine();
                    Console.Write("密码：");
                    string pwd = Console.ReadLine();

                    bool loginSuccess = UserMgr.Login(uid, pwd) != null;
                    if (loginSuccess)
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("用户名或密码错误，请重新登录！！！");
                    }
                }
            }
        }

        /// <summary>
        /// 通过控制台输入获取用户对象
        /// </summary>
        /// <returns></returns>
        public static User GetUserFromConsole()
        {
            User user = new User();

            Console.WriteLine("请输入用户名：");
            user.Name = Console.ReadLine();

            Console.WriteLine("请输入用户登录id：");
            user.UserId = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("请输入用户密码：");
                string pwd1 = Console.ReadLine();

                Console.WriteLine("请输入确认密码：");
                string pwd2 = Console.ReadLine();
                if (pwd1.Equals(pwd2))
                {
                    user.Password = pwd1;
                    break;
                }
                else
                {
                    Console.WriteLine("两次密码输入不一致，请重新录入密码！");
                }
            }

            Console.WriteLine("请输入用户地址：");
            user.Address = Console.ReadLine();

            return user;
        }
    }
}
