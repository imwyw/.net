using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
    public class Student
    {
        public Student(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Age公开字段
        /// </summary>
        public int Age;

        /// <summary>
        /// Name公开属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公开方法
        /// </summary>
        public bool Say(string oth)
        {
            Console.WriteLine("hello {0},how do u do,i'm {1}", oth, Name);
            return true;
        }
    }
}
