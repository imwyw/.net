using CompanySales.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanySales.Repository.DAO
{
    public class UserDAO
    {
        public static User Login(string uid, string pwd)
        {
            using (SaleContext db = new SaleContext())
            {
                var res = db.Users
                    .Where(t => t.UserId == uid && t.Password == pwd)
                    .FirstOrDefault();
                return res;
            }
        }
    }
}
