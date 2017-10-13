using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Test("abc");
            Console.WriteLine(Test2("i", "am", "jack", "hello"));

            int intA = 1;
            TestRef(ref intA);
            Console.WriteLine("new intA:" + intA);

            double dobA = 1.23;
            TestRef(dobA);
            Console.WriteLine("new dobA:" + dobA);

            /*
            out参数的使用
            */
            int[] array = { 1, 2, 3 };
            int count;
            TestOut(array, out count);
            Console.WriteLine("out参数的使用");
            Console.WriteLine(count);

            Console.ReadKey();
        }

        static int Test(string name)
        {
            return 0;
        }

        /// <summary>
        /// params多个形参的用法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static string Test2(params string[] args)
        {
            string res = "";
            foreach (var item in args)
            {
                res += item;
            }

            return res;
        }

        /// <summary>
        /// 引用形参
        /// </summary>
        /// <param name="value"></param>
        static void TestRef(ref int value)
        {
            value += 1;
        }

        /// <summary>
        /// 引用形参
        /// </summary>
        /// <param name="value"></param>
        static void TestRef(double value)
        {
            value += 1;
        }

        /// <summary>
        /// out参数的使用，多个返回结果
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        static int[] TestOut(int[] arr, out int cnt)
        {
            //cnt = 0;
            cnt = arr.Length;
            return arr;
        }
    }
}
