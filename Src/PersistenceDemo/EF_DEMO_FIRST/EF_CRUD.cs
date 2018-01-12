using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DEMO_FIRST
{
    class EF_CRUD
    {
        public static void Add()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                t_category obj = new t_category();
                obj.name = "寒冬腊月";
                context.t_category.Add(obj);

                var res = context.SaveChanges();
            }
        }

        public static void Update()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                //context.t_category.First(t => t.name == "show");
                var cate = context.t_category.FirstOrDefault(t => t.name == "寒冬腊月");
                cate.name = "九月九";
                context.SaveChanges();
            }
        }

        public static void Delete()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                var cate = context.t_category.FirstOrDefault(t => t.name == "九月九");
                if (null != cate)
                {
                    context.t_category.Remove(cate);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateEx()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                var time = new SqlParameter("@TITLE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = context.Database.ExecuteSqlCommand("UPDATE t_articles SET title = @TITLE WHERE CATE_ID = 0", time);
            }
        }

        public static void SqlQuery()
        {
            //实例化EMD对象ARTICLE_DBEntities，数据库上下文
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                /*
                按条件返回集合，以下两种方式均可。
                注意，如果没有ToList()，查询并不会执行
                */
                var list = context.Database.SqlQuery<t_users>("SELECT * FROM T_USERS").ToList();

                /*
                按条件查询返回实体，如果没有符合条件的数据，则返回为null
                注意，如果没有FirstOrDefault()的调用，查询并不会执行
                */
                var res1 = context.Database.SqlQuery<t_users>("SELECT  * FROM T_USERS WHERE NAME = 'www'").FirstOrDefault();

                /*
                统计数目
                注意，如果没有FirstOrDefault()的调用，查询并不会执行
                */
                var result2 = context.Database.SqlQuery<int>("SELECT  COUNT(*) FROM T_USERS");
                Console.WriteLine(result2.FirstOrDefault());

                /*
                按条件返回自定义对象
                注意，如果没有FirstOrDefault()的调用，查询并不会执行
                */
                var result3 = context.Database.SqlQuery<SimpleUser>("SELECT NAME,PWD FROM T_USERS").ToList();
            }

        }
    }
    public class SimpleUser
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}
