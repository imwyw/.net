using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPDemo
{
    struct PointEx
    {
        public double x;
        public double y;
    }

    class Animal
    {
        public string Name;

        //不显式的声明构造函数，会有一个默认的构造函数
        public Animal()
        {
            Console.WriteLine("使用构造函数进行实例化");
        }
    }
}
