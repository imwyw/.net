/*
利用数组设计一个文章发布系统，包含以下功能
内置数据类型、分支、循环等代码结构、数组的定义及使用、方法的封装调用
登录（用户名：admin，密码：admin）
文章新增：文章标题（string），文章的内容（string），文章更新时间（DateTime）
文章查看：查看之前发布的文章
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV1
{
    class Program
    {
        static void Main(string[] args)
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
            //标题的临时变量
            string[] arrArticleTitle = new string[100];
            //内容
            string[] arrArticleContent = new string[100];
            DateTime[] arrDate = new DateTime[100];
            //当前文章数目
            int count = 0;


            while (true)
            {
                Console.WriteLine("请选择对应的操作：");
                Console.WriteLine("1.发布文章");
                //英文状态下，快捷键呼出智能提示ctrl+j
                Console.WriteLine("2.查看文章");
                Console.WriteLine("0.退出");

                string keyCode = Console.ReadLine();
                switch (keyCode)
                {
                    case "1":
                        Console.WriteLine("请输入文章标题：");
                        string temp = Console.ReadLine();
                        arrArticleTitle[count] = temp;

                        Console.WriteLine("请输入文章内容：");
                        temp = Console.ReadLine();
                        arrArticleContent[count] = temp;

                        arrDate[count] = DateTime.Now;

                        count++;
                        break;
                    case "2":
                        for (int i = 0; i < arrArticleTitle.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(arrArticleTitle[i]))
                            //if (arrArticleTitle[i] != "" && arrArticleTitle[i] != null)
                            {
                                string res = StrCmp("文章标题：", arrArticleTitle[i], "，文章内容：", arrArticleContent[i], "，更新时间：" + arrDate[i]);
                                Console.WriteLine(res);
                            }
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

        /// <summary>
        /// 拼接多个字符串
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static string StrCmp(params string[] args)
        {
            string res = string.Empty;
            foreach (var item in args)
            {
                res += item;
            }
            return res;
        }
    }
}
