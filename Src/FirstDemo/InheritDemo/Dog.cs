using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritDemo
{
    class Dog : Animal
    {
        public int Age { get; set; }

        public override void Eat()
        {
            Console.WriteLine("吃骨头");
        }

        public override void Say()
        {
            Console.WriteLine("汪汪汪");
        }

    }
}
