using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    public class HunanYachi : Yachi
    {
        public override void Show()
        {
            Console.WriteLine("偏辣的鸭翅");
        }
    }
}
