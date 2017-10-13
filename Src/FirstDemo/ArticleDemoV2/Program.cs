/*
利用泛型集合设计一个文章发布系统，包含以下功能
内置数据类型、分支、循环等代码结构、泛型集合的的定义及使用、方法的封装调用
登录（用户名：admin，密码：admin）
文章新增：文章标题（string），文章的内容（string），文章更新时间（DateTime）
文章删除：根据文章的标题进行删除元素
文章查看：查看之前发布的文章
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV2
{
    class Program
    {
        //标题的临时变量
        static List<string> lstArticleTitle = new List<string>();

        //内容
        static List<string> lstArticleContent = new List<string>();
        static List<DateTime> lstDate = new List<DateTime>();

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
                        Console.WriteLine("请输入文章标题：");
                        string temp = Console.ReadLine();
                        lstArticleTitle.Add(temp);

                        Console.WriteLine("请输入文章内容：");
                        temp = Console.ReadLine();
                        lstArticleContent.Add(temp);

                        lstDate.Add(DateTime.Now);
                        break;
                    case "2":
                        for (int i = 0; i < lstArticleTitle.Count; i++)
                        {
                            string tipMsg = "文章标题：" + lstArticleTitle[i] + "，文章内容：" + lstArticleContent[i] + "，更新时间：" + lstDate[i];

                            Console.WriteLine(tipMsg);
                        }
                        break;
                    case "3":
                        Console.WriteLine("请输入需要删除的文章标题：");
                        string title = Console.ReadLine();

                        //查找文章标题第一个匹配项的索引位置
                        int index = lstArticleTitle.IndexOf(title);

                        if (index > -1)
                        {
                            //从文章标题的集合中删除该标题
                            lstArticleTitle.RemoveAt(index);
                            lstArticleContent.RemoveAt(index);
                            lstDate.RemoveAt(index);
                            Console.WriteLine("已删除。。。。");
                        }

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
