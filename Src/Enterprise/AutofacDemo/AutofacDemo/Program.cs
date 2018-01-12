using Autofac.Service;
using AutofacDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dal = AutofacHelper.CreateInstance<IDataAccess>();
            var res = dal.GetData();
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
