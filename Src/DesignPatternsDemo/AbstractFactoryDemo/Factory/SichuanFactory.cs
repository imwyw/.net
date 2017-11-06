using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    public class SichuanFactory : ZhouDuckFactory
    {
        public override Yabo CreateYabo()
        {
            return new SichuanYabo();
        }

        public override Yachi CreateYachi()
        {
            return new SichuanYachi();
        }
    }
}
