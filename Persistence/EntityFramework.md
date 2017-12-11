<!-- TOC -->

- [EntityFramework](#entityframework)
    - [三种开发方式](#三种开发方式)
    - [Database First](#database-first)
        - [EF创建](#ef创建)
    - [EF应用](#ef应用)
        - [Entity Client 方式](#entity-client-方式)
        - [Object Context 方式](#object-context-方式)
        - [Linq to Entities 方式](#linq-to-entities-方式)
        - [添加数据](#添加数据)
        - [修改数据](#修改数据)
        - [删除数据](#删除数据)

<!-- /TOC -->

<a id="markdown-entityframework" name="entityframework"></a>
# EntityFramework
ADO.NET EntityFramework框架是一组内建于ADO.NET的用于支持开发基于数据软件应用的技术。它使得开发人员通过特定的领域对象及属性来使用数据，而不需要关心他们存储在底层数据库时用的表以及列的信息。

EF是一种ORM（Object-relational mapping）框架，它能把我们在编程时使用对象映射到底层的数据库结构。比如，你可以在数据库中建立一个Order表，让它与程序中的Order类建立映射关系，这样一来，程序中的每个Order对象都对应着Order表中的一条记录，ORM框架负责把从数据库传回的记录集转换为对象，也可以依据对象当前所处的具体状态生成相应的SQL命令发给数据库，完成数据的存取工作（常见的数据存取操作可简称为CRUD：Create、Read、Update、Delete）。

<a id="markdown-三种开发方式" name="三种开发方式"></a>
## 三种开发方式
* Code First 先设计实体类，执行后既有底层库表。即先有代码再有数据库。
* Database First 先设计并建好数据库，然后使用VS的向导创建EF数据模型并生成实体类代码。 实用，推荐！！！
* Model First 在可视化设计器中创建实体和它们间的关联，再生成SQL脚本，然后执行SQL脚本完成数据库的创建。不推荐

<a id="markdown-database-first" name="database-first"></a>
## Database First

<a id="markdown-ef创建" name="ef创建"></a>
### EF创建
以我们数据库中现存的数据库为例，创建实体数据模型的步骤如下：

在项目上右键新建项，新增【ADO.NET 实体数据模型】。最好在一个空文件夹中进行创建，否则创建的文件较为杂乱。

![](..\assets\adonet\EF_create1.png)

选择【来自数据库的EF设计器】

![](..\assets\adonet\EF_create2.png)

如果已经存在需要连接数据库的连接可以直接使用。否则可以进行新建连接进行创建，在【连接属性】窗口中填入对应服务器名称、验证方式、数据库名称，如下：

![](..\assets\adonet\EF_create3.png)

设置实体数据模型，如下：

![](..\assets\adonet\EF_create4.png)

项目中会自动生成对应的数据模型，如下：

![](..\assets\adonet\EF_create5.png)

以上，针对Database First这种创建方式就完成了。

<a id="markdown-ef应用" name="ef应用"></a>
## EF应用

<a id="markdown-entity-client-方式" name="entity-client-方式"></a>
### Entity Client 方式
它是 ADO.NET Entity Framework 中的本地用户端 (Native Client)，它的对象模型和 ADO.NET 的其他用户端非常相似：

一样有 Connection, Command, DataReader 等对象，但最大的差异就是，它有自己的 SQL 指令 (Entity SQL)，可以用 SQL 的方式访问 EDM。

实质上，还是把 EDM 当成一个实体数据库。

```cs
// 创建并打开连接
EntityConnection conn = new EntityConnection();

//ARTICLE_DBEntities 对应app.config中的配置
conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ARTICLE_DBEntities"].ConnectionString;
conn.Open();
// 创建并执行命令
EntityCommand cmd = new EntityCommand();
cmd.Connection = conn;
cmd.CommandType = CommandType.Text;
/*
注意此处特别！！！注意以下两点：
1、不支持直接使用 *
2、表名前需要添加EDM名称，此处是 ARTICLE_DBEntities
*/
cmd.CommandText = "SELECT T.ID ,T.ZH_NAME ,T.NAME ,T.PWD ,T.ROLES FROM ARTICLE_DBENTITIES.T_USERS AS T";
EntityDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
// 输出
while (reader.Read())
{
    for (int i = 0; i < reader.FieldCount; i++)
    {
        Console.Write(reader[i].ToString() + "\t");
    }
    Console.WriteLine();
}
// 关闭连接
conn.Close();
```

EF的重点和精华都不在Entity Client 方式，当然也不推荐这种用法，不如封装ADO.NET操作来的畅快自在。

<a id="markdown-object-context-方式" name="object-context-方式"></a>
### Object Context 方式
它是微软在 Entity Client 的上层加上了一个供编程语言直接访问的界面。实质上，是把 EDM 当成对象集合的访问。

DbContext的派生类【`ARTICLE_DBEntities`】相当于一个数据库，之后实例化【`ARTICLE_DBEntities`】就相当于打开了一次数据库，跟数据库建立了一次连接。

![](..\assets\adonet\EF_dbcontext1.png)

自定义构造方法的传参
```cs
//partial class 部分类
public partial class ARTICLE_DBEntities : DbContext
{
    /*
    自定义构造方法 其中的 name=ARTICLE_DBEntities 关联的是 app.config中connectionStrings节点下配置
    */
    public ARTICLE_DBEntities()
        : base("name=ARTICLE_DBEntities")
    {
    }
    
    //....
}
```

通过lambda表达式 读取数据
```cs
//实例化EMD对象ARTICLE_DBEntities，可以看做是一个数据库对象
using (ARTICLE_DBEntities db = new ARTICLE_DBEntities())
{
    /*
    创建一个查询 lambda表达式的方式
    */
    var query = db.v_get_articles
        .Where(t => t.user_name == "w")
        .Select(t => new { uname = t.user_name, uid = t.id, utitle = t.title, ucontent = t.content });

    //遍历打印
    foreach (var item in query)
    {
        Console.WriteLine(item);
    }
}
```

<a id="markdown-linq-to-entities-方式" name="linq-to-entities-方式"></a>
### Linq to Entities 方式
因为EDM 的访问改变为一种对对象集合的访问方式，所以可以利用 LINQ 来访问 EDM。

```cs
//实例化EMD对象ARTICLE_DBEntities，可以看做是一个数据库对象
using (ARTICLE_DBEntities db = new ARTICLE_DBEntities())
{
    /* 创建一个查询 linq方式   
    */
    var query = from t in db.v_get_articles
                where t.user_name == "w"
                select new { a = t.title, b = t.content, c = t.user_name };

    //在调用时，才会真正的执行该查询，即从数据库进行实际查询操作
    foreach (var item in query)
    {
        Console.WriteLine(item);
    }
}
```

<a id="markdown-添加数据" name="添加数据"></a>
### 添加数据
向数据库中添加数据就跟往List<>集合添加数据一样，不过最后需要调用SaveChanges()向数据库保存一下数据。

```cs
//实例化EMD对象ARTICLE_DBEntities，可以看做是一个数据库对象
using (ARTICLE_DBEntities db = new ARTICLE_DBEntities())
{
    var cate = new t_category();
    cate.name = "EntityFramework";

    db.t_category.Add(cate);

    db.SaveChanges();
}
```

<a id="markdown-修改数据" name="修改数据"></a>
### 修改数据
先查询出你要修改的那条数据，之后直接更改其中的值就可以了。以上一节中新添加的数据为示例修改，如下：

```cs
//实例化EMD对象ARTICLE_DBEntities，可以看做是一个数据库对象
using (ARTICLE_DBEntities db = new ARTICLE_DBEntities())
{
    var cate = db.t_category.FirstOrDefault(t => t.name == "EntityFramework");
    if (null != cate)
    {
        cate.name = "EntityFramework_new";
        db.SaveChanges();
    }
}
```

<a id="markdown-删除数据" name="删除数据"></a>
### 删除数据
使用EF删除数据就和在List<>集合中删除元素一样

```cs
//实例化EMD对象ARTICLE_DBEntities，可以看做是一个数据库对象
using (ARTICLE_DBEntities db = new ARTICLE_DBEntities())
{
    var cate = db.t_category.FirstOrDefault(t => t.name == "EntityFramework_new");
    if (null != cate)
    {
        db.t_category.Remove(cate);
        db.SaveChanges();
    }
}
```


参考引用：

[初识EntityFramework6](http://www.cnblogs.com/wujingtao/p/5401132.html)