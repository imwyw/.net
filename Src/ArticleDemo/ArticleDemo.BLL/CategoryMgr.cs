using ArticleDemo.DAL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.BLL
{
    public class CategoryMgr
    {
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Add(string name)
        {
            int res = CategoryDao.Add(name);
            return res > 0;
        }

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCategory()
        {
            DataTable dt = CategoryDao.GetCategory();
            return dt;
        }

        /// <summary>
        /// 根据id删除类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int id)
        {
            int res = CategoryDao.DeleteCategory(id);
            return res > 0;
        }

        public static bool Update(Category cate)
        {
            int res = CategoryDao.Update(cate);
            return res > 0;
        }

        public static Category GetCategoryByID(int id)
        {
            return CategoryDao.GetCategoryByID(id);
        }

        /// <summary>
        /// 控制台添加类别
        /// </summary>
        public static void AddCategoryConsole()
        {
            Console.WriteLine("请输入类别名称：");
            string name = Console.ReadLine();
            if (Add(name))
            {
                Console.WriteLine("添加成功");
                ShowCategoryConsole();
            }
            else
            {
                Console.WriteLine("添加失败");
            }
        }

        /// <summary>
        /// 控制台显示所有类别
        /// </summary>
        public static void ShowCategoryConsole()
        {
            DataTable dt = GetCategory();
            Console.WriteLine("ID\t名称");
            foreach (DataRow dr in dt.Rows)
            {
                //Console.WriteLine(dr[0] + "\t" + dr[1]);
                Console.WriteLine(dr["ID"] + "\t" + dr["NAME"]);
            }
        }

        /// <summary>
        /// 控制台删除类别
        /// </summary>
        public static void DeleteCategoryConsole()
        {
            ShowCategoryConsole();
            Console.WriteLine("请输入需要删除类别的id");
            string id = Console.ReadLine();
            if (DeleteCategory(int.Parse(id)))
            {
                Console.WriteLine("删除成功");
                ShowCategoryConsole();
            }
            else
            {
                Console.WriteLine("删除失败");
            }
        }

    }
}
