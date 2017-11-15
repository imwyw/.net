using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// VirtualProxy 虚拟代理
/// </summary>
namespace ProxyPrintDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer p = new Printer("jack的打印机");
            Console.WriteLine("插播");//插播 需要等实例化完成后才能显示
            p.Print("我可以彩打");

            PrintProxy proxy = new PrintProxy("lucy的打印机");
            Console.WriteLine("插播");//插播 立即打印，代理的实例化不需要时间
            proxy.Print("我要上天");

        }
    }

    /// <summary>
    /// 打印接口
    /// </summary>
    public interface IPrint
    {
        /// <summary>
        /// 打印内容
        /// </summary>
        /// <param name="str"></param>
        void Print(string str);
    }

    /// <summary>
    /// 打印机类，继承Iprint接口
    /// </summary>
    public class Printer : IPrint
    {
        /// <summary>
        /// 打印机的名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 打印机自定义构造函数，形参为打印机名称
        /// </summary>
        /// <param name="name"></param>
        public Printer(string name)
        {
            Name = name;
            HeavyJob();
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="str"></param>
        public void Print(string str)
        {
            Console.WriteLine("=== " + Name + " ===");
            Console.WriteLine(str);
        }

        /// <summary>
        /// 耗时操作，模拟打印机启动时的预热
        /// </summary>
        private void HeavyJob()
        {
            Console.Write(Name + "\t开始启动");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("启动完成");
        }
    }

    /// <summary>
    /// 打印代理
    /// </summary>
    public class PrintProxy : IPrint
    {
        /// <summary>
        /// 真实主体，指向真的打印机
        /// </summary>
        public Printer Real { get; private set; }
        /// <summary>
        /// 代理的名称，作用是实例化时将这个name传递给真实主体Real
        /// </summary>
        public string Name { get; private set; }

        public PrintProxy(string name)
        {
            Name = name;
        }

        public void Print(string str)
        {
            if (null == Real)
            {
                Real = new Printer(Name);
            }
            Real.Print(str);
        }
    }
}
