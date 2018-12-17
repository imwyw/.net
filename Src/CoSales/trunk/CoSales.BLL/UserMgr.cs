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
            int res = UserDAO.DAO.Add(entity);
            return res;
        }

        public bool Update(User entity)
        {
            int res = UserDAO.DAO.Update(entity);
            return res > 0;
        }

        public List<User> GetList(object whereConditions = null)
        {
            var res = UserDAO.DAO.GetList(whereConditions);
            return res;
        }

        public User GetUser(string userid, string pwd)
        {
            var res = UserDAO.DAO.GetUser(userid, pwd);
            return res;
        }
    }
}
