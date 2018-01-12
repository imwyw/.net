using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// SQL尽量写在DAL层
/// </summary>
namespace ArticleDemo.DAL
{
    public class UserDao
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static int Add(User user)
        {
            string sql = "INSERT INTO T_USERS (ZH_NAME, NAME, PWD) VALUES (@ZH_NAME, @NAME, @PWD )";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ZH_NAME",user.Zh_Name),
                new SqlParameter("@NAME",user.Name),
                new SqlParameter("@PWD",user.Pwd)
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 校验是否有该用户，返回用户实体，不存在则返回null
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        public static User Login(string name, string pwd)
        {
            string sql = @"
                SELECT  A.ID ,
                        A.NAME ,
                        A.PWD ,
                        A.ZH_NAME ,
                        A.ROLES ,
                        R.ID ROLE_ID
                FROM    DBO.T_USERS A
                        LEFT JOIN DBO.T_ROLES_USERS RU ON A.ID = RU.USER_ID
                        LEFT JOIN DBO.T_ROLES R ON RU.ROLE_ID = R.ID                
                WHERE A.NAME = @NAME AND A.PWD = @PWD ";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",name),
                new SqlParameter("@PWD",pwd)
            };

            User user = SqlHelper.ExecuteReaderFirst<User>(sql, sqlParams);
            return user;
        }

        public static int CheckExist(string name)
        {
            string sql = "SELECT COUNT(1) FROM T_USERS WHERE NAME = @NAME ";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",name)
            };
            int res = SqlHelper.ExecuteScalar(sql, sqlParams);
            return res;
        }
    }
}
