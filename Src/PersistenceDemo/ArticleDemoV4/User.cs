using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV4
{
    public class User
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 登录ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 从控制台接收用户的输入，并返回一个用户实例
        /// </summary>
        /// <returns></returns>
        public static User GetUserFromConsole()
        {
            Console.WriteLine("请输入用户名：");
            string name = Console.ReadLine();
            Console.WriteLine("请输入密码：");
            string pwd = Console.ReadLine();

            User u = new User() { ID = name, Pwd = pwd };
            return u;
        }

        /// <summary>
        /// 检验数组中是否有该用户，匹配用户id和密码
        /// </summary>
        /// <param name="lstUser"></param>
        /// <param name="usr"></param>
        /// <returns></returns>
        public static bool CheckExist(List<User> lstUser, User usr)
        {
            foreach (User item in lstUser)
            {
                if (item.ID == usr.ID && item.Pwd == usr.Pwd)
                {
                    usr.Name = item.Name;
                    return true;
                }
            }
            return false;
        }


        public static bool CheckExistByID(List<User> lstUser, User usr)
        {
            foreach (User item in lstUser)
            {
                if (item.ID == usr.ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
