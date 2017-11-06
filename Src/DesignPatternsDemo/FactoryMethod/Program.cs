using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            IFactoryOp facAdd = new AddFactory();
            Operation optAdd = facAdd.CreateOperate();

            optAdd.NumberA = 2;
            optAdd.NumberB = 3;
            optAdd.GetResult();
        }
    }


}
