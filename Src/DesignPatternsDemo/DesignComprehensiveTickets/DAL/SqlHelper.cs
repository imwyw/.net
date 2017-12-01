using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlHelper
    {
        private SqlHelper() { }

        static string connStr = ConfigurationManager.AppSettings["ConnStr"];

        #region DML操作封装，如INSERT,UPDATE,DELETE

        /// <summary>
        /// 执行DML语句，如插入、删除、更新
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="sqlParams">SQL参数</param>
        /// <param name="cmdType">命令类型，默认为Text</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] sqlParams
            , CommandType cmdType = CommandType.Text)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;
            if (sqlParams != null && sqlParams.Length > 0)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region DQL操作封装,SELECT查询

        /// <summary>
        /// 返回统计的数目
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string cmdText, SqlParameter[] sqlParams)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            if (sqlParams != null && sqlParams.Length > 0)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                int res = int.Parse(cmd.ExecuteScalar().ToString());
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 查询，返回泛型集合，需要注意约束类型的属性名称需要和数据库表字段名称保持一致，但不区分大小写
        /// </summary>
        /// <typeparam name="T">约束类型</typeparam>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="sqlParams">SQL参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>返回约束的类型集合</returns>
        public static List<T> ExecuteReader<T>(string cmdText, SqlParameter[] sqlParams,
            CommandType cmdType = CommandType.Text) where T : new()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (sqlParams != null && sqlParams.Length > 0)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                SqlDataReader reader = cmd.ExecuteReader();
                List<T> lstRes = new List<T>();
                T entity = new T();

                //获取该类型的所有公开属性
                PropertyInfo[] props = typeof(T).GetProperties();

                while (reader.Read())
                {
                    entity = new T();
                    foreach (PropertyInfo p in props)
                    {
                        //默认当做属性名称和表字段名称对应，后期可以通过自定义特性进行匹配
                        object obj = reader[p.Name];

                        //设置对应的属性值
                        p.SetValue(entity, obj, null);
                    }
                    lstRes.Add(entity);
                }
                return lstRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 返回单个实体，第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="sqlParams"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static T ExecuteReaderFirst<T>(string cmdText, SqlParameter[] sqlParams,
            CommandType cmdType = CommandType.Text) where T : class, new()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (sqlParams != null && sqlParams.Length > 0)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                SqlDataReader reader = cmd.ExecuteReader();
                T entity = new T();

                //获取该类型的所有公开属性
                PropertyInfo[] props = typeof(T).GetProperties();

                if (reader.Read())
                {
                    foreach (PropertyInfo p in props)
                    {
                        //默认当做属性名称和表字段名称对应，后期可以通过自定义特性进行匹配
                        object obj = reader[p.Name];

                        //设置对应的属性值
                        p.SetValue(entity, obj, null);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);

                //需要限定where T : class, new() 有class引用类型才可确定返回null
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 查询，返回数据表
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="sqlParams">SQL参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>返回一个数据表</returns>
        public static DataTable FillTable(string cmdText, SqlParameter[] sqlParams
            , CommandType cmdType = CommandType.Text)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (sqlParams != null && sqlParams.Length > 0)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// 执行分页查询操作
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sqlTable">关系表名</param>
        /// <param name="sqlColumns">投影列，如*</param>
        /// <param name="sqlWhere">条件子句(可为空)，eg：and id=1 </param>
        /// <param name="sqlSort">排序语句(不可为空，必须有排序字段)，eg：id</param>
        /// <param name="pageIndex">当前页码索引号，从0开始</param>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <returns>分页对象</returns>
        public static Pager<T> ExecutePager<T>(string sqlTable, string sqlColumns, string sqlWhere
            , string sqlSort, int pageIndex, int pageSize) where T : new()
        {
            // 结果
            Pager<T> result = new Pager<T>();
            result.Total = 0;
            result.Rows = new List<T>();

            SqlConnection conn = new SqlConnection(connStr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            // 创建命令
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_paged_data";
            cmd.Parameters.AddWithValue("@sqlTable", sqlTable);
            cmd.Parameters.AddWithValue("@sqlColumns", sqlColumns);
            cmd.Parameters.AddWithValue("@sqlWhere", sqlWhere);
            cmd.Parameters.AddWithValue("@sqlSort", sqlSort);
            cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            cmd.Parameters.Add(new SqlParameter("@rowTotal", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });

            try
            {
                LogHelper.GetInstance().Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                // 执行命令
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    T a = new T();

                    PropertyInfo[] ps = typeof(T).GetProperties();
                    foreach (PropertyInfo pi in ps)
                    {
                        try
                        {
                            object v = reader[pi.Name];
                            pi.SetValue(a, v, null);
                        }
                        catch
                        { }
                    }

                    result.Rows.Add(a);
                }
                // 存在多个结果集，继续读取下一个结果
                reader.NextResult();
                result.Total = int.Parse(cmd.Parameters["@rowTotal"].Value.ToString());

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.GetInstance().Log("发生异常:" + ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 将command属性SqlParameterCollection转换为字符串显示
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        static string Params2String(SqlParameterCollection sqlParams)
        {
            StringBuilder builder = new StringBuilder("SqlValues:");
            if (sqlParams == null)
            {
                return string.Empty;
            }
            foreach (SqlParameter item in sqlParams)
            {
                builder.AppendFormat("[{0},{1}],", item.ParameterName, item.Value);
            }
            return builder.ToString().Substring(0, builder.ToString().Length - 1);
        }
    }
}
