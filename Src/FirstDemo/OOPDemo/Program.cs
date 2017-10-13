using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal obj = new Animal();//1、对象的诞生，通过Animal构造函数实例化一个对象
            obj.Name = "二狗子";

            PointEx pp = new PointEx();
            pp.x = 1;
            pp.y = 2;

            TestStruct(pp, obj);

            Console.WriteLine(pp.x);
            Console.WriteLine(obj.Name);

            Student stu = new Student();
            stu.Name = "王富贵";
            Console.WriteLine(stu.Version);

            Student.ViewCount++;
            
            Console.ReadKey();
        }

        private static void TestStruct(PointEx pp, Animal obj)
        {
            pp.x = 100;
            obj.Name = "二哈";
        }
    }


    class Student
    {
        public Student()
        {
            version = "v1.0";
        }

        public string Name { get; set; }

        public bool IsExist { get; set; }

        private string version;
        public string Version
        {
            get { return version; }
        }

        public static int ViewCount = 0;
    }
}
