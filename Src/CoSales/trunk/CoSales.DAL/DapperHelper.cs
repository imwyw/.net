using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using CoSales.Common;
using CoSales.Model;

namespace CoSales.DAL
{
    public static class DapperHelper
    {
        static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        #region Dapper 基本封装
        /// <summary>
        /// 执行sql，仅返回影响记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int? Excute(string sql, object param = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.Info(sql);
                    var res = conn.Execute(sql, param);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取查询结果的第一行一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExcuteScalar(string sql, object param = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.InfoFormat(sql, param);
                    var res = conn.ExecuteScalar(sql, param);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        public static T QueryFirstOrDefault<T>(string sql, object param = null) where T : class
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.InfoFormat(sql, param);
                    var res = conn.QueryFirstOrDefault<T>(sql, param);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        public static IEnumerable<dynamic> Query(string sql, object param = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.InfoFormat(sql, param);
                    var res = conn.Query(sql, param);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        public static IEnumerable<T> Query<T>(string sql, object param = null) where T : class
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.InfoFormat(sql, param);
                    var res = conn.Query<T>(sql, param);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 封装自定义的分页查询
        /// 自由化程度更高，不过sql还是写在DAL中
        /// todo 修改sql为xml可配置化
        /// 原始sql中必须包含 ROW_NUMBER()开窗！！！ /**where**/ where注释标记不可少！！
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sql">SELECT ROW_NUMBER() OVER ( ORDER BY XXID ) AS RN,* FROM TABLE /**where**/ </param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public static ResultPager<T> GetResultPager<T>(SqlBuilder builder, string sql, PageInfo pager) where T : class
        {
            string countSql = string.Format($"SELECT COUNT(1) FROM ({sql}) AS TR");
            string dataSql = pager.IsPager ? string.Format($@"SELECT * FROM ({sql}) AS TR WHERE TR.RN>@PageStart AND TR.RN<@PageEnd ") : sql;

            var templateCount = builder.AddTemplate(countSql);
            var templateData = builder.AddTemplate(dataSql);

            // 是否分页查询
            if (pager.IsPager)
            {
                builder.AddParameters(new { PageStart = pager.PageStart, PageEnd = pager.PageEnd });
            }
            ResultPager<T> resPager = new ResultPager<T>();
            resPager.Total = (int)DapperHelper.ExcuteScalar(templateCount.RawSql, templateCount.Parameters);
            resPager.Rows = Query<T>(templateData.RawSql, templateData.Parameters).ToList();

            return resPager;
        }

        #endregion

        #region Simple CRUD 封装 https://github.com/ericdc1/Dapper.SimpleCRUD/
        /// <summary>
        /// 插入实体对象
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="entity">实例</param>
        /// <returns></returns>
        public static int Insert<T>(T entity)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.Info(entity);
                    int? res = conn.Insert(entity);
                    return res.GetValueOrDefault(-1);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return -1;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public static int Update<T>(T entityToUpdate)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    //Dapper.SimpleCRUD.Update(conn, entityToUpdate);
                    Log4Helper.Log.Info(entityToUpdate);
                    var res = conn.Update(entityToUpdate);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据主键id删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int Delete<T>(int Id)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.Info(Id);
                    var res = conn.Delete<T>(Id);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static int DeleteList<T>(object whereConditions)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.Info(whereConditions);
                    var res = conn.DeleteList<T>(whereConditions);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int RecordCount<T>(string conditions = "", object parameters = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    Log4Helper.Log.InfoFormat(conditions, parameters);
                    var res = conn.DeleteList<T>(conditions, parameters);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据id获取单个实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get<T>(object id) where T : class
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    var res = conn.Get<T>(id);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件获取对象集合
        /// 例如：var user = connection.GetList<User>(new { Age = 10 });  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetList<T>(object whereConditions = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    var res = conn.GetList<T>(whereConditions);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件获取对象集合
        /// 例如：var user = connection.GetList<User>("where age = 10 or Name like '%Smith%'");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetList<T>(string conditions, object parameters = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    var res = conn.GetList<T>(conditions, parameters);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 分页查询
        /// 例如：var user = connection.GetListPaged<User>(1,10,"where age = 10 or Name like '%Smith%'","Name desc");  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetListPaged<T>(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    var res = conn.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Log.Error(ex);
                return null;
            }
        }

        #endregion
    }
}
