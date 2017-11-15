using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverNumberDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNumberGenerator rdGenerator = new RandomNumberGenerator();
            IObserver digit = new DigitObserver();
            IObserver graph = new GraphObserver();
            rdGenerator.AddObserver(digit);
            rdGenerator.AddObserver(graph);
            rdGenerator.Excute();

            Console.WriteLine("-----------------------------------------------------");
            rdGenerator.DeleteObserver(digit);
            rdGenerator.Excute();
        }
    }

    public interface IObserver
    {
        void Update(NumberGenerator number);
    }

    /// <summary>
    /// 数字生成器抽象类
    /// </summary>
    public abstract class NumberGenerator
    {
        /// <summary>
        /// 保存观察者对象的引用
        /// </summary>
        List<IObserver> lstObserver = new List<IObserver>();

        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="obs"></param>
        public void AddObserver(IObserver obs)
        {
            lstObserver.Add(obs);
        }

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="obs"></param>
        public void DeleteObserver(IObserver obs)
        {
            lstObserver.Remove(obs);
        }

        /// <summary>
        /// 通知所有观察者对象
        /// </summary>
        public void NotifyObservers()
        {
            foreach (IObserver item in lstObserver)
            {
                //this指代的是NumberGenerator类的实例
                item.Update(this);
            }
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <returns></returns>
        public abstract int GetNumber();
        /// <summary>
        /// 生成数值
        /// </summary>
        public abstract void Excute();
    }

    /// <summary>
    /// 随机数值生成器
    /// </summary>
    public class RandomNumberGenerator : NumberGenerator
    {
        Random rd = new Random();
        int number;

        /// <summary>
        /// 生成 [0,20]之间的整数
        /// </summary>
        public override void Excute()
        {
            for (int i = 0; i < 10; i++)
            {
                number = rd.Next(0, 21);
                NotifyObservers();
            }
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <returns></returns>
        public override int GetNumber()
        {
            return number;
        }
    }

    /// <summary>
    /// 以数字形式显示生成器的数值
    /// </summary>
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
