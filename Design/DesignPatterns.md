<!-- TOC -->

- [DesignPatterns](#designpatterns)
    - [设计模式](#设计模式)
    - [Singleton单例模式](#singleton单例模式)
    - [Factory Method工厂方法](#factory-method工厂方法)
        - [Simple Factory简单工厂](#simple-factory简单工厂)
        - [Factory Method工厂方法](#factory-method工厂方法-1)
    - [Abstract Factory模式-抽象工厂](#abstract-factory模式-抽象工厂)
        - [小结](#小结)
    - [Decorator装饰模式](#decorator装饰模式)
        - [接口API的透明性](#接口api的透明性)
    - [Proxy代理模式](#proxy代理模式)

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

简单工厂中我们是通过工厂类中的switch进行判断具体属于什么操作的。

而工厂方法中，我们把switch的部分放在客户端，让调用者判断调用哪个工厂类进行实例化。

以上，工厂方法是简单工厂的进一步抽象和推广。但缺点是，每新增一个产品，就需要一个产品工厂类，增加了额外的开发量。

## Abstract Factory模式-抽象工厂

抽象工厂模式，提供一个创建一系列相关或互相依赖对象的接口，而无需指定它们具体的类。

以周黑鸭为例，上海人偏甜口，湖南人偏辣口，所以两个地方生产的产品虽然都是鸭脖、鸭翅等，但在细节上有一定的区分。

![](..\assets\Design\AbstractFactory.png)

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

利用抽象工厂可以很方便的实现新增产品系列，如上例中新增四川口味的鸭脖和鸭翅。但是如果需要新增产品类型，比如新增鸭锁骨的销售，那么需要修改的地方就比较多了，而且违反了OCP原则。如需要新增鸭锁骨的销售，需要修改的内容见如下伪代码：
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

**不足之处：**
抽象工厂模式很难支持新种类产品的变化。这是因为抽象工厂接口中已经确定了可以被创建的产品集合，如果需要添加新产品，此时就必须去修改抽象工厂的接口，这样就涉及到抽象工厂类的以及所有子类的改变，这样也就违背了“开放—封闭”OCP原则。

### 小结

**简单工厂**

简单工厂模式的工厂类一般是使用静态方法，通过接收的参数的不同来返回不同的对象实例。不修改代码的话，是无法扩展的。

**工厂方法**

工厂方法是针对每一种产品提供一个工厂类。通过不同的工厂实例来创建不同的产品实例。在同一等级结构中，支持增加任意产品。

**抽象工厂**

抽象工厂是应对产品族概念的。比如说，每个汽车公司可能要同时生产轿车，货车，客车，那么每一个工厂都要有创建轿车，货车和客车的方法。

应对产品族概念而生，增加新的产品线很容易，但是无法增加新的产品。

## Decorator装饰模式
装饰器模式（Decorator Pattern）允许向一个现有的对象添加新的功能，同时又不改变其结构。这种类型的设计模式属于结构型模式，它是作为现有的类的一个包装。

**意图**：动态地给一个对象添加一些额外的职责。就增加功能来说，装饰器模式相比生成子类更为灵活。

**主要解决**：一般的，我们为了扩展一个类经常使用继承方式实现，由于继承为类引入静态特征，并且随着扩展功能的增多，子类会很膨胀。

**何时使用**：在不想增加很多子类的情况下扩展类。

**关键代码**： 1、Component 类充当抽象角色，不应该具体实现。 2、修饰类引用和继承 Component 类，具体扩展类重写父类方法。

![](..\assets\Design\Decorator.png)

针对上图中的基本实现代码如下：
```cs
class Program
{
    static void Main(string[] args)
    {
        //实例化具体对象
        Component c1 = new ConcreteComponent();

        //实例化两个装饰器
        Decorator d1 = new ConcreteDecoratorA();
        Decorator d2 = new ConcreteDecoratorB();

        c1.Operation();

        d1.SetComponent(c1);
        d1.Operation();

        d2.SetComponent(d1);
        d2.Operation();

    }
}

/// <summary>
/// 核心对象
/// </summary>
public abstract class Component
{
    public abstract void Operation();
}

public class ConcreteComponent : Component
{
    public override void Operation()
    {
        Console.WriteLine("被装饰对象，具体对象的操作");
    }
}

public abstract class Decorator : Component
{
    protected Component component;

    /// <summary>
    /// 设置component
    /// </summary>
    /// <param name="component"></param>
    public void SetComponent(Component component)
    {
        this.component = component;
    }

    /// <summary>
    /// 重写Operation，实际执行component的Operation
    /// </summary>
    public override void Operation()
    {
        if (component != null)
        {
            component.Operation();
        }
    }

}

/// <summary>
/// 装饰器A
/// </summary>
public class ConcreteDecoratorA : Decorator
{
    public override void Operation()
    {
        base.Operation();
        Console.WriteLine("ConcreteDecoratorA进行装饰");
    }
}

/// <summary>
/// 装饰器B
/// </summary>
public class ConcreteDecoratorB : Decorator
{
    public override void Operation()
    {
        base.Operation();
        AddedBehavior();
        Console.WriteLine("ConcreteDecoratorB进行装饰");
    }

    private void AddedBehavior()
    {
        Console.WriteLine("ConcreteDecoratorB独有方法");
    }
}
```

利用SetComponent对对象进行包装，这样每个装饰对象的实现就和如何使用对象分离，每个装饰对象只关心自己的功能，不需要关心如何被添加到对象链中。

《图解设计模式》中关于Decorator的示例如下：

![](..\assets\Design\DecoratorDisplayStr.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        Display d1 = new StringDisplay("hello world");
        Display d2 = new FullBorder(d1, '~');
        d2.Show();
    }
}

/// <summary>
/// 显示多行字符串抽象类
/// </summary>
public abstract class Display
{
    /// <summary>
    /// 获取横向字符数
    /// </summary>
    /// <returns></returns>
    public abstract int GetColumns();
    /// <summary>
    /// 获取纵向行数
    /// </summary>
    /// <returns></returns>
    public abstract int GetRows();
    /// <summary>
    /// 获取第index行的字符串
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public abstract string GetRowText(int index);
    /// <summary>
    /// 全部显示
    /// </summary>
    public void Show()
    {
        for (int i = 0; i < GetRows(); i++)
        {
            Console.WriteLine(GetRowText(i));
        }
    }
}

/// <summary>
/// 用于显示单行字符串类
/// </summary>
public class StringDisplay : Display
{
    public string Str { get; set; }
    public StringDisplay(string str)
    {
        Str = str;
    }
    public override int GetColumns()
    {
        return Str.Length;
    }

    public override int GetRows()
    {
        //行数为固定的1
        return 1;
    }

    public override string GetRowText(int index)
    {
        //仅有一行，仅当index为0时返回字符串
        if (index == 0)
        {
            return Str;
        }
        else
        {
            return null;
        }
    }
}

/// <summary>
/// 装饰边框抽象类
/// 装饰边框和被装饰物Display有相同的方法
/// </summary>
public abstract class Border : Display
{
    /// <summary>
    /// 被装饰物
    /// </summary>
    protected Display CurDisplay { get; set; }
    protected Border(Display display)
    {
        CurDisplay = display;
    }
}

/// <summary>
/// 左右两侧装饰
/// </summary>
public class SideBorder : Border
{
    /// <summary>
    /// 装饰边框的字符
    /// </summary>
    public char BorderChar { get; set; }
    /// <summary>
    /// 通过构造函数指定display和装饰边框字符
    /// </summary>
    /// <param name="display"></param>
    /// <param name="ch"></param>
    public SideBorder(Display display, char ch) : base(display)
    {
        BorderChar = ch;
    }
    /// <summary>
    /// 字符串长度加上左右边框的长度
    /// </summary>
    /// <returns></returns>
    public override int GetColumns()
    {
        return 1 + CurDisplay.GetColumns() + 1;
    }

    /// <summary>
    /// 左右编程，行数仍保持不变
    /// </summary>
    /// <returns></returns>
    public override int GetRows()
    {
        return CurDisplay.GetRows();
    }

    /// <summary>
    /// 显示时在左右两侧拼接装饰边框
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public override string GetRowText(int index)
    {
        return BorderChar + CurDisplay.GetRowText(index) + BorderChar;
    }
}

/// <summary>
/// 全边框装饰
/// </summary>
public class FullBorder : Border
{
    public char BorderChar { get; set; }
    public FullBorder(Display display, char ch) : base(display)
    {
        BorderChar = ch;
    }
    public override int GetColumns()
    {
        return 1 + CurDisplay.GetColumns() + 1;
    }

    public override int GetRows()
    {
        return 1 + CurDisplay.GetRows() + 1;
    }

    public override string GetRowText(int index)
    {
        if (index == 0)//上边框
        {
            return BorderChar + MakeLine(CurDisplay.GetColumns()) + BorderChar;
        }
        else if (index == CurDisplay.GetRows() + 1)
        {
            return BorderChar + MakeLine(CurDisplay.GetColumns()) + BorderChar;
        }
        else
        {
            return BorderChar + CurDisplay.GetRowText(index - 1) + BorderChar;
        }
    }

    private string MakeLine(int length)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Append(BorderChar);
        }
        return builder.ToString();
    }
}
```

http://blog.csdn.net/qiaoquan3/article/details/78203502

**总结：**
装饰者模式采用对象组合而非继承的方式实现了再运行时动态地扩展对象功能的能力，而且可以根据需要扩展多个功能，避免了单独使用继承带来的 ”灵活性差“和”多子类衍生问题“。同时它很好地符合面向对象设计原则中 ”优先使用对象组合而非继承“和”开放-封闭“原则。

### 接口API的透明性
在Decorator模式中，装饰器和被装饰物具有一致性。如此形成一个对象链。

## Proxy代理模式



参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)


