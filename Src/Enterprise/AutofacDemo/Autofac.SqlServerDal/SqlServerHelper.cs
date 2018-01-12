using Autofac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.SqlServerDal
{
    public class SqlServerHelper : IDataAccess
    {
        public string GetData()
        {
            return "SqlServer取数据。。。";
        }
    }
}
