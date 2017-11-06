using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    /// <summary>
    /// 上海的鸭翅
    /// </summary>
    public class ShanghaiYachi : Yachi
    {
        public override void Show()
        {
            Console.WriteLine("偏甜的鸭翅");
        }
    }
}
