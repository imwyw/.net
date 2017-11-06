using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    /// <summary>
    /// 工厂接口，定义具体工厂需要规范的方法
    /// </summary>
    public interface IFactoryOp
    {
        /// <summary>
        /// 定义接口，需要具体工厂类实现该接口
        /// 无参数，简单工厂有参数
        /// </summary>
        /// <returns></returns>
        Operation CreateOperate();
    }
}
