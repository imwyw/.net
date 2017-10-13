using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //数组初始化的方式
            int[] array1 = { 1, 2, 3 };

            int[] array2 = new int[5];

            int[] array3 = new int[] { 1, 2, 3 };

            //二维数组
            int[,] arrA = { { 1, 2 }, { 3, 4 } };

            for (int i = 0; i < array1.Length; i++)
            {
                Console.WriteLine(array1[i]);
            }
            Console.WriteLine("=============================");
            //foreach(int item in array1) 和下面写法一致
            foreach (var item in array1)
            {
                Console.WriteLine(item);
            }

            /*
            已知一个整型数组，如何获取该数组元素中的次小值
            */
            int[] arrUnSort = { 23, 1, 32, 98, 2, 9, -8, -5 };
            int minValue = arrUnSort[0];
            int minSecValue = arrUnSort[0];

            Console.WriteLine("单次循环");
            for (int i = 1; i < arrUnSort.Length; i++)
            {
                if (minValue > arrUnSort[i])
                {
                    minSecValue = minValue;
                    minValue = arrUnSort[i];
                }
                else if (minSecValue > arrUnSort[i])
                {
                    minSecValue = arrUnSort[i];
                }
            }
            Console.WriteLine("minSecValue:" + minSecValue);

            //冒泡排序方式，取次小值
            for (int i = 0; i < arrUnSort.Length - 1; i++)
            {
                for (int j = 0; j < arrUnSort.Length - i - 1; j++)
                {
                    if (arrUnSort[j] > arrUnSort[j + 1])
                    {
                        Swap(ref arrUnSort[j], ref arrUnSort[j + 1]);
                    }
                }
            }
            Console.WriteLine("================================");
            for (int i = 0; i < arrUnSort.Length; i++)
            {
                Console.WriteLine(arrUnSort[i]);
            }

            Console.WriteLine("minValue:" + arrUnSort[1]);

            Console.ReadKey();
        }

        /// <summary>
        /// 交换两个数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

    }
}
