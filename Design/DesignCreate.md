<!-- TOC -->

- [创建型设计模式](#创建型设计模式)
    - [Singleton单例模式-只有一个实例](#singleton单例模式-只有一个实例)
        - [应用场景](#应用场景)
        - [练习-单例改造](#练习-单例改造)
        - [拓展-关于static方法和单例模式：](#拓展-关于static方法和单例模式)
    - [Factory Method工厂方法](#factory-method工厂方法)
        - [Simple Factory简单工厂](#simple-factory简单工厂)
        - [Factory Method工厂方法-将实例的生成交给子类](#factory-method工厂方法-将实例的生成交给子类)
    - [Abstract Factory模式-抽象工厂 将关联零件组装成产品](#abstract-factory模式-抽象工厂-将关联零件组装成产品)
        - [.NET中抽象工厂模式实现](#net中抽象工厂模式实现)
        - [小结](#小结)
            - [对比工厂方法和抽象工厂](#对比工厂方法和抽象工厂)

<!-- /TOC -->
<a id="markdown-创建型设计模式" name="创建型设计模式"></a>
# 创建型设计模式
<a id="markdown-singleton单例模式-只有一个实例" name="singleton单例模式-只有一个实例"></a>
## Singleton单例模式-只有一个实例

从“单例”字面意思上理解为——一个类只有一个实例，所以单例模式也就是保证一个类只有一个实例的一种实现方法。

官方定义：确保一个类只有一个实例,并提供一个全局访问点。

为了帮助大家更好地理解单例模式，大家可以结合下面的类图来进行理解，以及后面也会剖析单例模式的实现思路:

![](../assets/Design/Singleton.png)

```cs
/// <summary>
/// 单例模式的实现
/// </summary>
public class Singleton
{
    // 定义一个静态变量来保存类的实例
    private static Singleton uniqueInstance;

    // 定义私有构造函数，使外界不能创建该类实例
    private Singleton()
    {
    }

    /// <summary>
    /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
    /// </summary>
    /// <returns></returns>
    public static Singleton GetInstance()
    {
        // 如果类的实例不存在则创建，否则直接返回
        if (uniqueInstance == null)
        {
            uniqueInstance = new Singleton();
        }
        return uniqueInstance;
    }
}
```

上面的单例模式的实现在单线程下确实是完美的,然而在多线程的情况下会得到多个Singleton实例。如下所示：

```cs
public class Singleton
{
    private static Singleton uniqueInstance;

    private Singleton()
    {
        Console.WriteLine("产生了新的对象！！！");
    }

    public static Singleton GetInstance()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = new Singleton();
        }
        return uniqueInstance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 多线程集合
        List<Thread> threadList = new List<Thread>();
        for (int i = 0; i < 20; i++)
        {
            // lambda表达式方式，线程添加方法
            threadList.Add(new Thread(() =>
            {
                Singleton.GetInstance();
            }));
        }
        // 启用线程
        foreach (var item in threadList)
        {
            item.Start();
        }
    }
}
```

因为在两个线程同时运行GetInstance方法时，此时两个线程判断(uniqueInstance == null)这个条件时都返回真，

此时两个线程就都会创建Singleton的实例，这样就违背了我们单例模式初衷了，既然上面的实现会运行多个线程执行，

那我们对于多线程的解决方案自然就是使GetInstance方法在同一时间只运行一个线程运行就好了，

也就是我们线程同步的问题了(对于线程同步大家也可以参考我线程同步的文章),具体的解决多线程的代码如下:

```cs
/// <summary>
/// 单例模式的实现
/// </summary>
public class Singleton
{
    // 定义一个静态变量来保存类的实例
    private static Singleton uniqueInstance;

    // 定义一个标识确保线程同步
    private static readonly object locker = new object();

    // 定义私有构造函数，使外界不能创建该类实例
    private Singleton()
    {
    }

    /// <summary>
    /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
    /// </summary>
    /// <returns></returns>
    public static Singleton GetInstance()
    {
        // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
        // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
        // lock语句运行完之后(即线程运行完之后)会对该对象"解锁"
        lock (locker)
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new Singleton();
            }
        }

        return uniqueInstance;
    }
}
```

上面这种解决方案确实可以解决多线程的问题,但是上面代码对于每个线程都会对线程辅助对象locker加锁之后再判断实例是否存在，

对于这个操作完全没有必要的，因为当第一个线程创建了该类的实例之后，

后面的线程此时只需要直接判断(uniqueInstance==null)为假，此时完全没必要对线程辅助对象加锁之后再去判断，

所以上面的实现方式增加了额外的开销，损失了性能，为了改进上面实现方式的缺陷，

我们只需要在lock语句前面加一句(uniqueInstance==null)的判断就可以避免锁所增加的额外开销，

这种实现方式我们就叫它 “双重锁定”，下面具体看看实现代码的：

```cs
/// <summary>
/// 单例模式的实现
/// </summary>
public class Singleton
{
    // 定义一个静态变量来保存类的实例
    private static Singleton uniqueInstance;

    // 定义一个标识确保线程同步
    private static readonly object locker = new object();

    // 定义私有构造函数，使外界不能创建该类实例
    private Singleton()
    {
    }

    /// <summary>
    /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
    /// </summary>
    /// <returns></returns>
    public static Singleton GetInstance()
    {
        // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
        // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
        // lock语句运行完之后(即线程运行完之后)会对该对象"解锁"
        // 双重锁定只需要一句判断就可以了
        if (uniqueInstance == null)
        {
            lock (locker)
            {
                // 如果类的实例不存在则创建，否则直接返回
                if (uniqueInstance == null)
                {
                    uniqueInstance = new Singleton();
                }
            }
        }
        return uniqueInstance;
    }
}
```

<a id="markdown-应用场景" name="应用场景"></a>
### 应用场景
实际应用：
1. Windows的Task Manager（任务管理器）就是很典型的单例模式（这个很熟悉吧），想想看，是不是呢，你能打开两个windows task manager吗？
2. 网站的计数器，一般也是采用单例模式实现，否则难以同步。
3. 应用程序的日志应用，一般都何用单例模式实现，这一般是由于共享的日志文件一直处于打开状态，因为只能有一个实例去操作，否则内容不好追加。
4. Web应用的配置对象的读取，一般也应用单例模式，这个是由于配置文件是共享的资源。
5. 数据库连接池的设计一般也是采用单例模式，因为数据库连接是一种数据库资源。数据库软件系统中使用数据库连接池，主要是节省打开或者关闭数据库连接所引起的效率损耗，这种效率上的损耗还是非常昂贵的，因为何用单例模式来维护，就可以大大降低这种损耗。
6. 多线程的线程池的设计一般也是采用单例模式，这是由于线程池要方便对池中的线程进行控制。
7. 操作系统的文件系统，也是大的单例模式实现的具体例子，一个操作系统只能有一个文件系统。
8. HttpApplication 也是单位例的典型应用。熟悉ASP.Net(IIS)的整个请求生命周期的人应该知道HttpApplication也是单例模式，所有的HttpModule都共享一个HttpApplication实例.

单例模式应用的场景一般发现在以下条件下：
* 资源共享的情况下，避免由于资源操作时导致的性能或损耗等。如上述中的日志文件，应用配置。
* 控制资源的情况下，方便资源之间的互相通信。如线程池等。

<a id="markdown-练习-单例改造" name="练习-单例改造"></a>
### 练习-单例改造
在下面的TicketMaker类中，每次调用GetNextTicketNumber方法都会返回1000，1001，1002...的数列。

我们可以用它生成票的编号或是其他序列号。

在现在该类的实现方式下，我们可以生成多个该类的实例。

请修改代码，运用Singleton模式确保只能生成一个该类的实例。

【非单例模式的TicketMaker.cs】
```cs
public class TicketMaker 
{
    private int ticket = 1000;
    public int GetNextTicketNumber()
    {
        return ticket++;
    }
}
```

【进行单例的改造】
```cs
public class TicketMaker
{
    private TicketMaker(){}
    private int ticket = 1000;
    static TicketMaker ticker;
    static readonly object lockObj = new object();
    public static TicketMaker Ticket
    {
        get
        {
            if (ticker == null)
            {
                ticker = new TicketMaker();
            }
            return ticker;
        }
    }

    public int GetNextTicketNumber()
    {
        return ticket++;
    }
}
```

但是上述代码仍然存在问题，在多线程的情况，仍会有多个实例对象，获取得到的ticket值并不能保证唯一

【多线程的调用】
```cs
static void Main(string[] args)
{
    Thread th1 = new Thread(Test);
    th1.Start();
    Test();
}

/// <summary>
/// 测试调用
/// </summary>
static void Test()
{
    for (int i = 0; i < 10; i++)
    {
        //Thread.Sleep(10);
        Console.WriteLine(TicketMaker.Ticket.GetNextTicketNumber());
    }
}
```

上述演示中并不能保证每次结果都是一致的，所以需要进一步修改单例的实现。

【TicketMaker.cs多线程的考虑】
```cs
public class TicketMaker
{
    private TicketMaker(){}
    private int ticket = 1000;
    static TicketMaker ticker;
    static readonly object lockObj = new object();
    public static TicketMaker Ticket
    {
        get
        {
            考虑多线程，在多线程的情况下仍为单例
            if (ticker == null)
            {
               lock (lockObj)
               {
                   if (ticker == null)
                   {
                       ticker = new TicketMaker();
                   }
               }
            }
            return ticker;
        }
    }

    public int GetNextTicketNumber()
    {
        // 需要保证多个线程访问过程中不出现重复编号
        lock(lockObj)
        {
            // 将线程挂起模拟过程，先lock后挂起，否则无法保证编号顺序
            Thread.Sleep(10);
            return ticket++;
        }
    }

}
```

<a id="markdown-拓展-关于static方法和单例模式" name="拓展-关于static方法和单例模式"></a>
### 拓展-关于static方法和单例模式：
[静态方法和实例化方法的区别](http://www.cnblogs.com/chinhr/archive/2008/04/03/1135561.html)

<a id="markdown-factory-method工厂方法" name="factory-method工厂方法"></a>
## Factory Method工厂方法

<a id="markdown-simple-factory简单工厂" name="simple-factory简单工厂"></a>
### Simple Factory简单工厂
引用大话设计模式中的计算器示例：

![](../assets/Design/SimpleFactory.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        //通过简单工厂返回运算类对象，调用方法得到运算结果
        Operation op = OperationFactory.CreateOperate("+");
        op.NumberA = 1.23;
        op.NumberB = 7.65;
        Console.WriteLine(op.GetResult());
    }
}

/// <summary>
/// 运算类
/// </summary>
public class Operation
{
    public double NumberA { get; set; }
    public double NumberB { get; set; }
    /// <summary>
    /// 获取运算结果。虚方法，子类可以重写
    /// </summary>
    /// <returns></returns>
    public virtual double GetResult()
    {
        return 0;
    }
}

/// <summary>
/// 简单工厂类，获取具体操作类型
/// </summary>
public class OperationFactory
{
    public static Operation CreateOperate(string operate)
    {
        Operation opt = null;
        switch (operate)
        {
            case "+":
                opt = new OperationAdd();
                break;
            case "-":
                opt = new OperationSub();
                break;
        }
        return opt;
    }
}

/// <summary>
/// 加法类
/// </summary>
public class OperationAdd : Operation
{
    public override double GetResult()
    {
        return NumberA + NumberB;
    }
}

/// <summary>
/// 减法类
/// </summary>
public class OperationSub : Operation
{
    public override double GetResult()
    {
        return NumberA - NumberB;
    }
}

//...后续运算省略...
```

简单工厂最大的优点在于工厂类中包含了必要的逻辑判断，根据客户端的选择条件动态实例化相关类，对于客户端来说，去除了与具体产品的依赖。

<a id="markdown-factory-method工厂方法-将实例的生成交给子类" name="factory-method工厂方法-将实例的生成交给子类"></a>
### Factory Method工厂方法-将实例的生成交给子类

对于上面简单工厂的示例代码来说，Main方法中并不关心具体调用哪个类的实例，只要传入约定的操作符(+-*/)即可得到运算结果。

但如果需要新增运算类型的话，需要修改到原有的【简单工厂类】，违背了开放-封闭原则。

**开放-封闭原则OCP**：软件实体(类、模块、函数等)应该可以扩展，但不可以修改。对于扩展是开放的，对于更改已有的类、模块、函数等是封闭的。

针对简单工厂的扩展，诞生了工厂方法，即定义一个用于创建对象的接口，让工厂类决定实例化哪一个类。

工厂方法使一个类的实例化延迟到其子类中。

![](../assets/Design/FactoryMethod.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        IFactory fac = new AddFactory();
        Operation op = fac.CreateOperate();

        op.NumberA = 1.23;
        op.NumberB = 7.65;
        Console.WriteLine(op.GetResult());
    }
}

/// <summary>
/// 运算类
/// </summary>
public class Operation
{
    public double NumberA { get; set; }
    public double NumberB { get; set; }
    /// <summary>
    /// 获取运算结果。虚方法，子类可以重写
    /// </summary>
    /// <returns></returns>
    public virtual double GetResult()
    {
        return 0;
    }
}

/// <summary>
/// 工厂接口，工厂类需要实现的方法
/// </summary>
public interface IFactory
{
    Operation CreateOperate();
}

/// <summary>
/// 加法类
/// </summary>
public class OperationAdd : Operation
{
    public override double GetResult()
    {
        return NumberA + NumberB;
    }
}

/// <summary>
/// 减法类
/// </summary>
public class OperationSub : Operation
{
    public override double GetResult()
    {
        return NumberA - NumberB;
    }
}

/// <summary>
/// 加法工厂类
/// </summary>
public class AddFactory : IFactory
{
    public Operation CreateOperate()
    {
        return new OperationAdd();
    }
}

/// <summary>
/// 除法工厂类
/// </summary>
public class SubFactory : IFactory
{
    public Operation CreateOperate()
    {
        return new OperationSub();
    }
}
```

当增加新的运算时，不需要修改原有的工厂类。

比如新增乘法操作，新增OperationMul乘法类，新增MulFactory乘法工厂即可，在客户端的调用如下：
```cs
IFactory mulFt = new MulFactory();
Operation optMul = mulFt.CreateOperate();

optMul.NumberA = 7;
optMul.NumberB = 8;
Console.WriteLine(optMul.GetResult());
```

工厂方法符合OCP开放-封闭原则，但面对需求的更改，不可能不修改代码(此说法并不绝对)，关键考虑怎么修改，修改量等因素。

简单工厂中我们是通过工厂类中的switch进行判断具体属于什么操作的。

而工厂方法中，我们把switch的部分去除，让调用者来决定进行调用哪个工厂类进行实例化。

以上，工厂方法是简单工厂的进一步抽象和推广。

但缺点是，每新增一个产品，就需要一个产品工厂类，增加了额外的开发量。

<a id="markdown-abstract-factory模式-抽象工厂-将关联零件组装成产品" name="abstract-factory模式-抽象工厂-将关联零件组装成产品"></a>
## Abstract Factory模式-抽象工厂 将关联零件组装成产品

抽象工厂模式，提供一个创建一系列相关或互相依赖对象的接口，而无需指定它们具体的类。

以周黑鸭为例，上海人偏甜口，湖南人偏辣口，所以两个地方生产的产品虽然都是鸭脖、鸭翅等，但在细节上有一定的区分。

![](../assets/Design/AbstractFactory.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        ZhouDuckFactory shanghaiFt = new ShangHaiFactory();
        Yabo shYabo = shanghaiFt.CreateYabo();
        shYabo.Show();

        ZhouDuckFactory hunanFt = new HuNanFactory();
        Yabo hnYabo = hunanFt.CreateYabo();
        hnYabo.Show();

    }
}

/// <summary>
/// 鸭脖抽象基类，注意和鸭翅是不同产品
/// </summary>
public abstract class Yabo
{
    public abstract void Show();
}

/// <summary>
/// 鸭翅抽象基类，注意和鸭脖是不同产品
/// </summary>
public abstract class Yachi
{
    public abstract void Show();
}

/// <summary>
/// 上海鸭脖
/// </summary>
public class ShangHaiYabo : Yabo
{
    public override void Show() { Console.WriteLine("偏甜鸭脖"); }
}

/// <summary>
/// 上海鸭翅
/// </summary>
public class ShangHaiYachi : Yachi
{
    public override void Show() { Console.WriteLine("偏甜鸭翅"); }
}

/// <summary>
/// 湖南鸭脖
/// </summary>
public class HuNanYabo : Yabo
{
    public override void Show() { Console.WriteLine("狠辣鸭脖"); }
}

/// <summary>
/// 湖南鸭翅
/// </summary>
public class HuNanYachi : Yachi
{
    public override void Show() { Console.WriteLine("狠辣鸭翅"); }
}

/// <summary>
/// 周黑鸭的抽象工厂
/// </summary>
public abstract class ZhouDuckFactory
{
    public abstract Yabo CreateYabo();

    public abstract Yachi CreateYachi();
}

/// <summary>
/// 上海工厂
/// </summary>
public class ShangHaiFactory : ZhouDuckFactory
{
    public override Yabo CreateYabo()
    {
        return new ShangHaiYabo();
    }

    public override Yachi CreateYachi()
    {
        return new ShangHaiYachi();
    }
}

/// <summary>
/// 湖南工厂
/// </summary>
public class HuNanFactory : ZhouDuckFactory
{
    public override Yabo CreateYabo()
    {
        return new HuNanYabo();
    }

    public override Yachi CreateYachi()
    {
        return new HuNanYachi();
    }
}
```

现在考虑，周黑鸭在四川授权销售，则对应的新增代码需要有：
```cs
//新增四川口味的鸭脖和鸭翅产品
public class SiChuanYabo : Yabo
{
    public override void Show() { Console.WriteLine("麻辣鸭脖"); }
}
public class SiChuanYachi : Yachi
{
    public override void Show() { Console.WriteLine("麻辣鸭翅"); }
}

//新增四川具体工厂类，实现四川产品的实例化
public class SiChuanFactory : ZhouDuckFactory
{
    public override Yabo CreateYabo()
    {
        return new SiChuanYabo();
    }

    public override Yachi CreateYachi()
    {
        return new SiChuanYachi();
    }
}

//客户端调用时
ZhouDuckFactory sichuanFt = new SiChuanFactory();
Yabo scYb = sichuanFt.CreateYabo();
scYb.Show();
```

利用抽象工厂可以很方便的实现新增产品系列，如上例中新增四川口味的鸭脖和鸭翅。

但是如果需要新增产品类型，比如新增鸭锁骨的销售，那么需要修改的地方就比较多了，而且违反了OCP原则。

如需要新增鸭锁骨的销售，需要修改的内容见如下伪代码：
```cs
//1、新增鸭锁骨抽象类
public abstract class Yasuogu{ //... }

//2、修改现有的抽象工厂类
public abstract class ZhouDuckFactory
{
    //新增产品
    public abstract Yasuogu CreateYasuogu();
}

//3、实现抽象工厂派生类中的新增抽象方法。。。
```

<a id="markdown-net中抽象工厂模式实现" name="net中抽象工厂模式实现"></a>
### .NET中抽象工厂模式实现
抽象工厂模式在实际中的应用也是相当频繁的，然而在我们.NET类库中也存在应用抽象工厂模式的类。

这个类就是System.Data.Common.DbProviderFactory，这个类位于System.Data.dll程序集中，

该类扮演抽象工厂模式中抽象工厂的角色，我们可以用reflector反编译工具查看该类的实现：

```cs
/// <summary>
/// 扮演抽象工厂的角色
/// 创建连接数据库时所需要的对象集合，
/// 这个对象集合包括有 DbConnection对象(这个是抽象产品类,如绝味例子中的YaBo类)、DbCommand类、DbDataAdapter类，
/// 针对不同的具体工厂都需要实现该抽象类中方法，
/// </summary>
public abstract class DbProviderFactory
{
    // 提供了创建具体产品的接口方法
    protected DbProviderFactory();
    public virtual DbCommand CreateCommand();
    public virtual DbCommandBuilder CreateCommandBuilder();
    public virtual DbConnection CreateConnection();
    public virtual DbConnectionStringBuilder CreateConnectionStringBuilder();
    public virtual DbDataAdapter CreateDataAdapter();
    public virtual DbDataSourceEnumerator CreateDataSourceEnumerator();
    public virtual DbParameter CreateParameter();
    public virtual CodeAccessPermission CreatePermission(PermissionState state);
}
```

DbProviderFactory类是一个抽象工厂类，该类提供了创建数据库连接时所需要的对象集合的接口，

实际创建的工作在其子类工厂中进行，微软使用的是SQL Server数据库，

因此提供了连接SQL Server数据的具体工厂实现，具体代码可以用反编译工具查看，具体代码如下：

```cs
/// 扮演着具体工厂的角色，用来创建连接SQL Server数据所需要的对象
public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
{
    // Fields
    public static readonly SqlClientFactory Instance = new SqlClientFactory();

   // 构造函数
    private SqlClientFactory()
    {
    }
    
   // 重写抽象工厂中的方法
    public override DbCommand CreateCommand()
    {  // 创建具体产品
        return new SqlCommand();
    }

    public override DbCommandBuilder CreateCommandBuilder()
    {
        return new SqlCommandBuilder();
    }

    public override DbConnection CreateConnection()
    {
        return new SqlConnection();
    }

    public override DbConnectionStringBuilder CreateConnectionStringBuilder()
    {
        return new SqlConnectionStringBuilder();
    }

    public override DbDataAdapter CreateDataAdapter()
    {
        return new SqlDataAdapter();
    }

    public override DbDataSourceEnumerator CreateDataSourceEnumerator()
    {
        return SqlDataSourceEnumerator.Instance;
    }

    public override DbParameter CreateParameter()
    {
        return new SqlParameter();
    }

    public override CodeAccessPermission CreatePermission(PermissionState state)
    {
        return new SqlClientPermission(state);
    }
}
```
因为微软只给出了连接SQL Server的具体工厂的实现，我们也可以自定义连接Oracle、MySql的具体工厂的实现。

**不足之处：**
抽象工厂模式很难支持新种类产品的变化。

这是因为抽象工厂接口中已经确定了可以被创建的产品集合，如果需要添加新产品，此时就必须去修改抽象工厂的接口，

这样就涉及到抽象工厂类的以及所有子类的改变，这样也就违背了“开放—封闭”OCP原则。

<a id="markdown-小结" name="小结"></a>
### 小结

**简单工厂**

简单工厂模式的工厂类一般是使用静态方法，通过接收的参数的不同来返回不同的对象实例。不修改代码的话，是无法扩展的。

**工厂方法**

工厂方法是针对每一种产品提供一个工厂类。通过不同的工厂实例来创建不同的产品实例。在同一等级结构中，支持增加任意产品。

**抽象工厂**

抽象工厂是应对产品族概念的。比如说，每个汽车公司可能要同时生产轿车，货车，客车，那么每一个工厂都要有创建轿车，货车和客车的方法。

应对产品族概念而生，增加新的产品线很容易，但是无法增加新的产品。


<a id="markdown-对比工厂方法和抽象工厂" name="对比工厂方法和抽象工厂"></a>
#### 对比工厂方法和抽象工厂

工厂方法：
1. 一个抽象产品类，可以派生出多个具体产品类。 
2. 一个抽象工厂类，可以派生出多个具体工厂类。 
3. 每个具体工厂类只能创建一个具体产品类的实例。

抽象工厂：
1. 多个抽象产品类，每个抽象产品类可以派生出多个具体产品类。
2. 一个抽象工厂类，可以派生出多个具体工厂类。 
3. 每个具体工厂类可以创建多个具体产品类的实例。

对比发现：
1. 工厂方法模式只有一个抽象产品类，而抽象工厂模式有多个。 
2. 工厂方法模式的具体工厂类只能创建一个具体产品类的实例，而抽象工厂模式可以创建多个。 

---

参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)


