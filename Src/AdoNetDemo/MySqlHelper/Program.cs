using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlHelper.ExecuteNonQuery("", null);

            MySqlHelper.ExecuteNonQuery("", null, System.Data.CommandType.StoredProcedure);

        }
    }
}
