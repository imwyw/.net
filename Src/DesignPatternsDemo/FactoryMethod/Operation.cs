using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    /// <summary>
    /// 运算类
    /// </summary>
    public class Operation
    {
        public double NumberA { get; set; }
        public double NumberB { get; set; }
        /// <summary>
        /// 获取运算结果。虚方法，子类可以重写
        /// </summary>
        /// <returns></returns>
        public virtual double GetResult()
        {
            return 0;
        }
    }
}
