using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main Thread";

            Console.WriteLine(Thread.CurrentThread.Name);//打印当前线程名称
            Console.WriteLine(Thread.CurrentThread.ThreadState);//打印当前线程状态

            Console.WriteLine("################################################");

            /*开辟线程*/
            //1、无参的
            //Thread threadVoid = new Thread(new ThreadStart(Say));
            Thread threadVoid = new Thread(Say);
            threadVoid.Start();

            //2、有参的
            //Thread threadParam = new Thread(new ParameterizedThreadStart(Talk));
            Thread threadParam = new Thread(Talk);
            threadParam.Start();

            //Thread.Sleep 阻塞当前线程
            Thread.Sleep(1);

            //实例方法，阻塞调用线程，直到某个线程终止或经过了指定时间为止。
            threadVoid.Join(10);

        }

        static void Say()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("1子线程" + DateTime.Now.ToString("mm:ss.fff"));
            }
        }


        static void Talk(object obj)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("2子线程" + DateTime.Now.ToString("mm:ss.fff"));
            }
        }
    }
}
