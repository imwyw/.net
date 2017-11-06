using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorDemo
{
    public abstract class Welcome
    {
        public abstract void Say(string name);
    }

    public class DefaultWelcome : Welcome
    {
        public override void Say(string name)
        {
            Console.WriteLine(name);
        }
    }

    public class ZhWelcome : Welcome
    {
        public override void Say(string name)
        {
            Console.WriteLine("你好！" + name);
        }
    }

    public class EnWelcome : Welcome
    {
        public override void Say(string name)
        {
            Console.WriteLine("hello!" + name);
        }
    }
}
