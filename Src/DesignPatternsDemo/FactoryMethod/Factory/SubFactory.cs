using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    /// <summary>
    /// 减法工厂
    /// </summary>
    public class SubFactory : IFactoryOp
    {
        public Operation CreateOperate()
        {
            return new OperationSub();
        }
    }
}
