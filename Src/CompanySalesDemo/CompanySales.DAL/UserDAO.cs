using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class UserDAO
    {
        public static User Login(string uid, string pwd)
        {
            using (SaleContext db = new SaleContext())
            {
                var res = db.Users
                    .Where(t => t.UserId == uid && t.Password == pwd)
                    .First();
                return res;
            }
        }

        public static bool AddUser(User user)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
        }

        public static bool AddBatchUser(List<User> list)
        {
            using (SaleContext db = new SaleContext())
            {
                if (null == list || list.Count == 0)
                {
                    return false;
                }
                db.Users.AddRange(list);
                db.SaveChanges();
                return true;
            }
        }

    }
}
