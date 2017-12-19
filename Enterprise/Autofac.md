#Autofac

Autofac是.NET领域最为流行的IOC框架之一，传说是速度最快的一个。

[Autofac官网](http://autofac.readthedocs.io/en/latest/getting-started/index.html)

在介绍Autofac前，我们需要了解几个重要的概念，DIP,DI,IOC,IOC容器

## 前言
### DIP、IOC、DI、IOC容器
* 依赖倒置原则 DIP(Dependency Inversion Principle)：一种软件架构**设计原则**(抽象概念)。

高层模块不应依赖于低层模块，两者应该依赖于抽象。
抽象不不应该依赖于实现，实现应该依赖于抽象。

* 控制反转 IoC(Inversion of Control)：一种反转流、依赖和接口的方式，一种**设计模式**(DIP的具体实现方式)。

控制反转(IoC)，它为相互依赖的组件提供抽象，将依赖(低层模块)对象的获得交给第三方(系统)来控制，即依赖对象不在被依赖模块的类中直接通过new来获取。

* 依赖注入 DI(Dependency Injection)：IoC的一种实现方式，用来反转依赖(IoC的具体实现方式)。

提供一种机制，将需要依赖(低层模块)对象的引用传递给被依赖(高层模块)对象。又分为构造函数、属性、接口注入三种方式。

* IoC容器：依赖注入的框架，用来映射依赖，管理对象创建和生存周期(DI框架)。

IoC容器实际上是一个DI框架，它能简化我们的工作量。

### 案例引入

我们模拟一个电商网站的下订单操作，当用户下订单的时候需要有入库操作，即保存到数据库，通常我们的实现方式是这样的：
```cs
    /// <summary>
    /// 模拟UI层的调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Order od = new Order();
            od.Add();
        }
    }

    /// <summary>
    /// 模拟订单业务逻辑，相当于业务逻辑层BLL
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 私有变量，保存对数据操作的对象
        /// 即 Order 依赖于 SqlServerDal，上层的功能依赖于底层的功能
        /// 不好的设计，当SqlServerDal发生变化或者不再需要SqlServerDal的时候需要改动业务层Order类!
        /// </summary>
        readonly SqlServerDal dal = new SqlServerDal();

        /// <summary>
        /// 添加订单
        /// </summary>
        public void Add()
        {
            // 业务验证。。。。。
            dal.Add();
        }
    }

    /// <summary>
    /// 模拟SqlServer数据库的操作，相当于数据访问层DAL
    /// 用于数据读写
    /// </summary>
    public class SqlServerDal
    {
        public void Add()
        {
            Console.WriteLine("在数据库中添加一条订单!");
        }
    }
```

上述代码有个很不好的一点，就是当持久化存储方案发生变化的时候，需要改动业务逻辑层代码。

比如，用户需求支持灵活的数据库存储方案，需要灵活支持SqlServer、Oracle、SQLite等等，那么对应的持久化保存方案都有对应的数据访问类，如下：
```cs
public class OracleDal
{
    public void Insert()
    {
        Console.WriteLine("Oracle中保存订单");
    }
}

public class SqLiteDal
{
    public void New()
    {
        Console.WriteLine("SQLite中添加订单");
    }
}
```

对于如上的两个数据访问类，不仅类名不一样，方法名称也不一样，在遇到需求变化时业务层的适配很麻烦，并不是很好的方法。

那么DI所做的操作就是将new()实例化操作放到类的外部来实现，解决Order依赖于SqlServerDal的问题，**就是将依赖对象的创建和绑定转移到被依赖对象类的外部来实现。**

### DI实践
我们以一个简化的例子有一个初步的认识，例如现在有类A依赖于C
```cs
/// <summary>
/// 模拟上层模块，对底层直接依赖
/// 类A直接依赖于B
/// </summary>
public class A
{
    private B _opt = new B();
}

/// <summary>
/// 模拟底层模块
/// </summary>
public class B { }
```

在DI的使用之前，我们首先需要将面向对象编程方式修改为面向接口的编程，如下：
```cs
/// <summary>
/// 模拟上层模块，对底层直接依赖
/// 类A直接依赖于B
/// </summary>
public class A
{
    private ISub _opt = new B();
}

/// <summary>
/// 将底层模块抽象出接口，通过接口进行约束
/// </summary>
public interface ISub { }

/// <summary>
/// 模拟底层模块
/// </summary>
public class B : ISub { }
```

#### 构造函数注入
通过添加上层模块类的自定义构造函数，将依赖通过构造函数传入的方式实现：
```cs
class Program
{
    static void Main(string[] args)
    {
        A obj = new A(new B());
        A obj1 = new A(new C());
    }
}

public class A
{
    private ISub _opt;
    public A(ISub op)
    {
        _opt = op;
    }
}

public interface ISub { }

public class B : ISub { }

public class C : ISub { }
```

#### 属性注入
通过属性的方式，达到引用从外部的传入：
```cs
class Program
{
    static void Main(string[] args)
    {
        A obj = new A();
        obj.Opt = new B();

        A obj1 = new A();
        obj1.Opt = new C();
    }
}

public class A
{
    private ISub _opt;
    public ISub Opt { set { _opt = value; } }
}

public interface ISub { }

public class B : ISub { }

public class C : ISub { }
```
#### 接口注入
具体思路是先定义一个接口，包含一个设置依赖的方法。然后依赖类，继承并实现这个接口。
```cs
class Program
{
    static void Main(string[] args)
    {
        A obj = new A();
        obj.Injection(new B());

        A obj1 = new A();
        obj1.Injection(new C());
    }
}
public interface IDependencyInjection
{
    void Injection(ISub op);
}

public class A : IDependencyInjection
{
    private ISub _opt;
    public void Injection(ISub op)
    {
        _opt = op;
    }
}

public interface ISub { }

public class B : ISub { }

public class C : ISub { }
```

### 总结
对于大型项目来说，相互依赖的组件比较多。如果还用手动的方式，自己来创建和注入依赖的话，显然效率很低，而且往往还会出现不可控的场面。正因如此，IoC容器诞生了。
IoC容器实际上是一个DI框架，它能简化我们的工作量。它包含以下几个功能：
* 动态创建、注入依赖对象。
* 管理对象生命周期。
* 映射依赖关系。

配置文件加反射注入(各种IOC容器框架)：通过配置文件中的Interface或abstract class名和实现的类名，利用反射将Interface或abstract class与其实现实现动态装配完成注入。

## Autofac配置及使用



---

参考引用：

http://www.cnblogs.com/pangxiaoliang/archive/2010/02/23/1670501.html

[IoC 容器和 Dependency Injection 模式](https://files.cnblogs.com/files/xupng/IoC%E5%AE%B9%E5%99%A8%E5%92%8CDependencyInjection%E6%A8%A1%E5%BC%8F.pdf)

[深入理解DIP、IoC、DI以及IoC容器](http://www.cnblogs.com/liuhaorain/p/3747470.html)

[AutoFac文档](http://www.cnblogs.com/wolegequ/archive/2012/06/02/2531945.html)
