using ArticleDemo.Common;
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
        /// 单元测试添加的方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Add(double x, double y)
        {
            return x + y;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(User user)
        {
            //添加用户时也做加密处理
            user.Pwd = EncryptHelper.MD5Encrypt(user.Pwd);
            int res = UserDao.Add(user);
            return res > 0;
        }

        /// <summary>
        /// 登录验证，是否有该用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User Login(string name, string pwd, bool isCipherPwd = false)
        {
            //加密后进行判断。自动登录情况下cookie保存的就是加密后的密码
            string cipher = isCipherPwd ? pwd : EncryptHelper.MD5Encrypt(pwd);
            User user = UserDao.Login(name, cipher);
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
