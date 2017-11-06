using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDao
    {
        public static bool AddUser(User entity)
        {
            string sql = "INSERT INTO T_USERS (NAME, PWD) VALUES (@NAME, @PWD)";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",entity.Name),
                new SqlParameter("@PWD",entity.Pwd),
            };
            int res = MySqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res > 0;
        }

        public static User GetUser(string name, string pwd)
        {
            string sql = "SELECT ID, NAME, PWD, ZH_NAME FROM T_USERS WHERE NAME=@NAME AND PWD=@PWD";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",name),
                new SqlParameter("@PWD",pwd),
            };
            User res = MySqlHelper.ExecuteReaderFirst<User>(sql, sqlParams);
            return res;
        }
    }
}
