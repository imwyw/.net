using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyStarDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteStar baoqiang = new ConcreteStar("王宝强");
            StarProxy proxy = new StarProxy("宋喆", baoqiang);
            proxy.BookTicket();
            proxy.MakeMovie();
            proxy.Sleep();
        }
    }

    /// <summary>
    /// Subject类 抽象明星和经纪人
    /// </summary>
    public abstract class Star
    {
        public string Name { get; set; }
        public Star(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 订票 可有由代理进行代替操作的
        /// </summary>
        public abstract void BookTicket();

        /// <summary>
        /// 拍电影 只可以由真实主体进行
        /// </summary>
        public abstract void MakeMovie();

        /// <summary>
        /// 应该是真实主体做的，但是被代理代替了。。。
        /// </summary>
        public abstract void Sleep();
    }

    /// <summary>
    /// 具体明星
    /// </summary>
    public class ConcreteStar : Star
    {
        public ConcreteStar(string name) : base(name) { }

        public override void BookTicket()
        {
            Console.WriteLine(Name + "订票");
        }

        public override void MakeMovie()
        {
            Console.WriteLine(Name + "拍电影");
        }

        public override void Sleep()
        {
            Console.WriteLine(Name + "很累。。。");
        }
    }

    /// <summary>
    /// 经纪人类
    /// </summary>
    public class StarProxy : Star
    {
        /// <summary>
        /// 该经纪人的真实代理明星
        /// </summary>
        ConcreteStar real;
        /// <summary>
        /// 构造经纪人时，其所代理的明星由构造函数传入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="star"></param>
        public StarProxy(string name, ConcreteStar star) : base(name)
        {
            real = star;
        }

        /// <summary>
        /// 订票 可以由经纪人代理
        /// </summary>
        public override void BookTicket()
        {
            Console.WriteLine(Name + "代购票");
        }

        /// <summary>
        /// 拍电影 不可以由经纪人代理
        /// </summary>
        public override void MakeMovie()
        {
            //可增加前置操作
            real.MakeMovie();
            //可增加后置操作
        }

        public override void Sleep()
        {
            //real.Sleep();
            Console.WriteLine(Name + "很开心");
        }
    }
}
