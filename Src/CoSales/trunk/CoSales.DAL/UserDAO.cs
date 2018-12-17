using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.DAL
{
    public class UserDAO
    {
        public static readonly UserDAO DAO = new UserDAO();

        public int Add(User entity)
        {
            return DapperHelper.Insert(entity);
        }

        public int Update(User entity)
        {
            return DapperHelper.Update(entity);
        }

        public List<User> GetList(object whereConditions = null)
        {
            return DapperHelper.GetList<User>(whereConditions).ToList();
        }

        public User GetUser(string userid, string pwd)
        {
            var res = DapperHelper.GetList<User>(new { UserID = userid, Password = pwd }).FirstOrDefault();
            return res;
        }
    }
}
