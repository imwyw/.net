using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    /// <summary>
    /// 加法工厂
    /// </summary>
    public class AddFactory : IFactoryOp
    {
        public Operation CreateOperate()
        {
            return new OperationAdd();
        }
    }
}
