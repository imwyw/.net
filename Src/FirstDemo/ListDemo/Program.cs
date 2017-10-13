using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*集合
            ArrayList、List
            */
            ArrayList arr = new ArrayList();

            //在集合尾部进行添加元素
            arr.Add("qqq");
            arr.Add("abc");
            arr.Add("zzz");

            //添加一组元素到集合中
            arr.AddRange(new string[] { "a", "b", "c" });

            //Insert 插入操作，需要指定插入的索引位置
            arr.Insert(1, "halo");

            //删除 "qqq"元素 以下两行Remove和RemoveAt等效
            arr.Remove("qqq");
            arr.RemoveAt(0);

            Console.WriteLine(arr.Contains("c"));

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            /*
            Hashtable
            */
            Hashtable ht = new Hashtable();
            ht.Add(1, "abc");
            ht.Add(2, new int[] { 1, 2, 3 });
            ht.Add(3, true);


            /*
            泛型集合!!!!!!
            */
            List<int> lst1 = new List<int>();

            List<string> lst2 = new List<string>();

            //添加元素 lst1限定了类型为int
            lst1.Add(1);

            //添加元素 lst2限定类型为string
            lst2.Add("a");

            //在指定位置添加元素
            lst1.Insert(0, 2);

            //按照集合元素进行删除
            lst1.Remove(0);

            //按照集合索引位置进行删除
            lst1.RemoveAt(0);

            //检查元素是否存在，返回布尔值
            lst1.Contains(1);

            lst1.Min();

            lst1.RemoveRange(0, lst1.Count);

            /*
            已知一个double泛型集合，如何获取该集合元素中的次小值
            */
            List<double> lstDouble = new List<double>() { 23, 1, 32, 98, 2, 9, -8, -5 };
            double min = lstDouble.Min();
            lstDouble.Remove(min);
            Console.WriteLine(lstDouble.Min());

            Console.ReadKey();
        }

    }
}
