using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //生产上海的鸭脖和鸭翅
            ZhouDuckFactory shanghaiFt = new ShanghaiFactory();
            Yabo shanghaiYabo = shanghaiFt.CreateYabo();
            Yachi shanghaiYachi = shanghaiFt.CreateYachi();

            //生产湖南的鸭脖和鸭翅
            ZhouDuckFactory hunanFt = new HunanFactory();
            Yabo hunanYabo = hunanFt.CreateYabo();
            Yachi hunanYachi = hunanFt.CreateYachi();

            //生产四川的鸭脖和鸭翅
            ZhouDuckFactory sichuanFt = new SichuanFactory();
            Yabo sichuanYabo = sichuanFt.CreateYabo();
            Yachi sichuanYachi = sichuanFt.CreateYachi();
        }
    }


}
