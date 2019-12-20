using CompanySales.Common;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class MySqlHelper
    {
        static readonly string CONNSTR = "server=.;database=companysales;uid=sa;pwd=123456;";

        /// <summary>
        /// 执行DML（增删改）的SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">带有占位符的SQL</param>
        /// <param name="sqlParams">参数化查询的数组</param>
        /// <returns></returns>
        public static int ExecuteMyDML(string sql, SqlParameter[] sqlParams)
        {
            SqlConnection conn = new SqlConnection(CONNSTR);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                if (null != sqlParams && sqlParams.Length > 0)
                {
                    CheckNull2DbNull(sqlParams);
                    cmd.Parameters.AddRange(sqlParams);
                }

                // 打印sql脚本和参数值到日志信息
                MyLogHelper.Info($"{sql}\n{Params2String(cmd.Parameters)}");

                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                MyLogHelper.Error(ex);
                return -1;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// 根据sql查询得到数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable QueryData(string sql)
        {
            SqlConnection conn = new SqlConnection(CONNSTR);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                // 打印sql脚本和参数值到日志信息
                MyLogHelper.Info($"{sql}\n{Params2String(cmd.Parameters)}");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MyLogHelper.Error(ex);
                return null;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 获取第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static object ExecuteFirstValue(string sql, SqlParameter[] sqlParams)
        {
            SqlConnection conn = new SqlConnection(CONNSTR);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                if (null != sqlParams && sqlParams.Length > 0)
                {
                    cmd.Parameters.AddRange(sqlParams);
                }

                // 打印sql脚本和参数值到日志信息
                MyLogHelper.Info($"{sql}\n{Params2String(cmd.Parameters)}");

                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MyLogHelper.Error(ex);
                return null;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
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
            //connStr 为web.config中数据库连接字符串
            SqlConnection conn = new SqlConnection(CONNSTR);
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
                // 打印sql脚本和参数值到日志信息
                MyLogHelper.Info($"{cmdText}\n{Params2String(cmd.Parameters)}");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<T> lstRes = new List<T>();

                    //获取指定的数据类型
                    Type modelType = typeof(T);

                    //遍历reader
                    while (reader.Read())
                    {
                        //创建指定类型的实例
                        T entity = Activator.CreateInstance<T>();

                        //遍历reader字段
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //判断字段值是否为空或不存在
                            if (!IsNullOrDbNull(reader[i]))
                            {
                                //根据reader序列返回对应名称，并反射找到匹配的属性
                                PropertyInfo pi = typeof(T).GetProperty(reader.GetName(i),
                                    BindingFlags.GetProperty | BindingFlags.Public
                                    | BindingFlags.Instance | BindingFlags.IgnoreCase);

                                if (pi != null)
                                {
                                    //设置对象中匹配属性的值
                                    pi.SetValue(entity, CheckType(reader[i], pi.PropertyType), null);
                                }
                            }
                        }
                        lstRes.Add(entity);
                    }
                    return lstRes;
                }

            }
            catch (Exception ex)
            {
                // 应该在这里记录崩溃，保存异常信息
                //MyLogHelper.Write("发生异常", ex.Message);
                MyLogHelper.Error(ex);
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
            SqlConnection conn = new SqlConnection(CONNSTR);
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
                // 打印sql脚本和参数值到日志信息
                MyLogHelper.Info($"{cmdText}\n{Params2String(cmd.Parameters)}");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // 创建指定类型的实例
                        // 等同于 T entity = new T();
                        T entity = Activator.CreateInstance<T>();

                        //遍历reader字段
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //判断字段值是否为空或不存在
                            if (!IsNullOrDbNull(reader[i]))
                            {
                                //根据reader序列返回对应名称，并反射找到匹配的属性
                                PropertyInfo pi = typeof(T).GetProperty(reader.GetName(i),
                                    BindingFlags.GetProperty | BindingFlags.Public
                                    | BindingFlags.Instance | BindingFlags.IgnoreCase);

                                if (pi != null)
                                {
                                    //设置对象中匹配属性的值
                                    pi.SetValue(entity, CheckType(reader[i], pi.PropertyType), null);
                                }
                            }
                        }
                        return entity;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                MyLogHelper.Error(ex);
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
        public static Pager<T> ExecutePager<T>(
            string sqlTable,
            string sqlColumns,
            string sqlWhere,
            string sqlSort,
            int pageIndex,
            int pageSize) where T : new()
        {
            // 结果
            Pager<T> result = new Pager<T>();
            result.Total = 0;
            result.Rows = new List<T>();

            SqlConnection conn = new SqlConnection(CONNSTR);
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
                //MyLogHelper.Info(cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                Log4Helper.InfoLog.Info(cmd.CommandText + "\n" + Params2String(cmd.Parameters));
                // 执行命令
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //创建指定类型的实例
                    T entity = Activator.CreateInstance<T>();

                    //遍历reader字段
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        //判断字段值是否为空或不存在
                        if (!IsNullOrDbNull(reader[i]))
                        {
                            //根据reader序列返回对应名称，并反射找到匹配的属性
                            PropertyInfo pi = typeof(T).GetProperty(reader.GetName(i),
                                BindingFlags.GetProperty | BindingFlags.Public
                                | BindingFlags.Instance | BindingFlags.IgnoreCase);

                            if (pi != null)
                            {
                                //设置对象中匹配属性的值
                                pi.SetValue(entity, CheckType(reader[i], pi.PropertyType), null);
                            }
                        }
                    }

                    result.Rows.Add(entity);
                }
                // 存在多个结果集，继续读取下一个结果
                reader.NextResult();
                result.Total = int.Parse(cmd.Parameters["@rowTotal1"].Value.ToString());

                return result;
            }
            catch (Exception ex)
            {
                //MyLogHelper.Error(ex);
                Log4Helper.ErrorLog.Error(ex);
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
        /// 对可空类型进行判断转换，考虑实体类属性可为空的情况
        /// </summary>
        /// <param name="value">reader中的值</param>
        /// <param name="conversionType">实体类属性类型</param>
        /// <returns></returns>
        static object CheckType(object value, Type conversionType)
        {
            /*
            判断属性是否为可空类型  即可分配为 null 的值类型，有以下两种声明方式，是等价的： 
            public Nullable<int> NumA { get; set; }
            public int? NumB { get; set; }
            */
            if (conversionType.IsGenericType
                && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                System.ComponentModel.NullableConverter nullableConverter =
                    new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 判断对象是否为null或是dbnull
        /// DbNull较为特殊，使用 obj is DbNull 或是 obj == DbNull.Value进行判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static bool IsNullOrDbNull(object obj)
        {
            return (obj == null || (obj is DBNull)) ? true : false;
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

        /// <summary>
        /// 由于null不能直接插入至数据库，需要经过转换 DbNull
        /// </summary>
        /// <param name="sqlParams"></param>
        static void CheckNull2DbNull(SqlParameter[] sqlParams)
        {
            foreach (var item in sqlParams)
            {
                if (item.Value == null)
                {
                    item.Value = DBNull.Value;
                }
            }
        }
    }
}
