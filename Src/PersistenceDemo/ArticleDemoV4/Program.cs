using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV4
{
    class Program
    {
        //保存文章实体的泛型集合
        static List<Article> lstArticle = new List<Article>();

        //保存用户实例的泛型集合
        static List<User> lstUser = new List<User>();

        static void Main(string[] args)
        {
            try
            {

                FilePersisHelper.LoadData("User.txt", lstUser);

                Login();

                SwitchOperate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FilePersisHelper.SaveData("User.txt", lstUser);
            }
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
                User usr = User.GetUserFromConsole();
                switch (key)
                {
                    case "1":
                        if (User.CheckExist(lstUser, usr))
                        {
                            Console.WriteLine(usr.Name + "，欢迎登录");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("用户名或密码错误");
                        }
                        break;
                    case "2":
                        if (User.CheckExistByID(lstUser, usr))
                        {
                            Console.WriteLine("该用户已存在");
                        }
                        else
                        {
                            Console.WriteLine("请输入用户昵称");
                            usr.Name = Console.ReadLine();
                            lstUser.Add(usr);
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
