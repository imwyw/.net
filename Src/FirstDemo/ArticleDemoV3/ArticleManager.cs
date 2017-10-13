using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV3
{
    class ArticleManager
    {
        /// <summary>
        /// 添加文章到集合中
        /// </summary>
        /// <param name="lst"></param>
        public static void AddArticle(List<Article> lst)
        {
            Article entity = new Article();
            Console.WriteLine("请输入文章标题：");
            entity.Title = Console.ReadLine();

            Console.WriteLine("请输入文章内容：");
            entity.Content = Console.ReadLine();

            entity.UpdateTime = DateTime.Now;

            lst.Add(entity);
        }

        /// <summary>
        /// 迭代器循环显示文章详情
        /// </summary>
        /// <param name="lst"></param>
        public static void ShowArticle(List<Article> lst)
        {
            foreach (Article item in lst)
            {
                string tipMsg = "文章标题：" + item.Title + "，文章内容：" + item.Content + "，更新时间：" + item.UpdateTime;
                Console.WriteLine(tipMsg);
            }
        }

        /// <summary>
        /// 根据文章标题移除文章
        /// </summary>
        /// <param name="lst"></param>
        public static void RemoveArticle(List<Article> lst)
        {
            Console.WriteLine("请输入需要删除的文章标题：");
            string title = Console.ReadLine();

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Title == title)
                {
                    lst.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
