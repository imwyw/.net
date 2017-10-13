using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //装箱操作  值类型 -> 引用
            int a = 1;
            Console.WriteLine(a + "");

            object obj = a;

            //拆箱操作 引用 -> 值类型
            int b = (int)obj;

            double c = 1.2345678912;

            object o1 = c;

            //拆箱 丢失精度
            int i1 = (int)o1;
            float f1 = (float)o1;

            Console.WriteLine($"int拆箱后：{i1}，float拆箱后： {f1}");

            Console.ReadKey();
        }
    }
}
