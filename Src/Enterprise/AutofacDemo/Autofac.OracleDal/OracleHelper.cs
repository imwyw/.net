using Autofac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.OracleDal
{
    public class OracleHelper : IDataAccess
    {
        public string GetData()
        {
            return "Oracle取数据";
        }
    }
}
