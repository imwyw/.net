using ArticleDemo.DAL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.BLL
{
    public class UserMgr : IUserMgr
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(User user)
        {
            int res = UserDao.Add(user);
            return res > 0;
        }

        /// <summary>
        /// 登录验证，是否有该用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User Login(string name, string pwd)
        {
            User user = UserDao.Login(name, pwd);
            return user;
        }

        /// <summary>
        /// 检查该用户名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckExist(string name)
        {
            int res = UserDao.CheckExist(name);
            return res > 0;
        }

        /// <summary>
        /// 从控制台接收用户的输入，并返回一个用户实例
        /// </summary>
        /// <returns></returns>
        public static User GetUserFromConsole()
        {
            User entity = new User();

            Console.WriteLine("请输入用户名：");
            entity.Name = Console.ReadLine();
            Console.WriteLine("请输入密码：");
            entity.Pwd = Console.ReadLine();

            return entity;
        }
    }
}
