/*
利用面向对象的思想设计一个文章发布系统，并利用文本文件进行持久化保存，包含以下功能
登录/注册
文章新增：文章标题（string），文章的内容（string），文章更新时间（DateTime）
文章删除：根据文章的标题进行删除元素
文章查看：查看之前发布的文章
*/
using ArticleDemo.BLL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.UI
{
    class Program
    {
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
                Console.WriteLine("1.登录");
                Console.WriteLine("2.注册");

                string key = Console.ReadLine();
                User usr = UserMgr.GetUserFromConsole();
                switch (key)
                {
                    case "1":
                        //用户输入信息并不包含唯一标识，需要重新从数据库获取
                        User loginUser = new UserMgr().Login(usr.Name, usr.Pwd);
                        if (loginUser != null)
                        {
                            Console.WriteLine(usr.Name + "，欢迎登录");
                            GlobalData.CurrentUser = loginUser;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("用户名或密码错误");
                        }
                        break;
                    case "2":
                        if (new UserMgr().CheckExist(usr.Name))
                        {
                            Console.WriteLine("该用户已存在");
                        }
                        else
                        {
                            Console.WriteLine("请输入用户昵称");
                            usr.Zh_Name = Console.ReadLine();
                            if (new UserMgr().Add(usr))
                            {
                                Console.WriteLine("注册成功，欢迎 " + usr.Zh_Name);
                            }
                            else
                            {
                                Console.WriteLine("注册失败，请联系管理员 12312341234");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("输入有误");
                        break;
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
                Console.WriteLine("====================================");
                Console.WriteLine("请选择对应的操作：");
                Console.WriteLine("1、新增类别");
                Console.WriteLine("2、删除类别");
                Console.WriteLine("3、查看类别");

                Console.WriteLine("4、发布文章");
                Console.WriteLine("5、更新文章");
                Console.WriteLine("6、查看文章");
                Console.WriteLine("7、删除文章");

                Console.WriteLine("0、退出");

                string keyCode = Console.ReadLine();
                switch (keyCode)
                {
                    case "1":
                        CategoryMgr.AddCategoryConsole();
                        break;
                    case "2":
                        CategoryMgr.DeleteCategoryConsole();
                        break;
                    case "3":
                        CategoryMgr.ShowCategoryConsole();
                        break;
                    case "4":
                        ArticleMgr.AddConsole();
                        break;
                    case "5":
                        ArticleMgr.UpdateConsole();
                        break;
                    case "6":
                        ArticleMgr.ShowConsole();
                        break;
                    case "7":
                        ArticleMgr.RemoveConsole();
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
