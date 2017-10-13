using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            string connStr = "server=.;database=TEST_DB;uid=sa;pwd=123;";

            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }


        }
    }
}
