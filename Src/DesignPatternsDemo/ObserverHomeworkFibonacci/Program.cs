using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverHomeworkFibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime d1 = DateTime.Now;
            ulong[] arr1 = Generator(40);
            foreach (var item in arr1)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("\n时间消耗：" + (DateTime.Now - d1));

            Console.WriteLine("====================================================");
            DateTime d2 = DateTime.Now;
            ulong[] arr2 = GeneratorFibonacci(100);
            foreach (var item in arr2)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("\n时间消耗：" + (DateTime.Now - d2));
        }

        /// <summary>
        /// 返回指定长度的fibonacci数列
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static ulong[] Generator(int length)
        {
            ulong[] arr = new ulong[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = CreateFibonacci(i);
            }
            return arr;
        }

        /// <summary>
        /// 递归生成fibonacci数列对应索引位置上的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        static ulong CreateFibonacci(int index)
        {
            if (index == 0 || index == 1)
            {
                return 1;
            }
            else
            {
                ulong num = CreateFibonacci(index - 1) + CreateFibonacci(index - 2);
                return num;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static ulong[] GeneratorFibonacci(int length)
        {
            ulong[] arr = new ulong[length];
            for (int i = 0; i < length; i++)
            {
                if (i <= 1)
                {
                    arr[i] = 1;
                }
                else
                {
                    arr[i] = arr[i - 1] + arr[i - 2];
                }
            }
            return arr;
        }
    }
}
