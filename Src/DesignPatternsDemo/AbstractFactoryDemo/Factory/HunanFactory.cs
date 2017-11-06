using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    public class HunanFactory : ZhouDuckFactory
    {
        public override Yabo CreateYabo()
        {
            return new HunanYabo();
        }

        public override Yachi CreateYachi()
        {
            return new HunanYachi();
        }
    }
}
