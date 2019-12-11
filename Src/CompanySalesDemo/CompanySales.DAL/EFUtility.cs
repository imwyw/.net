using CompanySales.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class EFUtility
    {
        /// <summary>
        /// EF 查询 DataTable 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DataTable GetDataTableBySql(string sql, SqlParameter[] sqlParams)
        {
            using (SaleContext db = new SaleContext())
            {
                IDbCommand cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = sql;
                if (sqlParams != null && sqlParams.Length > 0)
                {
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd as SqlCommand;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }
    }
}
