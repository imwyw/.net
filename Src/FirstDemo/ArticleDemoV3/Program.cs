using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV3
{
    class Program
    {
        //保存文章实体的泛型集合
        static List<Article> lstArticle = new List<Article>();

        static void Main(string[] args)
        {
            Login();

            SwitchOperate();
        }

        /// <summary>
        /// 登录
        /// </summary>
        static void Login()
        {
            while (true)
            {
                Console.WriteLine("欢迎使用xxx文章发布系统");
                Console.WriteLine("请输入用户名：");
                string name = Console.ReadLine();
                Console.WriteLine("请输入密码：");
                string pwd = Console.ReadLine();

                if (name == "admin" && pwd == "admin")
                {
                    Console.WriteLine("恭喜");
                    break;
                }
                else
                {
                    Console.WriteLine("用户名密码错误，请重新输入");
                }
            }
        }

        /// <summary>
        /// 操作
        /// </summary>
        static void SwitchOperate()
        {
            while (true)
            {
                Console.WriteLine("请选择对应的操作：");
                Console.WriteLine("1.发布文章");
                //英文状态下，快捷键呼出智能提示ctrl+j
                Console.WriteLine("2.查看文章");
                Console.WriteLine("3.删除文章");
                Console.WriteLine("0.退出");

                string keyCode = Console.ReadLine();
                switch (keyCode)
                {
                    case "1":
                        ArticleManager.AddArticle(lstArticle);
                        break;
                    case "2":
                        ArticleManager.ShowArticle(lstArticle);
                        break;
                    case "3":
                        ArticleManager.RemoveArticle(lstArticle);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("输入有误");
                        break;
                }
            }
        }
    }
}
