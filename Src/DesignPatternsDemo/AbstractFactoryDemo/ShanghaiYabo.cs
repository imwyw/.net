using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    /// <summary>
    /// 上海口味鸭脖
    /// </summary>
    public class ShanghaiYabo : Yabo
    {
        public override void Show()
        {
            Console.WriteLine("偏甜的鸭脖");
        }
    }
}
