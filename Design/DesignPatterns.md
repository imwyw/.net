<!-- TOC -->

- [DesignPatterns](#designpatterns)
    - [设计模式](#设计模式)
    - [Singleton单例模式](#singleton单例模式)
    - [Factory Method工厂方法](#factory-method工厂方法)
        - [Simple Factory简单工厂](#simple-factory简单工厂)
        - [Factory Method工厂方法](#factory-method工厂方法-1)
    - [Abstract Factory模式-抽象工厂](#abstract-factory模式-抽象工厂)

<!-- /TOC -->
# DesignPatterns
## 设计模式
模式设计（Design pattern）是一套被反复使用、多数人知晓的、经过分类编目的、代码设计经验的总结。使用设计模式是为了可重用代码、让代码更容易被他人理解、保证代码可靠性。 毫无疑问，设计模式于己于他人于系统都是多赢的，设计模式使代码编制真正工程化，设计模式是软件工程的基石，如同大厦的一块块砖石一样。

## Singleton单例模式
说到单例模式,大家第一反应应该就是——什么是单例模式？，从“单例”字面意思上理解为——一个类只有一个实例，所以单例模式也就是保证一个类只有一个实例的一种实现方法罢了(设计模式其实就是帮助我们解决实际开发过程中的方法, 该方法是为了降低对象之间的耦合度,然而解决方法有很多种,所以前人就总结了一些常用的解决方法为书籍,从而把这本书就称为设计模式)，下面给出单例模式的一个官方定义：确保一个类只有一个实例,并提供一个全局访问点。为了帮助大家更好地理解单例模式,大家可以结合下面的类图来进行理解,以及后面也会剖析单例模式的实现思路:

![](..\assets\Design\Singleton.png)

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

上面的单例模式的实现在单线程下确实是完美的,然而在多线程的情况下会得到多个Singleton实例,因为在两个线程同时运行GetInstance方法时，此时两个线程判断(uniqueInstance ==null)这个条件时都返回真，此时两个线程就都会创建Singleton的实例，这样就违背了我们单例模式初衷了，既然上面的实现会运行多个线程执行，那我们对于多线程的解决方案自然就是使GetInstance方法在同一时间只运行一个线程运行就好了，也就是我们线程同步的问题了(对于线程同步大家也可以参考我线程同步的文章),具体的解决多线程的代码如下:

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
        // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
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

上面这种解决方案确实可以解决多线程的问题,但是上面代码对于每个线程都会对线程辅助对象locker加锁之后再判断实例是否存在，对于这个操作完全没有必要的，因为当第一个线程创建了该类的实例之后，后面的线程此时只需要直接判断（uniqueInstance==null）为假，此时完全没必要对线程辅助对象加锁之后再去判断，所以上面的实现方式增加了额外的开销，损失了性能，为了改进上面实现方式的缺陷，我们只需要在lock语句前面加一句（uniqueInstance==null）的判断就可以避免锁所增加的额外开销，这种实现方式我们就叫它 “双重锁定”，下面具体看看实现代码的：

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
        // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
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

* 拓展-关于static方法和单例模式：
[静态方法和实例化方法的区别](http://www.cnblogs.com/chinhr/archive/2008/04/03/1135561.html)

## Factory Method工厂方法
### Simple Factory简单工厂
引用大话设计模式中的计算器示例：

![](..\assets\Design\SimpleFactory.png)

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

### Factory Method工厂方法

对于上面简单工厂的示例代码来说，Main方法中并不关心具体调用哪个类的实例，只要传入约定的操作符(+-*/)即可得到运算结果。但如果需要新增运算类型的话，需要修改到原有的工厂类，违背了开放-封闭原则。

**开放-封闭原则OCP**：软件实体(类、模块、函数等)应该可以扩展，但不可以修改。对于扩展是开放的，对于更改已有的类、模块、函数等是封闭的。

针对简单工厂的扩展，诞生了工厂方法，即定义一个用于创建对象的接口，让工厂类决定实例化哪一个类。工厂方法使一个类的实例化延迟到其子类中。

![](..\assets\Design\FactoryMethod.png)

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

当增加新的运算时，不需要修改原有的工厂类。比如新增乘法操作，新增OperationMul乘法类，新增MulFactory乘法工厂即可，在客户端的调用如下：
```cs
IFactory mulFt = new MulFactory();
Operation optMul = mulFt.CreateOperate();

optMul.NumberA = 7;
optMul.NumberB = 8;
Console.WriteLine(optMul.GetResult());
```

工厂方法符合OCP开放-封闭原则，但面对需求的更改，不可能不修改代码(此说法并不绝对)，关键考虑怎么修改，修改量等因素。

简单工厂中我们是通过工厂类中的case when进行判断具体属于什么操作的。

而工厂方法中，我们把case when的部分放在客户端，让调用者判断调用哪个工厂类进行实例化。

以上，工厂方法是简单工厂的进一步抽象和推广。但缺点是，每新增一个产品，就需要一个产品工厂类，增加了额外的开发量。

## Abstract Factory模式-抽象工厂



参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)


