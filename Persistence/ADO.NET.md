<!-- TOC -->

- [ADO.NET](#adonet)
    - [共享类](#共享类)
    - [常用类及操作](#常用类及操作)
        - [SqlConnection-连接数据库](#sqlconnection-连接数据库)
        - [SqlCommand-数据库命令对象](#sqlcommand-数据库命令对象)
        - [DataReader](#datareader)
        - [DataAdapter](#dataadapter)
        - [参数化SQL语句](#参数化sql语句)
        - [存储过程的调用](#存储过程的调用)
        - [事务的处理](#事务的处理)
    - [SqlHelper类的封装](#sqlhelper类的封装)
        - [基本封装](#基本封装)
        - [分页查询封装](#分页查询封装)
        - [SqlDataReader转换为实体类优化](#sqldatareader转换为实体类优化)
    - [Guid的应用](#guid的应用)
        - [Unique Int64](#unique-int64)
    - [ADO.NET Oracle](#adonet-oracle)
        - [OracleConnection 对象](#oracleconnection-对象)

<!-- /TOC -->
<a id="markdown-adonet" name="adonet"></a>
# ADO.NET
ADO.NET是一组允许.NET开发人员使用标准的，结构化的，甚至无连接的方式与数据交互的技术。

简单来说，ADO.NET就是一种数据访问接口，可以让我们在程序中调用相应的类库对数据库(通常为SQL Server，也可以是access 等其他数据库)进行增删改查等操作。

<a id="markdown-共享类" name="共享类"></a>
## 共享类
无论是使用SQL Server类，还是OLE DB类，都可以使用以下共享类：

![](..\assets\adonet\adonet_class.png)

<a id="markdown-常用类及操作" name="常用类及操作"></a>
## 常用类及操作
- Connection(用于建立与 数据库的连接)
- Command(用于执行SQL语句)
- DataReader(用于读取数据)
- DataAdapter(用于填充把数据填充到DataSet)
- DataSet(数据集，用于程序中)

通常，从程序中访问数据库的基本流程是：
1. 连接并打开数据库
2. 创建并执行命令
3. 如果数据检索的话，分为步进和填充两种方式

<a id="markdown-sqlconnection-连接数据库" name="sqlconnection-连接数据库"></a>
### SqlConnection-连接数据库
首先，要想访问数据库，我们需要一个媒介把程序与数据库连接起来。这就是连接字符串，它的基本语法为：
```cs
//windows验证登录
string connStr = "Data Source=服务器地址;Initial Catalog=数据库名称;Integrated Security=SSPI;";

//SQLServer身份验证登录
string connStr = "Data Source=服务器地址;Initial Catalog=数据库名称;User ID=账号;Password = 密码;";
//或者简写为以下形式，更为推荐此方式
string connStr = "server=服务器地址; database=数据库名称; uid=账号; pwd=密码;";
```

基于本机数据库的连接测试代码：
```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";

//SqlConnection需要引用System.Data.SqlClient;
SqlConnection conn = new SqlConnection(connStr);

try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    //TODO 数据库操作...............

    Console.WriteLine("打开数据库连接成功！");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-sqlcommand-数据库命令对象" name="sqlcommand-数据库命令对象"></a>
### SqlCommand-数据库命令对象

常用属性和方法：

实例属性方法 | 说明
-----|---
Connection | 数据源的连接
CommandType | 设置你执行的SQL语句类型，有三个枚举，分别是Text(SQL文本命令),StoredProcedure(存储过程),TableDirect(表名)。 
CommandText | 用于获取或设置药对数据源之行的SQL语句、表名或存储过程。 
Parameters | 设置T-SQL语句中需要用到的参数。
ExecuteNonQuery() | 返回被SQL语句执行影响的行数（int），主要执行增删改操作。 
ExecuteReader() | 执行SQL或存储过程，返回的是SqlDataReader类型，主要用来查询。 
ExecuteScalar() | 返回执行结果集中的第一行第一列，如果没有数据，则返回NULL。 
CreateParameter() | 创建SqlParameter实例。 

示例：使用SqlCommand进行插入数据
```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";

//SqlConnection需要引用System.Data.SqlClient;
SqlConnection conn = new SqlConnection(connStr);

try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    //可用conn.CreateCommand();代替
    SqlCommand command = new SqlCommand();
    command.Connection = conn;

    //默认值是Text，可以不写
    command.CommandType = System.Data.CommandType.Text;
    command.CommandText = "INSERT INTO TABLE_NAME (FIELD_1,FIELD_2...) VALUES (VALUE_1,VALUE_2...)";
    int res = command.ExecuteNonQuery();

    Console.WriteLine(res);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-datareader" name="datareader"></a>
### DataReader
SqlDataReader是连接相关的，也就是说与数据库的连接一断开就无法读取数据库中的数据，说明查询结果并不是放在程序中，而是放在数据库的服务中。

常用属性方法：

实例属性方法 | 说明
-----|------
HasRows | 判断是否有数据。
FieldCount | 获取读取的列数。
IsClosed | 判断读取的数据流是否关闭。
Read() | 读取数据。
Close() | 关闭DataReader对象
GetOrdinal() | 可以获取指定列名的序列号，int name = dr.GetOrdinal(“name”);
GetName() | 与上面的方法对应，可以通过列号返回列名字。
IsDBNull() | 判断当前读取的数据是否为Null。

通过DataReader在控制台显示查询结果集：
```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
SqlConnection conn = new SqlConnection(connStr);
try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    SqlCommand command = conn.CreateCommand();
    command.CommandText = "SELECT * FROM dbo.T_STUDENT;";

    SqlDataReader reader = command.ExecuteReader();

    //获取当前行的列数
    int colCount = reader.FieldCount;
    while (reader.Read())
    {
        //遍历每一列，打印输出
        for (int i = 0; i < colCount; i++)
        {
            Console.Write(reader[i] + "\t");
        }
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-dataadapter" name="dataadapter"></a>
### DataAdapter
数据适配器，表示用于填充 System.Data.DataSet 和更新 SQL Server 数据库的一组数据命令和一个数据库连接。

常用属性方法：

实例属性方法 | 说明
-----|---
SelectCommand | 获取或设置用于在数据源选择记录的命令
UpdateCommand | 获取或设置用于更新数据源中的记录的命令。
DeleteCommand | 获取或设置用于从数据源中删除记录的命令。
InsertCommand | 获取或设置用于将新记录插入数据源中的命令。
Fill() | 填充数据集。
Update() | 更新数据源。

通过DataAdapter在控制台显示查询结果集，并进行增删改查：
```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
SqlConnection conn = new SqlConnection(connStr);
try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    SqlCommand command = conn.CreateCommand();
    command.CommandText = "SELECT * FROM dbo.T_STUDENT;";

    SqlDataAdapter adapter = new SqlDataAdapter(command);
    DataTable dt = new DataTable();

    //填充数据
    adapter.Fill(dt);
    if (dt != null)
    {
        //遍历数据表中的行
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Console.Write(row[i] + "\t");
            }
            Console.WriteLine();
        }
    }

    /*SqlDataAdapter.Update()作为了解，使用并不多*/
    //自动生成单表命令,SqlCommandBuilder用于将数据改动转换为sql语句来更新数据库，被操作表需要有主键设置
    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
    //修改内存中数据表
    dt.Rows[0]["NAME"] = "孙悟空";

    //添加新行
    DataRow newRow = dt.NewRow();
    newRow[0] = 10;
    newRow[1] = 1;
    newRow[2] = "猪八戒";
    dt.Rows.Add(newRow);
    adapter.Update(dt);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-参数化sql语句" name="参数化sql语句"></a>
### 参数化SQL语句
若想在程序中传递参数给数据库，可以使用SqlParameter。

常用属性方法：

实例属性方法 | 说明
-------|---
ParameterName | 设置参数名
Value | 给参数设置值
Size | 设置参数字节最大长度
SqlDbType | 参数在SQL中的类型
AddWithVlue() | 添加参数
AddRange() | 添加参数数组

简单登录功能，防止SQL注入：
```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
SqlConnection conn = new SqlConnection(connStr);
try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    SqlCommand command = conn.CreateCommand();

    /*
    SELECT * FROM dbo.T_USERS WHERE USERID = 'admin' AND PWD = 'admin';
    实现以上带参数的SQL语句查询
    其中userid变量中含有危险字符(-)
    */
    string userid = "' OR 1=1 --";
    string pwd = "admin";

    /*采用字符串拼接方式，会有SQL注入的危险*/
    /*
    string text = string.Format("SELECT * FROM dbo.T_USERS WHERE USERID = '{0}' AND PWD = '{1}'", userid, pwd);
    command.CommandText = text;
    */

    /*使用SqlParameter方式，避免SQL注入*/
    command.CommandText = "SELECT * FROM dbo.T_USERS WHERE USERID = @USERID AND PWD = @PWD;";
    /*
    SqlParameter[] sqlParams = new SqlParameter[] {
        new SqlParameter("@USERID",userid) ,
        new SqlParameter("@PWD",pwd)
    };
    command.Parameters.AddRange(sqlParams);
    */
    command.Parameters.AddWithValue("@USERID", userid);
    command.Parameters.AddWithValue("@PWD", pwd);

    SqlDataAdapter adapter = new SqlDataAdapter(command);
    DataTable dt = new DataTable();

    //填充数据
    adapter.Fill(dt);
    if (dt != null)
    {
        if (dt.Rows.Count > 0)
        {
            Console.WriteLine("登录成功");
        }
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-存储过程的调用" name="存储过程的调用"></a>
### 存储过程的调用
由于SqlCommand的CommandType属性默认为Txt，需要修改为存储类型才可以进行存储的调用

调用如下存储过程，有参数有返回结果：
```sql
CREATE PROCEDURE SP_TEST
    (
      @INPUT INT ,
      @RES VARCHAR(20) OUTPUT
    )
AS
    BEGIN
        SELECT  @INPUT ,NEWID() ,SYSDATETIME();
        SET @RES = '执行完毕';
    END
```

```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
SqlConnection conn = new SqlConnection(connStr);
try
{
    //如果当前未打开连接，则进行打开连接
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }

    SqlCommand command = conn.CreateCommand();
    //修改为存储类型
    command.CommandType = CommandType.StoredProcedure;
    //存储名称
    command.CommandText = "SP_TEST";
    SqlParameter[] sqlParams = new SqlParameter[] {
        new SqlParameter("@INPUT", 123),
        //需要指定Size大小，否则显示不完全
        new SqlParameter("@RES","") { Direction = ParameterDirection.Output, Size = 20}
    };
    command.Parameters.AddRange(sqlParams);

    DataTable dt = new DataTable();
    SqlDataAdapter adapter = new SqlDataAdapter(command);
    adapter.Fill(dt);

    //打印输出OUTPUT参数
    Console.WriteLine(command.Parameters["@RES"].Value);
    if (dt != null)
    {
        //遍历数据表中的行
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Console.Write(row[i] + "\t");
            }
            Console.WriteLine();
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-事务的处理" name="事务的处理"></a>
### 事务的处理
在ADO.NET中，事务的处理大致如下：
1. 调用Connection 对象的BeginTransaction 方法来标记事务的开始。
2. 将Transaction 对象分配给要执行的Command的Transaction 属性。
3. 执行所需的命令。
4. 调用Transaction 对象的Commit 方法来完成事务，或调用Rollback 方法来取消事务。

```cs
string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
SqlConnection conn = new SqlConnection(connStr);
//如果当前未打开连接，则进行打开连接
if (conn.State != System.Data.ConnectionState.Open)
{
    conn.Open();
}

//启动一个事务
SqlTransaction myTrans = conn.BeginTransaction();
SqlCommand command = conn.CreateCommand();
//设置事务
command.Transaction = myTrans;

try
{
    //先删除课程，可以删除，如未加事务，则课程已经删除但学生并未删除
    command.CommandText = "DELETE FROM [T_COURSE] WHERE ID = 2";
    command.ExecuteNonQuery();

    //再删除关联的学生，表名有误，删除发生异常
    command.CommandText = "DELETE FROM [T_STUDENT_1] WHERE C_ID = 2";
    command.ExecuteNonQuery();

    command.Transaction.Commit();
}
catch (Exception ex)
{
    //发生异常，进行回滚
    command.Transaction.Rollback();
    Console.WriteLine(ex.Message + "\r\n发生异常，已回滚操作");
}
finally
{
    //如果当前连接未关闭，则进行关闭
    if (conn.State != System.Data.ConnectionState.Closed)
    {
        conn.Close();
    }
}
```

<a id="markdown-sqlhelper类的封装" name="sqlhelper类的封装"></a>
## SqlHelper类的封装
SqlHelper文件最初起源于微软，它是一个基于 .NET Framework 的数据库操作组件，封装了所有的关于数据库的操作。

微软SqlHelper链接：http://pan.baidu.com/s/1jIMN38M 密码：c17o

微软还提供了企业开发库[Enterprise Library](https://www.microsoft.com/en-us/download/details.aspx?id=38789)，
官网下载很慢的可以从百度云下载链接：http://pan.baidu.com/s/1nvspnaL 密码：kfwd

<a id="markdown-基本封装" name="基本封装"></a>
### 基本封装
很多时候，我们会自己进行简单的封装：
```cs
public class SqlHelper
{
    /// <summary>
    /// 私有构造函数，不允许在外部进行new实例化操作
    /// </summary>
    private SqlHelper() { }

    //static string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";
    /*
    将数据库连接字符串放在配置文件中是更好的做法, 
    ConfigurationManager类需要在工程中添加System.Configuration dll引用
    */
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
            int res = cmd.ExecuteNonQuery();
            return res;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            SqlDataReader reader = cmd.ExecuteReader();
            List<T> lstRes = new List<T>();
            T element = new T();

            //获取该类型的所有公开属性
            PropertyInfo[] props = typeof(T).GetProperties();

            while (reader.Read())
            {
                element = new T();
                foreach (PropertyInfo p in props)
                {
                    //默认当做属性名称和表字段名称对应，后期可以通过自定义特性进行匹配
                    object obj = reader[p.Name];

                    //设置对应的属性值
                    p.SetValue(element, obj, null);
                }
                lstRes.Add(element);
            }
            return lstRes;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
}
```

<a id="markdown-分页查询封装" name="分页查询封装"></a>
### 分页查询封装
分页查询的封装：
```cs
/// <summary>
/// 分页实体类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pager<T>
{
    /// <summary>
    /// 满足去掉分页条件下的记录总数
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 当前页码下的数据集合
    /// </summary>
    public List<T> Rows { get; set; }
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
```

<a id="markdown-sqldatareader转换为实体类优化" name="sqldatareader转换为实体类优化"></a>
### SqlDataReader转换为实体类优化
通过SqlDataReader类查询数据，并需要返回实体类时，可能会存在一些隐含的问题，比如数据库字段和实体类属性不匹配，数据库字段的值为DbNull等。

为了保证转换过程的正常使用，我们需要做如下改动：
```cs
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
        LogHelper.Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));
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
        //发生异常时记录日志
        LogHelper.Log("发生异常:" + ex.Message, ex.StackTrace);
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
    //connStr 为web.config中数据库连接字符串
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
        //记录日志
        LogHelper.Log("SQL:", cmd.CommandText + "\n" + Params2String(cmd.Parameters));

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
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
                return entity;
            }
            return null;
        }
    }
    catch (Exception ex)
    {
        //发生异常时记录日志
        LogHelper.Log("发生异常:" + ex.Message, ex.StackTrace);

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
/// 判断对象是否为null或是dbnull
/// DbNull较为特殊，使用 obj is DbNull 或是 obj == DbNull.Value进行判断
/// </summary>
/// <param name="obj"></param>
/// <returns></returns>
public static bool IsNullOrDbNull(object obj)
{
    return (obj == null || (obj is DBNull)) ? true : false;
}

/// <summary>
/// 对可空类型进行判断转换，考虑实体类属性可为空的情况
/// </summary>
/// <param name="value">reader中的值</param>
/// <param name="conversionType">实体类属性类型</param>
/// <returns></returns>
public static object CheckType(object value, Type conversionType)
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
```

<a id="markdown-guid的应用" name="guid的应用"></a>
## Guid的应用
<a id="markdown-unique-int64" name="unique-int64"></a>
### Unique Int64
TODO待测试重复性，目前仅测试百万级别，更大数据量有待测试是否会产生重复ID

```cs
class Program
{
    static void Main(string[] args)
    {
        List<long> list = new List<long>();
        Test(100000, list);
        Console.WriteLine(list.GroupBy(t => t).Count());
    }

    static void Test(int cnt, List<long> list)
    {
        int i = 0;
        while (i < cnt)
        {
            i++;
            string text = Guid.NewGuid().ToString();

            list.Add(GetInt64HashCode(text));
        }
    }

    /// <summary>
    /// Return unique Int64 value for input string
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    static Int64 GetInt64HashCode(string strText)
    {
        Int64 hashCode = 0;
        if (!string.IsNullOrEmpty(strText))
        {
            //Unicode Encode Covering all characterset
            byte[] byteContents = Encoding.Unicode.GetBytes(strText);
            System.Security.Cryptography.SHA256 hash =
            new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] hashText = hash.ComputeHash(byteContents);
            //32Byte hashText separate
            //hashCodeStart = 0~7  8Byte
            //hashCodeMedium = 8~23  8Byte
            //hashCodeEnd = 24~31  8Byte
            //and Fold
            Int64 hashCodeStart = BitConverter.ToInt64(hashText, 0);
            Int64 hashCodeMedium = BitConverter.ToInt64(hashText, 8);
            Int64 hashCodeEnd = BitConverter.ToInt64(hashText, 24);
            hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
        }
        return (hashCode);
    }
}
```


<a id="markdown-adonet-oracle" name="adonet-oracle"></a>
## ADO.NET Oracle
<a id="markdown-oracleconnection-对象" name="oracleconnection-对象"></a>
### OracleConnection 对象
要访问一个数据源，你必须先建立一个到它的连接。这个连接里描述了数据库服务器类型、数据库名字、用户名、密码，和连接数据库所需要的其它参数。

command对象通过使用connection对象来知道是在哪个数据库上面执行ORACLE命令。

```cs
// oracle 连接字符串 server指明服务器，data source为oracle实例名称，uid为用户名，pwd为密码
string connStr = "server=172.16.123.250;data source=orcl;uid=hero;pwd=123456;";
// 需要添加 using System.Data.OracleClient;
OracleConnection connOrcl = new OracleConnection(connStr);

try
{
    connOrcl.Open();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    if (connOrcl.State != ConnectionState.Closed)
    {
        connOrcl.Close();
    }
}
```

在上述代码中会提示OracleConnection已过时，可以使用oracle官方推荐dll解决。

在oracle安装路径【X:\app\Administrator\product\11.2.0\client_1\ODP.NET\bin\2.x】找到dll文件【Oracle.DataAccess.dll】，使用oracle推荐的类库进行操作。

```cs
// oracle 连接字符串 Data Source为本机oracle客户端配置的tns连接，user id为用户名，password为密码
string connStr = "Data Source=ORCL;Persist Security Info=True;User ID=hero;Password=123456;";

// 推荐使用oracle推荐的odp.net方式
Oracle.DataAccess.Client.OracleConnection connOrcl = new Oracle.DataAccess.Client.OracleConnection(connStr);

try
{
    connOrcl.Open();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    if (connOrcl.State != ConnectionState.Closed)
    {
        connOrcl.Close();
    }
}
```
