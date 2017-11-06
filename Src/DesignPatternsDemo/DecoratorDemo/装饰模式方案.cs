using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorDemo
{
    /// <summary>
    /// 抽象组件，核心
    /// </summary>
    public abstract class Component
    {
        public abstract void Say(string name);
    }

    /// <summary>
    /// 具体组件，也就是被装饰对象
    /// </summary>
    public class ConcreteComponent : Component
    {
        public override void Say(string name)
        {
            Console.WriteLine(name);
        }
    }

    /// <summary>
    /// 装饰器抽象类
    /// </summary>
    public abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component com)
        {
            component = com;
        }

        public override void Say(string name)
        {
            component.Say(name);
        }
    }

    public class ZhDecorator : Decorator
    {
        public override void Say(string name)
        {
            base.Say(name);
            Console.WriteLine("你好!" + name);
        }
    }

    public class EnDecorator : Decorator
    {
        public override void Say(string name)
        {
            base.Say(name);
            Console.WriteLine("hello!" + name);
        }
    }

}
