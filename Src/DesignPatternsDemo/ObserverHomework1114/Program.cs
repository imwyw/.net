using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 刘欣欣代码作为示例
/// </summary>
namespace ObserverHomework1114
{
    class Program
    {
        static void Main(string[] args)
        {
            IncremetalNumberGenerator ing = new IncremetalNumberGenerator(0, 50, 5);
            IObserver digit = new DigitObserver();
            IObserver graph = new GraphObserver();
            ing.AddObserves(digit);
            ing.AddObserves(graph);
            ing.Excute();

            Console.WriteLine("================================");
            FibonacciNumberGenerator fng = new FibonacciNumberGenerator(10);
            fng.AddObserves(digit);
            fng.AddObserves(graph);
            fng.Excute();

            Console.ReadKey();
        }
    }
    public interface IObserver
    {
        void Update(NumberGenerator number);
    }
    public abstract class NumberGenerator
    {
        List<IObserver> lstObserver = new List<IObserver>();

        public void AddObserves(IObserver ob)
        {
            lstObserver.Add(ob);
        }
        public void RemoveObserves(IObserver ob)
        {
            lstObserver.Remove(ob);
        }
        public void NotifyObserves()
        {
            foreach (IObserver item in lstObserver)
            {
                item.Update(this);
            }
        }
        public abstract int GetNumber();

        public abstract void Excute();

    }


    /// <summary>
    /// 作业一
    /// </summary>
    public class IncremetalNumberGenerator : NumberGenerator
    {
        int initNum;//初始值
        int endNum;//结束值
        int addNum;//增长
        int num;
        public IncremetalNumberGenerator(int initNum, int endNum, int addNum)
        {
            this.initNum = initNum;
            this.endNum = endNum;
            this.addNum = addNum;
        }

        public override int GetNumber()
        {
            return num;
        }
        public int GetInitNum()
        {
            return initNum;
        }
        public int GetEndNum()
        {
            return endNum;
        }
        public int GetAddNum()
        {
            return addNum;
        }

        public override void Excute()
        {
            for (int i = initNum; i <= endNum; i = i + addNum)
            {
                num = i;
                NotifyObserves();
            }
        }
    }
    /// <summary>
    /// 作业二
    /// </summary>

    public class FibonacciNumberGenerator : NumberGenerator
    {
        int length;
        int num;
        public FibonacciNumberGenerator(int length)
        { this.length = length; }
        public override void Excute()
        {
            for (int i = 0; i < length; i++)
            {
                num = CreateFibonacci(i);
                NotifyObserves();
            }
        }
        public int CreateFibonacci(int length)
        {
            if (length == 0 || length == 1)
            {
                return 1;
            }
            else
            {
                int num = CreateFibonacci(length - 1) + CreateFibonacci(length - 2);
                return num;
            }

        }
        public override int GetNumber()
        {
            return num;
        }
    }



    public class DigitObserver : IObserver
    {
        public void Update(NumberGenerator number)
        {
            Console.WriteLine(number.GetNumber());
            Thread.Sleep(100);
        }
    }

    /// <summary>
    /// 以“*”表示生成器的数值
    /// </summary>
    public class GraphObserver : IObserver
    {
        public void Update(NumberGenerator number)
        {
            for (int i = 0; i < number.GetNumber(); i++)
            {
                Console.Write(" * ");
            }
            Console.WriteLine();
            Thread.Sleep(100);
        }
    }
}
