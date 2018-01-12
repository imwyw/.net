using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "后悔创立阿里杰克马";
            name.ToConsole();

            //Delegate1 dele = new Delegate1(BuyTicket);
            //dele += Subway;

            //BackHome(dele);

            //Console.WriteLine("===================");

            //PredicateDemo();
        }

        delegate void Delegate1();

        static void BackHome(Delegate1 action)
        {
            action();
        }

        static void BuyTicket()
        {
            Console.WriteLine("买火车票");
        }

        static void Subway()
        {
            Console.WriteLine("再换乘地铁");
        }

        /*
        预定义的 委托
        泛型委托
        */

        //Action 等效
        delegate void Delegate11();

        //Action<int, string, bool, object> 等效
        delegate void Delegate2(int num, string str, bool isa, object obj);

        //Func<string> 等效
        delegate string Delegate12();

        //Func<string, bool> 等效
        delegate bool Delegate22(string name);

        //Func<string, bool, object, int> 等效
        delegate int Delegate3(string str, bool isa, object obj);

        static void PredicateDemo()
        {
            List<string> listStr = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
            string[] arrStr = listStr.ToArray();

            /*
            筛选 长度小于3的元素
            */

            //1、原始方法
            foreach (var item in listStr)
            {
                if (item.Length <= 3)
                {
                    //Console.WriteLine(item);
                }
            }
            foreach (var item in arrStr)
            {
                if (item.Length <= 3)
                {
                    //Console.WriteLine(item);
                }
            }

            //2、使用Predicate
            /*
            (t) => { t.length <=3; }
            */
            List<string> list1 = listStr.FindAll(t => t.Length <= 3 && t.Contains("O"));//One
            Console.WriteLine(string.Join(",", list1));

            Console.WriteLine("============================");
            List<string> list2 = listStr.FindAll(t =>
            {
                //...
                return true;
            });
            Console.WriteLine(string.Join(",", list2));

            string[] arr1 = Array.FindAll(arrStr, t => t.Length <= 3);
            //Console.WriteLine(string.Join(",", arr1));

            //Where
            //listStr.Where(t=>);
        }
    }
}
