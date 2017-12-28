<!-- TOC -->

- [EntityFramework](#entityframework)
    - [三种开发方式](#三种开发方式)
    - [Database First](#database-first)
        - [EF创建](#ef创建)
    - [更新模型](#更新模型)
    - [EF应用](#ef应用)
        - [Entity Client 方式](#entity-client-方式)
        - [Object Context 方式](#object-context-方式)
        - [Linq to Entities 方式](#linq-to-entities-方式)
        - [添加数据](#添加数据)
        - [修改数据](#修改数据)
        - [删除数据](#删除数据)
        - [事务](#事务)
    - [在EntityFramework6中执行SQL语句](#在entityframework6中执行sql语句)
        - [ExecuteSqlCommand](#executesqlcommand)
        - [SqlQuery](#sqlquery)
        - [DbSet下的SqlQuery](#dbset下的sqlquery)

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

<a id="markdown-更新模型" name="更新模型"></a>
## 更新模型

以Database First为例，当底层库表结构发生变化时，需要更新模型，操作也很简单，在Diagram界面右键选择【从数据库更新模型...】即可，如下：

![](..\assets\adonet\EF_update_model.png)

完成更新后，就会将底层最新的库表结构转换为实体类。

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
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    /*
    创建一个查询 lambda表达式的方式。
    可以理解为是一个SQL的封装，并没有执行查询
    */
    var query = context.v_get_articles
        .Where(t => t.cate_id == 2)
        .Select(t =>
            new
            {
                uname = t.user_name,
                uid = t.id,
                utitle = t.title,
                ucontent = t.content
            }
        );

    // ToList 将查询执行，并返回集合
    var res1 = context.t_users.Where(t => t.name == "w").ToList();

    //遍历打印 才会调用query查询
    foreach (var item in query)
    {
        //Console.WriteLine(item.title + "\t" + item.content);
        Console.WriteLine(item);
    }
```

<a id="markdown-linq-to-entities-方式" name="linq-to-entities-方式"></a>
### Linq to Entities 方式
因为EDM 的访问改变为一种对对象集合的访问方式，所以可以利用 LINQ 来访问 EDM。

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    /* 创建一个查询 linq方式   
    */
    var query = from t in context.v_get_articles
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
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    var cate = new t_category();
    cate.name = "EntityFramework";

    context.t_category.Add(cate);

    context.SaveChanges();
}
```

<a id="markdown-修改数据" name="修改数据"></a>
### 修改数据
先查询出你要修改的那条数据，之后直接更改其中的值就可以了。以上一节中新添加的数据为示例修改，如下：

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    var cate = context.t_category.FirstOrDefault(t => t.name == "EntityFramework");
    if (null != cate)
    {
        cate.name = "EntityFramework_new";
        context.SaveChanges();
    }
}
```

<a id="markdown-删除数据" name="删除数据"></a>
### 删除数据
使用EF删除数据就和在List<>集合中删除元素一样

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    var cate = context.t_category.FirstOrDefault(t => t.name == "EntityFramework_new");
    if (null != cate)
    {
        context.t_category.Remove(cate);
        context.SaveChanges();
    }
}
```

<a id="markdown-事务" name="事务"></a>
### 事务
在EF使用事务有两种方案，一种是EF自带的.BeginTransaction()方法，另一种是使用TransactionScope类。

使用BeginTransaction：
```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    //开始事务
    var tran = context.Database.BeginTransaction();

    try
    {
        var user = context.t_users.FirstOrDefault(t => t.name == "w");
        context.t_users.Remove(user);

        context.SaveChanges();

        //进行提交，否则开始事务后的操作在数据库并不会生效
        tran.Commit();
    }
    catch (Exception ex)
    {
        //发生异常则进行回滚
        tran.Rollback();
    }
}
```

使用TransactionScope，前提需要引用System.Transactions.dll，如下：
```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    //开始事务，有异常发生会跳出using TransactionScope代码块，TransactionScope 将自行释放并回滚该事务。
    using (var trans = new System.Transactions.TransactionScope())
    {
        var user = context.t_users.FirstOrDefault(t=>t.name=="w");
        context.t_users.Remove(user);

        context.SaveChanges();

        //提交操作
        trans.Complete();
    }
}
```

<a id="markdown-在entityframework6中执行sql语句" name="在entityframework6中执行sql语句"></a>
## 在EntityFramework6中执行SQL语句
前面的内容除了Entity Client 方式我们了SQL脚本，其余方式我们都没有写任何SQL脚本。

但不可避免的需要在EF中写SQL，比如需要根据条件更新/删除数据，使用集合Predicate方式就显得效率低下并且麻烦了。

使用EF执行SQL又比ADO.NET方便，特别是在执行查询语句的时候，EF会把查询到的数据自动保存到数据实体中，省去了使用DataReader的麻烦。同时查询出来的数据还会进行跟踪，如果你修改了查询出的值，之后就可以很方便的使用.SaveChanges()直接更新到数据库了。

在数据上下文DBModel的实例中有个Database属性，其中有两组方法.ExecuteSqlCommand()和.SqlQuery()。它们都可以执行SQL语句。

<a id="markdown-executesqlcommand" name="executesqlcommand"></a>
### ExecuteSqlCommand
ExecuteSqlCommand()是不返回结果的，只返回受影响的行数，所以.ExecuteSqlCommand()更适合执行创建、更新、删除操作。

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    //同步的方式执行SQL，并返回受影响的行数
    int result = context.Database.ExecuteSqlCommand(@"CREATE TABLE `test`.`test` (
        `id` INT NOT NULL,
        PRIMARY KEY(`id`)); ");

    //使用SqlParameter传值可以避免SQL注入
    var p_name = new SqlParameter("@name", "萝莉");
    var p_age = new SqlParameter("@age", 13);

    //更改学生年龄
    result = context.Database.ExecuteSqlCommand(@"UPDATE `test`.`student`
                                SET `age` = @age
                                WHERE `name` = @name;", p_age, p_name);

    //异步的方式执行SQL，并返回受影响的行数
    Task<int> result2 = context.Database.ExecuteSqlCommandAsync("DROP TABLE `test`.`test`;");
}
```

如果需要创建或删除当前数据库，Database属性中还存在.Create() 和.Delete()方法，它们不接受参数，返回一个bool值表示执行成功或失败。

<a id="markdown-sqlquery" name="sqlquery"></a>
### SqlQuery
SqlQuery()返回查询到的结果，并将结果保存在数据实体中，所以更适合执行查询操作。

从名字就看的出来.SqlQuery()是用来执行查询的。.SqlQuery()使用前需指定返回值的数据类型，比如我查询寻一条学生的完整信息，类型就可以指定为student类型。如果是统计有多少个学生，返回值是个整数，就以设置为int。

**注意：不仅返回值的个数必须与传入类型中属性值的个数相同，而且名称还必须一样，不然会出错。**那么如果我只想获取姓名和年龄，那就得单独定义一个类（其中包含一个string类型的name和int类型的age），来保存数据了。

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    /*
    按条件返回集合，以下两种方式均可。
    注意，如果没有ToList()，查询并不会执行
    */
    var list = context.Database.SqlQuery<t_users>("SELECT * FROM T_USERS").ToList();

    /*
    按条件查询返回实体，如果没有符合条件的数据，则返回为null
    注意，如果没有FirstOrDefault()的调用，查询并不会执行
    */
    var res1 = context.Database.SqlQuery<t_users>("SELECT  * FROM T_USERS WHERE NAME = 'www'").FirstOrDefault();

    /*
    统计数目
    注意，如果没有FirstOrDefault()的调用，查询并不会执行
    */
    var result2 = context.Database.SqlQuery<int>("SELECT  COUNT(*) FROM T_USERS");
    Console.WriteLine(result2.FirstOrDefault());

    /*
    按条件返回自定义对象
    注意，如果没有FirstOrDefault()的调用，查询并不会执行
    */
    var result3 = context.Database.SqlQuery<SimpleUser>("SELECT NAME,PWD FROM T_USERS").ToList();

    /*
    SqlQuery同样也支持 SqlParameter传参，以防止sql注入危险
    */
    //使用SqlParameter传值可以避免SQL注入
    var sqlpams = new SqlParameter[] { 
        new SqlParameter("@NAME", "admin");
    };
    var result4 = context.Database.SqlQuery<SimpleUser>("SELECT NAME,PWD FROM T_USERS WHERE NAME = @NAME", sqlpams).ToList();
}
```

<a id="markdown-dbset下的sqlquery" name="dbset下的sqlquery"></a>
### DbSet下的SqlQuery
在每个数据实体集合DbSet<T>下也有一个.SqlQuery()，功能与上面介绍的一样，只不过DbSet<T>下的.SqlQuery()只能返回DbSet<T>中包含的类型。

但DbSet<T>下的.SqlQuery()在返回数据的同时还会让数据库上下文（DBModel）跟踪返回数据的状态，如果返回的数据发生了修改，就可以使用.SaveChanges()将结果直接保存回数据库。而.Database.SqlQuery()查出的结果则是做不到的。

```cs
//实例化EMD对象ARTICLE_DBEntities，数据库上下文
using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
{
    //查询一个用户实体
    t_users user = context.t_users.SqlQuery("SELECT * FROM T_USERS WHERE NAME = 'w'").FirstOrDefault();

    //存在该用户则进行修改
    if (null != user)
    {
        //实体集合下获取数据，修改后是可以再保存到数据库的
        user.zh_name = "悔创阿里杰克马";

        context.SaveChanges();
    }
}
```

---

参考引用：

[初识EntityFramework6](http://www.cnblogs.com/wujingtao/p/5401132.html)

[.NET Entity Framework(EF)使用SqlQuery直接操作SQL查询语句或者执行过程](https://www.ofnhkb1.com/chenmo/128.html)