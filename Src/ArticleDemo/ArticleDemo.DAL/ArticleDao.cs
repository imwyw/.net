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
                            VALUES (@CATE_ID, @TITLE, @CONTENT, @UPDATE_TIME, @CREATE_USER)";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CATE_ID",entity.Cate_id),
                new SqlParameter("@TITLE",entity.Title),
                new SqlParameter("@CONTENT",entity.Content),
                new SqlParameter("@UPDATE_TIME",entity.Update_Time),
                new SqlParameter("@CREATE_USER",entity.Create_User)
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

        public static Article GetAritlceByID(int id)
        {
            string sql = @"SELECT * FROM V_GET_ARTICLES WHERE ID = @ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ID",id)
            };

            Article res = SqlHelper.ExecuteReaderFirst<Article>(sql, sqlParams);
            return res;
        }

        #region EF操作
        /// <summary>
        /// EF方式操作返回所有数据
        /// </summary>
        /// <param name="cateid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static List<v_get_articles> GetArticlesEF(int cateid, string title)
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                //动态拼接多条件
                var res = context.v_get_articles.Where(t => true);
                if (cateid != -1)
                {
                    res = res.Where(t => t.cate_id == cateid);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    res = res.Where(t => t.title.Contains(title));
                }

                //tolist是才会执行前面拼接的查询计划
                return res.ToList();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="cateid"></param>
        /// <param name="title"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Pager<v_get_articles> GetArticlesByPager(int cateid, string title, PageParam page)
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                Pager<v_get_articles> pg = new Pager<v_get_articles>();
                var res = context.v_get_articles.Where(t => true);
                if (cateid != -1)
                {
                    res = res.Where(t => t.cate_id == cateid);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    // contains -> like
                    res = res.Where(t => t.title.Contains(title));
                }
                pg.Total = res.Count();
                pg.Rows = res.OrderBy(t => t.update_time).Skip(page.Skip).Take(page.PageSize).ToList();

                return pg;
            }
        }
        #endregion
    }
}
