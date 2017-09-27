<!-- TOC -->

- [ADO.NET](#adonet)
    - [共享类](#共享类)
    - [五大类](#五大类)
        - [SqlConnection-连接数据库](#sqlconnection-连接数据库)
        - [SqlCommand-数据库命名对象](#sqlcommand-数据库命名对象)
        - [DataReader](#datareader)
        - [DataAdapter](#dataadapter)
        - [参数化SQL语句](#参数化sql语句)
        - [存储过程的调用](#存储过程的调用)

<!-- /TOC -->
# ADO.NET
ADO.NET是一组允许.NET开发人员使用标准的，结构化的，甚至无连接的方式与数据交互的技术。

简单来说，ADO.NET就是一种数据访问接口，可以让我们在程序中调用相应的类库对数据库(通常为SQL Server，也可以是access 等其他数据库)进行增删改查等操作。

## 共享类
无论是使用SQL Server类，还是OLE DB类，都可以使用以下共享类：

![](..\assets\adonet\adonet_class.png)

## 五大类
- Connection(用于建立与 数据库的连接)
- Command(用于执行SQL语句)
- DataReader(用于读取数据)
- DataAdapter(用于填充把数据填充到DataSet)
- DataSet(数据集，用于程序中)

通常，从程序中访问数据库的基本流程是：
1. 连接并打开数据库
2. 创建并执行命令
3. 如果数据检索的话，分为步进和填充两种方式

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

### SqlCommand-数据库命名对象

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