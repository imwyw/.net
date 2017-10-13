using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateDemo
{
    class Program
    {


        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //TakeFood();

            //TakeFood 传递引用
            BringSomethingBack(TakeFood);
            
            Console.ReadKey();
        }

        /// <summary>
        /// 委托的定义，需要注意两点
        /// a）返回类型和被委托方法的返回类型一致
        /// b）形参和被委托方法的形参保持一致
        /// </summary>
        delegate void DelegateBug();

        static void BringSomethingBack(DelegateBug buyMethod)
        {
            //如果委托调用时传递的是TakeFood方法的引用，那么下面两行代码等效
            //TakeFood();
            buyMethod();//执行我们的委托方法
        }

        static void TakeFood()
        {
            Console.WriteLine("带饭");
        }

        static void BuyWater()
        {
            Console.WriteLine("买水");
        }
    }
}
