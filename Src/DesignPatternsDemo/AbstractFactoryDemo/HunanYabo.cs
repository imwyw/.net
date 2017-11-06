using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    /// <summary>
    /// 湖南鸭脖
    /// </summary>
    public class HunanYabo : Yabo
    {
        public override void Show()
        {
            Console.WriteLine("偏辣的鸭脖");
        }
    }
}
