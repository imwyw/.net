using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyDemo.Controllers
{
    /// <summary>
    /// 这样的派生自Controller的类，我们称之为控制器
    /// </summary>
    public class DefaultController : Controller
    {

        /// <summary>
        /// 对于这样的方法我们称之为action
        /// 仅返回一个视图view，不带有任何数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //返回一个视图
            return View();
        }

        /// <summary>
        /// 返回带有数据的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexSimpleData()
        {
            Student stu = new Student("jack", 123);

            //强类型的传值
            return View(stu);
        }

        /// <summary>
        /// 返回动态对象的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexComplexData()
        {
            //动态属性
            dynamic viewModel = new ExpandoObject();
            viewModel.FirstName = "妲己";
            viewModel.LastName = "斯温";
            viewModel.ZhName = "路西法";

            return View(viewModel);
        }

    }

    /// <summary>
    /// 定义一个view model类
    /// </summary>
    public class Student
    {
        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Pwd { get; set; }
    }
}