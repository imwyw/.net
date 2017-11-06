using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo
{
    /// <summary>
    /// 周黑鸭抽象类
    /// 总部
    /// </summary>
    public abstract class ZhouDuckFactory
    {
        public abstract Yabo CreateYabo();
        public abstract Yachi CreateYachi();
    }
}
