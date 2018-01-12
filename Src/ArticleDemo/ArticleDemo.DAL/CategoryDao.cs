using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.DAL
{
    public class CategoryDao
    {
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int Add(string name)
        {
            string sql = "INSERT INTO T_CATEGORY (NAME) VALUES (@NAME)";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",name)
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCategory()
        {
            string sql = "SELECT * FROM T_CATEGORY";
            DataTable dt = SqlHelper.FillTable(sql, null);
            return dt;
        }

        /// <summary>
        /// 根据id删除一个类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCategory(int id)
        {
            string sql = "DELETE FROM T_CATEGORY WHERE ID = @ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ID",id)
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 更新一个类别
        /// </summary>
        /// <param name="cate"></param>
        /// <returns></returns>
        public static int Update(Category cate)
        {
            string sql = "UPDATE T_CATEGORY SET NAME = @NAME WHERE ID = @ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@NAME",cate.Name),
                new SqlParameter("@ID",cate.ID)
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        public static Category GetCategoryByID(int id)
        {
            string sql = "SELECT * FROM T_CATEGORY WHERE ID = @ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ID",id)
            };
            Category res = SqlHelper.ExecuteReaderFirst<Category>(sql, sqlParams);
            return res;
        }

    }
}
