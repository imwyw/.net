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

        /// <summary>
        /// 更新用户基本信息，但不包含密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int? UpdateInfo(User entity)
        {
            string sql = @"
UPDATE T_USER SET UserName=@UserName
,Gender=@Gender
,BirthDate=@BirthDate
,RoleID=@RoleID
,Remark=@Remark
,HeadImg=@HeadImg
WHERE ID=@ID
";
            return DapperHelper.Excute(sql, entity);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int? UpdateImage(User entity)
        {
            string sql = @"UPDATE T_USER SET HeadImg=@HeadImg WHERE ID=@ID";
            return DapperHelper.Excute(sql, entity);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int? UpdatePwd(User entity)
        {
            string sql = @"UPDATE T_USER SET Password=@Password WHERE ID=@ID";
            return DapperHelper.Excute(sql, entity);
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

        public User GetUser(int id)
        {
            var res = DapperHelper.Get<User>(id);
            return res;
        }
    }
}
