<!-- TOC -->

- [EF Core](#ef-core)
    - [安装](#安装)
    - [生成数据实体](#生成数据实体)
    - [工具类](#工具类)
        - [SqlQuery raw sql查询](#sqlquery-raw-sql查询)
        - [Utility工具](#utility工具)

<!-- /TOC -->

<a id="markdown-ef-core" name="ef-core"></a>
# EF Core
Entity Framework (EF) Core 是轻量化、可扩展、开源和跨平台版的常用 Entity Framework 数据访问技术。

就是 EntityFramework 的跨平台版本，用法基本一致。

<a id="markdown-安装" name="安装"></a>
## 安装
在Package Manager console中运行以下命令，安装包管理器控制台工具：

```bash
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

<a id="markdown-生成数据实体" name="生成数据实体"></a>
## 生成数据实体
首先确保都安装CLI相关工具引用，在Package Manager console中运行以下命令：

```bash
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools
```

需要使用 Scaffold-DbContext 命令生成数据实体类型，数据表必须有主键！

```bash
Scaffold-DbContext -connection "Server=.;Database=CompanySales;Trusted_Connection=True;uid=sa;pwd=123456;" -provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context SaleContext -project CompanySales.Repository -force
```

详见 [Scaffold-DbContext](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell#scaffold-dbcontext)

需要注意的是，如果是多个项目的话，比如三层架构，同样需要在每个项目添加EF相关的依赖。

<a id="markdown-工具类" name="工具类"></a>
## 工具类

<a id="markdown-sqlquery-raw-sql查询" name="sqlquery-raw-sql查询"></a>
### SqlQuery raw sql查询

`SqlQueryExtensions.cs` 扩展对 `EF CORE` 的sql 查询支持

```cs
public static class SqlQueryExtensions
{
    public static IList<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
    {
        using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
        {
            return db2.Set<T>().FromSqlRaw(sql, parameters).ToList();
        }
    }

    private class ContextForQueryType<T> : DbContext where T : class
    {
        private readonly DbConnection connection;

        public ContextForQueryType(DbConnection connection)
        {
            this.connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
```

<a id="markdown-utility工具" name="utility工具"></a>
### Utility工具

```cs
/// <summary>
/// ef core 操作工具类
/// SqlParameter 是 Microsoft.Data.SqlClient.SqlParameter，System.Data.SqlClient会报错 坑！！！
/// modified by imwyw 2020-4-10
/// </summary>
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
            var cmd = db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sql;
            if (sqlParams != null && sqlParams.Length > 0)
            {
                foreach (var item in sqlParams)
                {
                    cmd.Parameters.Add(item);
                }
            }

            DataTable dt = new DataTable();
            db.Database.OpenConnection();
            dt.Load(cmd.ExecuteReader());

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
        string sqlCount = ConvertSqlCount(sql);

        using (SaleContext db = new SaleContext())
        {
            var cmd = db.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sql;

            if (parameters != null)
            {
                // clone 为解决 【另一个 SqlParameterCollection 中已包含 SqlParameter。】问题
                var paramClone = parameters.Select(t => ((ICloneable)t).Clone());

                foreach (var item in paramClone)
                {
                    cmd.Parameters.Add(item as SqlParameter);
                }
            }

            db.Database.OpenConnection();
            int count = (int)cmd.ExecuteScalar();

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
    public static List<T> GetList<T>(string sql, IEnumerable<SqlParameter> parameters, PageParameter pageInfo) where T : class
    {
        using (SaleContext db = new SaleContext())
        {
            IEnumerable<object> paramClone = null;
            if (null != parameters)
            {
                // clone 为解决 【另一个 SqlParameterCollection 中已包含 SqlParameter。】问题
                paramClone = parameters.Select(t => ((ICloneable)t).Clone());
            }

            var query = db
                .SqlQuery<T>(sql, paramClone)
                .AsQueryable();

            // 如果开启分页，则进行 skip take
            if (pageInfo.IsPage)
            {
                query = query.Skip(pageInfo.Skip).Take(pageInfo.PageSize);
            }
            List<T> list = query.ToList();

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

```





























