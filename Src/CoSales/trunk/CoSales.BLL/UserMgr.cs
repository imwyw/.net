using CoSales.Common;
using CoSales.DAL;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoSales.BLL
{
    public class UserMgr
    {
        public static readonly UserMgr Mgr = new UserMgr();

        public int Add(User entity)
        {
            // 密码保存为密文，密码做加密处理
            entity.Password = EncryptHelper.MD5Encrypt(entity.Password);
            int res = UserDAO.DAO.Add(entity);
            return res;
        }

        public bool Update(User entity)
        {
            entity.Password = EncryptHelper.MD5Encrypt(entity.Password);
            int res = UserDAO.DAO.Update(entity);
            return res > 0;
        }

        public List<User> GetList(object whereConditions = null)
        {
            var res = UserDAO.DAO.GetList(whereConditions);
            return res;
        }

        /// <summary>
        /// 根据userid和password判断用户是否存在
        /// 使用cookie自动进行登录，密码为密文，不需要再次加密，所以需要进行判断
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <param name="isCipherPwd">是否为密文，密码默认为明文</param>
        /// <returns></returns>
        public User GetUser(string userid, string pwd, bool isCipherPwd = false)
        {
            //加密后进行判断。自动登录情况下cookie保存的就是加密后的密文
            string cipher = isCipherPwd ? pwd : EncryptHelper.MD5Encrypt(pwd);
            var res = UserDAO.DAO.GetUser(userid, cipher);
            return res;
        }
    }
}
