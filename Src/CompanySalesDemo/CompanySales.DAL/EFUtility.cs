using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// 查询统计数目
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int GetCount(string sql, IEnumerable<SqlParameter> parameters)
        {
            // clone 为解决 【另一个 SqlParameterCollection 中已包含 SqlParameter。】问题
            var paramClone = parameters.Select(t => ((ICloneable)t).Clone());
            string sqlCount = ConvertSqlCount(sql);

            using (SaleContext db = new SaleContext())
            {
                int count = db.Database.SqlQuery<int>(sqlCount, paramClone.ToArray())
                    .First();
                return count;
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string sql, IEnumerable<SqlParameter> parameters, PageParameter pageInfo)
        {
            // clone 为解决 【另一个 SqlParameterCollection 中已包含 SqlParameter。】问题
            var paramClone = parameters.Select(t => ((ICloneable)t).Clone());

            using (SaleContext db = new SaleContext())
            {
                var list = db.Database.SqlQuery<T>(sql, paramClone.ToArray())
                    .Skip(pageInfo.Skip).Take(pageInfo.PageSize)
                    .ToList();
                return list;
            }
        }

        /// 处理sql，将 select a,b,c from xxx 转换为 select count(1) from xxx结构
        /// 快速统计数目
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static string ConvertSqlCount(string sql)
        {
            /* 正则替换，忽略大小写
            \s空白符，\S非空白符，[\s\S]是任意字符
            */
            Regex reg = new Regex(@"select[\s\S]*from", RegexOptions.IgnoreCase);
            string sqlCount = reg.Replace(sql, "SELECT COUNT(1) FROM ");
            return sqlCount;
        }
    }
}
