using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBuyTickets
{
    class Program
    {
        /// <summary>
        /// 用于lock锁
        /// </summary>
        static object lockObj = new object();

        /// <summary>
        /// 保存余票数目
        /// </summary>
        static int ticketAccount = 100;

        /// <summary>
        /// 用于生成随机数的实例
        /// </summary>
        static Random random = new Random();

        static void Main(string[] args)
        {
            //开辟三个线程，模拟同时购票的场景
            Thread worker1 = new Thread(new ThreadStart(BuyTicket));
            worker1.Name = "王富贵";

            Thread worker2 = new Thread(new ThreadStart(BuyTicket));
            worker2.Name = "赵有才";

            Thread worker3 = new Thread(new ThreadStart(BuyTicket));
            worker3.Name = "宋小宝";

            //开启线程
            worker1.Start();
            worker2.Start();
            worker3.Start();
        }

        /// <summary>
        /// 购票方法
        /// </summary>
        static void BuyTicket()
        {
            //注意此处，需要while包含lock，否则锁住资源不会释放，会在一个线程上耗死
            //余票充足的情况下，循环进行随机购票
            while (ticketAccount > 0)
            {
                lock (lockObj)
                {
                    int cnt = random.Next(1, 5);
                    if (ticketAccount < cnt)
                    {
                        Console.WriteLine("你好，{0}，余票不足。。。");
                        return;
                    }
                    ticketAccount -= cnt;
                    Console.WriteLine("{0}购票{1}张，剩余票数{2}", Thread.CurrentThread.Name, cnt, ticketAccount);
                }
            }

        }
    }
}
