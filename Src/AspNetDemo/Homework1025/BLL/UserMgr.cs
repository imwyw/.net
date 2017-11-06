using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserMgr
    {
        public static bool AddUser(User entity)
        {
            return UserDao.AddUser(entity);
        }

        public static User GetUser(string name, string pwd)
        {
            return UserDao.GetUser(name, pwd);
        }
    }
}
