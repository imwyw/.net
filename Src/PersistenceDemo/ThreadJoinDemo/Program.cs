using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadJoinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread worker = new Thread(delegate ()
            {
                Thread.Sleep(100);
                Console.WriteLine("new");
            });
            worker.Start();
            Console.WriteLine("main1");
            /*
            Join方法不传参数的时候，默认等待子线程执行完
            子线程的join会阻塞主线程，直到子线程终止。结果如下：
            main1
            new
            main2
            注释下面这行，如果没有子线程阻塞的话，则是：
            main1
            main2
            new
            */
            worker.Join();
            Console.WriteLine("main2");
        }

    }
}
