using ArticleDemo.DAL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.BLL
{
    public class ArticleMgr
    {
        public static bool Add(Article entity)
        {
            int res = ArticleDao.Add(entity);
            return res > 0;
        }

        public static bool Remove(int id)
        {
            int res = ArticleDao.Remove(id);
            return res > 0;
        }

        public static bool Update(Article entity)
        {
            int res = ArticleDao.Update(entity);
            return res > 0;
        }

        public static List<Article> GetArticles()
        {
            List<Article> lstRes = ArticleDao.GetArticles();
            return lstRes;
        }

        public static Article GetArticleByID(int id)
        {
            return ArticleDao.GetAritlceByID(id);
        }

        private static Article GetArticleInfoConsole()
        {
            Article entity = new Article();
            CategoryMgr.ShowCategoryConsole();
            Console.WriteLine("请选择文章类别：");
            entity.Cate_id = int.Parse(Console.ReadLine());

            Console.WriteLine("请输入文章标题：");
            entity.Title = Console.ReadLine();

            Console.WriteLine("请输入文章内容：");
            entity.Content = Console.ReadLine();

            entity.Update_Time = DateTime.Now;
            entity.Create_User = GlobalData.CurrentUser.ID;
            return entity;
        }

        /// <summary>
        /// 控制台添加文章
        /// </summary>
        public static void AddConsole()
        {
            Article entity = GetArticleInfoConsole();
            if (Add(entity))
            {
                Console.WriteLine("发布成功！");
                ShowConsole();
            }
            else
            {
                Console.WriteLine("发生异常");
            }
        }

        /// <summary>
        /// 控制台更新文章
        /// </summary>
        public static void UpdateConsole()
        {
            Console.WriteLine("请选择需要修改的文章ID");
            ShowConsole();
            int id = int.Parse(Console.ReadLine());
            Article entity = GetArticleInfoConsole();
            entity.ID = id;
            if (Update(entity))
            {
                Console.WriteLine("更新成功！");
                ShowConsole();
            }
            else
            {
                Console.WriteLine("发生异常");
            }
        }

        /// <summary>
        /// 控制台显示所有文章
        /// </summary>
        public static void ShowConsole()
        {
            Console.WriteLine("*********文章信息*********");
            Console.WriteLine("文章ID\t文章类别\t文章标题\t文章内容\t更新时间\t作者");
            List<Article> lstRes = GetArticles();
            foreach (Article art in lstRes)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", art.ID, art.Cate_Name, art.Title, art.Content, art.Update_Time, art.User_Name);
            }
        }

        /// <summary>
        /// 控制台根据id删除文章
        /// </summary>
        public static void RemoveConsole()
        {
            ShowConsole();
            Console.WriteLine("请输入需要删除的文章ID：");
            int id = int.Parse(Console.ReadLine());
            if (Remove(id))
            {
                Console.WriteLine("删除成功");
            }
            else
            {
                Console.WriteLine("删除失败");
            }
        }

        #region
        public static List<v_get_articles> GetArticlesEF(int cateid, string title)
        {
            return ArticleDao.GetArticlesEF(cateid, title);
        }

        public static Pager<v_get_articles> GetArticlesByPager(int cateid, string title, PageParam page)
        {
            return ArticleDao.GetArticlesByPager(cateid, title, page);
        }
        #endregion
    }
}
