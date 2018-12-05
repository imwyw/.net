<!-- TOC -->

- [Dapper](#dapper)
    - [安装](#安装)
    - [Dapper操作](#dapper操作)
        - [强类型集合查询](#强类型集合查询)
        - [动态类型的查询](#动态类型的查询)
        - [增删改](#增删改)

<!-- /TOC -->

<a id="markdown-dapper" name="dapper"></a>
# Dapper

从字面理解，O是Object，对象；R是Relation，关系；M是Mapping，映射。

所以，用一句话概括就是：ORM是一种对象关系映射的技术。

Dapper是一款轻量级ORM工具。
Dapper是一个轻型的ORM类。代码就一个SqlMapper.cs文件，编译后就40K的一个很小的Dll.
Dapper很快，性能优越。Dapper的速度接近与IDataReader，取列表的数据超过了DataTable。
支持多数据库。。Dapper可以在所有Ado.net Providers下工作，包括sqlite, sqlce, firebird, oracle, MySQL, PostgreSQL and SQL Server

开源代码：https://github.com/StackExchange/Dapper

<a id="markdown-安装" name="安装"></a>
## 安装
推荐使用NuGet方式进行安装

![](..\assets\ORM\dapper-nuget.png)

安装后，项目中会自动添加Dapper的引用，

![](..\assets\ORM\dapper-nuget-ref.png)

【packages.config】也会有对应的包配置
```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Dapper" version="1.50.5" targetFramework="net46" />
</packages>
```

<a id="markdown-dapper操作" name="dapper操作"></a>
## Dapper操作
以下所有操作均基于下面数据库连接配置的读取：

```cs
/// <summary>
/// 读取数据库连接配置
/// 需要添加引用 System.Configuration.dll
/// </summary>
static readonly string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
```

基于T_DOG表结构：
```sql
CREATE TABLE T_DOG
    (
      [ID] INT IDENTITY(1, 1) PRIMARY KEY ,
      [NAME] VARCHAR(32) ,
      [WEIGHT] FLOAT ,
      [BIRTH] DATE
    )
```

添加测试数据：
```sql
INSERT INTO dbo.T_DOG ( NAME, WEIGHT, BIRTH ) VALUES  ( '小黑',  3.1,  '2010-1-1')
INSERT INTO dbo.T_DOG ( NAME, WEIGHT, BIRTH ) VALUES  ( '小花',  2.5,  '2010-2-1')
INSERT INTO dbo.T_DOG ( NAME, WEIGHT, BIRTH ) VALUES  ( '小白',  2.2,  '2010-3-1')
INSERT INTO dbo.T_DOG ( NAME, WEIGHT, BIRTH ) VALUES  ( '二狗子',  4,  '2010-4-1')
INSERT INTO dbo.T_DOG ( NAME, WEIGHT, BIRTH ) VALUES  ( '斗牛',  5.7,  '2010-5-1')
```

Dog实体类：
```cs
public class Dog
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public DateTime Birth { get; set; }
}
```

<a id="markdown-强类型集合查询" name="强类型集合查询"></a>
### 强类型集合查询

Dapper 基于IDbConnection接口扩展了Query方法，所以在使用Dapper方法时需要添加Dapper程序集的using引用：
```cs
public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql
    , object param = null, SqlTransaction transaction = null, bool buffered = true)
```

执行查询，返回对应的泛型集合，Dapper会按照属性名称进行映射：
```cs
using (IDbConnection conn = new SqlConnection(connStr))
{
    string sql = @"SELECT ID,NAME,WEIGHT,BIRTH FROM T_DOG WHERE NAME LIKE @NAME ";
    object parameters = new { NAME = "%小%" };
    List<Dog> list = conn.Query<Dog>(sql, parameters).ToList();
}
```

```cs
using (IDbConnection conn = new SqlConnection(connStr))
{
    string sql = @"SELECT ID,NAME,WEIGHT,BIRTH FROM T_DOG WHERE WEIGHT > @WEIGHT ";
    object parameters = new { WEIGHT = 2.5 };
    List<Dog> list = conn.Query<Dog>(sql, parameters).ToList();
}
```

<a id="markdown-动态类型的查询" name="动态类型的查询"></a>
### 动态类型的查询
Query方法若为指定类型，即返回动态类型集合
```cs
public static IEnumerable<dynamic> Query (this IDbConnection cnn, string sql
    , object param = null, SqlTransaction transaction = null, bool buffered = true)
```

```cs
using (IDbConnection conn = new SqlConnection(connStr))
{
    string sql = @"SELECT ID,NAME,WEIGHT,BIRTH FROM T_DOG WHERE WEIGHT > @WEIGHT ";
    object parameters = new { WEIGHT = 2.5 };
    List<dynamic> list = conn.Query(sql, parameters).ToList();
}
```

<a id="markdown-增删改" name="增删改"></a>
### 增删改
这是一个简单的参数化insert，匿名类型对象，跟原始的SqlParameter相比要简单很多
```cs
using (IDbConnection conn = new SqlConnection(connStr))
{
    string sql = @"INSERT INTO T_DOG(NAME,WEIGHT,BIRTH) VALUES(@NAME,@WEIGHT,@BIRTH) ";
    object parameters = new { NAME = "田园守护者", WEIGHT = 2.5, BIRTH = DateTime.Now };
    int res = conn.Execute(sql, parameters);
}
```

Bulk操作，批量化的操作，我们要做的就是将上面这个【对象】变成【对象集合】就可以了。
```cs
using (IDbConnection conn = new SqlConnection(connStr))
{
    string sql = @"INSERT INTO T_DOG(NAME,WEIGHT,BIRTH) VALUES(@NAME,@WEIGHT,@BIRTH) ";
    List<Dog> parameters = new List<Dog>();
    parameters.Add(new Dog()
    { Name = "jack", Weight = 1.2, Birth = new DateTime(2012, 1, 1) });
    parameters.Add(new Dog()
    { Name = "lee", Weight = 1.1, Birth = new DateTime(2012, 1, 2) });
    parameters.Add(new Dog()
    { Name = "rock", Weight = 2.2, Birth = new DateTime(2012, 1, 3) });
    
    int res = conn.Execute(sql, parameters);
}
```

相应的删除、修改也是一样的套路。暂略...



---

参考引用：

https://www.cnblogs.com/huangxincheng/p/5828470.html






