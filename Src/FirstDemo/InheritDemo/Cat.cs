using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritDemo
{
    class Cat : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("吃鱼");
        }

        public override void Say()
        {
            Console.WriteLine("喵喵喵");
        }
    }
}
