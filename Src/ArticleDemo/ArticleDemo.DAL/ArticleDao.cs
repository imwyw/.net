using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.DAL
{
    public class ArticleDao
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int Add(Article entity)
        {
            string sql = @"INSERT INTO T_ARTICLES(CATE_ID, TITLE, CONTENT, UPDATE_TIME, CREATE_USER) 
                            VALUES (@CATE_ID, @TITLE, @CONTENT, @UPDATE_TIME, @CREATE_TIME)";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CATE_ID",entity.Cate_id),
                new SqlParameter("@TITLE",entity.Title),
                new SqlParameter("@CONTENT",entity.Content),
                new SqlParameter("@UPDATE_TIME",entity.Update_Time),
                new SqlParameter("@CREATE_TIME",entity.Create_User)
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 根据id进行删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Remove(int id)
        {
            string sql = "DELETE FROM T_ARTICLES WHERE ID = @ID";
            SqlParameter[] sqlParams = { new SqlParameter("@ID", id) };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int Update(Article entity)
        {
            string sql = @"UPDATE T_ARTICLES SET CATE_ID=@CATE_ID, TITLE=@TITLE, CONTENT=@CONTENT, 
                            UPDATE_TIME=@UPDATE_TIME, CREATE_USER=@CREATE_USER
                            WHERE ID=@ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ID",entity.ID),
                new SqlParameter("@CATE_ID",entity.Cate_id),
                new SqlParameter("@TITLE",entity.Title),
                new SqlParameter("@CONTENT",entity.Content),
                new SqlParameter("@UPDATE_TIME",entity.Update_Time),
                new SqlParameter("@CREATE_USER",entity.Create_User),
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 查询返回文章列表
        /// </summary>
        /// <returns></returns>
        public static List<Article> GetArticles()
        {
            string sql = @"SELECT * FROM V_GET_ARTICLES";
            List<Article> lstResult = SqlHelper.ExecuteReader<Article>(sql, null);
            return lstResult;
        }
    }
}
