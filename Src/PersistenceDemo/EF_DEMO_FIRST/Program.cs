using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DEMO_FIRST
{
    class Program
    {
        static void Main(string[] args)
        {
            //EntityClient();
            Console.WriteLine("===============================");
            //ObjectContext();

            //SimpleLinqDemo();

            //LinqToObject();

            /*
            EF CRUD方法
            */
            EF_CRUD.Add();
            //EF_CRUD.Update();
            //EF_CRUD.Delete();
            //EF_CRUD.UpdateEx();
            //EF_CRUD.SqlQuery();
        }

        //Entity Client 相当于实体数据库的操作
        static void EntityClient()
        {
            // 创建并打开连接
            EntityConnection conn = new EntityConnection();

            //ARTICLE_DBEntities 对应app.config中的配置
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ARTICLE_DBEntities"].ConnectionString;
            conn.Open();
            // 创建并执行命令
            EntityCommand cmd = new EntityCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            /*
            注意此处特别！！！注意以下两点：
            1、不支持直接使用 *
            2、表名前需要添加EDM名称，此处是 ARTICLE_DBEntities
            */
            cmd.CommandText = "SELECT T.ID ,T.ZH_NAME ,T.NAME ,T.PWD ,T.ROLES FROM ARTICLE_DBENTITIES.T_USERS AS T";
            EntityDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            // 输出
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i].ToString() + "\t");
                }
                Console.WriteLine();
            }
            // 关闭连接
            conn.Close();
        }

        //object操作
        static void ObjectContext()
        {
            //实例化EMD对象ARTICLE_DBEntities，数据库上下文
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                /*
                创建一个查询 lambda表达式的方式。
                可以理解为是一个SQL的封装，并没有执行查询
                */
                var query = context.v_get_articles
                    .Where(t => t.cate_id == 2)
                    .Select(t =>
                        new
                        {
                            uname = t.user_name,
                            uid = t.id,
                            utitle = t.title,
                            ucontent = t.content
                        }
                    );

                // ToList 将查询执行，并返回集合
                var res1 = context.t_users.Where(t => t.name == "w").ToList();

                //遍历打印 才会调用query查询
                foreach (var item in query)
                {
                    //Console.WriteLine(item.title + "\t" + item.content);
                    Console.WriteLine(item);
                }
            }
        }

        static void SimpleLinqDemo()
        {
            // The Three Parts of a LINQ Query:
            //  1. Data source. 数据源
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.创建查询
            // numQuery is an IEnumerable<int>
            var numQuery =
                from t in numbers
                where (t % 2) == 0
                select t;

            // 3. Query execution.执行查询
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }

        //linq 方式
        static void LinqToObject()
        {
            //实例化EMD对象ARTICLE_DBEntities，数据库上下文
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                /* 创建一个查询 linq方式   
                */
                var query = from t in context.v_get_articles
                            where t.cate_id == 2
                            select new { a = t.title, b = t.content, c = t.user_name };

                //在调用时，才会真正的执行该查询，即从数据库进行实际查询操作
                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
