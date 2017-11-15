using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
使用装饰模式设计一个 装饰不同口味的冰激凌
香草、巧克力、草莓、黄桃
最终销售的：
1、默认；
2、香草/巧克力;(单独口味)
3、香草+草莓；
*/
namespace DecoratorHomework1106
{
    class Program
    {
        static void Main(string[] args)
        {

            Ice ice = new IceCream();
            //IceDecorator xiangcao = new DecoratorXiangCao();
            //xiangcao.SetIce(ice);

            //IceDecorator qiaokeli = new DecoratorQiaokeli();
            //qiaokeli.SetIce(xiangcao);

            //IceDecorator caomei = new DecoratorCaoMei();
            //caomei.SetIce(qiaokeli);

            //IceDecorator huangtao = new DecoratorHuangTao();
            //huangtao.SetIce(caomei);

            //huangtao.Show();

            //实例化实参是另外一个装饰器的实例化操作
            IceDecorator deco = new DecoratorXiangCao(new DecoratorCaoMei(new DecoratorHuangTao(ice)));
            deco.Show();

        }
    }

    /// <summary>
    /// 抽象组件
    /// </summary>
    public abstract class Ice
    {
        /// <summary>
        /// 抽象方法，显示当前冰激凌类型
        /// </summary>
        public abstract void Show();
    }

    /// <summary>
    /// 冰激凌，被装饰体
    /// </summary>
    public class IceCream : Ice
    {
        public override void Show()
        {
            Console.WriteLine("冰激凌");
        }
    }

    /// <summary>
    /// 装饰抽象类
    /// </summary>
    public abstract class IceDecorator : Ice
    {
        protected Ice component;

        public IceDecorator(Ice ice)
        {
            component = ice;
        }

        public override void Show()
        {
            component.Show();
        }
    }

    /// <summary>
    /// 香草装饰
    /// </summary>
    public class DecoratorXiangCao : IceDecorator
    {
        public DecoratorXiangCao(Ice ice) : base(ice) { }

        public override void Show()
        {
            Console.Write("香草\t");
            base.Show();
        }
    }

    /// <summary>
    /// 草莓装饰
    /// </summary>
    public class DecoratorCaoMei : IceDecorator
    {
        public DecoratorCaoMei(Ice ice) : base(ice) { }
        public override void Show()
        {
            Console.Write("草莓\t");
            base.Show();
        }
    }

    /// <summary>
    /// 巧克力装饰
    /// </summary>
    public class DecoratorQiaokeli : IceDecorator
    {
        public DecoratorQiaokeli(Ice ice) : base(ice) { }
        public override void Show()
        {
            Console.Write("巧克力\t");
            base.Show();
        }
    }

    /// <summary>
    /// 黄桃装饰
    /// </summary>
    public class DecoratorHuangTao : IceDecorator
    {
        public DecoratorHuangTao(Ice ice) : base(ice) { }
        public override void Show()
        {
            Console.Write("黄桃\t");
            base.Show();
        }
    }
}
