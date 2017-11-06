using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    public class SichuanYabo : Yabo
    {
        public override void Show()
        {
            Console.WriteLine("偏麻的鸭脖");
        }
    }
}
