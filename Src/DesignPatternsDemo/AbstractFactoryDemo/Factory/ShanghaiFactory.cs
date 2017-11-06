using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    public class ShanghaiFactory : ZhouDuckFactory
    {
        public override Yabo CreateYabo()
        {
            return new ShanghaiYabo();
        }

        public override Yachi CreateYachi()
        {
            return new ShanghaiYachi();
        }
    }
}
