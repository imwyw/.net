<!-- TOC -->

- [结构型设计模式](#结构型设计模式)
    - [Decorator装饰模式-装饰边框与被装饰物的一致性](#decorator装饰模式-装饰边框与被装饰物的一致性)
        - [.NET中装饰者模式的实现](#net中装饰者模式的实现)
        - [接口API的透明性](#接口api的透明性)
        - [总结](#总结)
    - [Proxy代理模式-只在必要时生成实例](#proxy代理模式-只在必要时生成实例)
        - [静态代理](#静态代理)
        - [总结](#总结-1)

<!-- /TOC -->
# 结构型设计模式

## Decorator装饰模式-装饰边框与被装饰物的一致性
装饰器模式(Decorator Pattern)允许向一个现有的对象添加新的功能，同时又不改变其结构。这种类型的设计模式属于结构型模式，它是作为现有的类的一个包装。

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
    /// 这一步重写很重要！！！
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

### .NET中装饰者模式的实现
在.NET 类库中也有装饰者模式的实现，该类就是System.IO.Stream,下面看看Stream类结构：

![](..\assets\Design\DecoratorStream.png)

```cs
// MemoryStream是内存流,为系统内存提供读写操作。被装饰
MemoryStream memoryStream = new MemoryStream(new byte[] { 95, 96, 97, 98, 99 });

// 添加扩展缓冲的功能
BufferedStream buffStream = new BufferedStream(memoryStream);

// 添加加密的功能
CryptoStream cryptoStream = new CryptoStream(buffStream, new AesManaged().CreateEncryptor(), CryptoStreamMode.Write);

// 添加压缩功能
GZipStream gzipStream = new GZipStream(cryptoStream, CompressionMode.Compress, true);
```


### 接口API的透明性
在Decorator模式中，装饰器和被装饰物具有一致性，也就是装饰类和被装饰类具有相同的接口。如此形成一个对象链，类似于递归结构，就像是剥洋葱一样，以为洋葱心要出来了，结果发现还是一层皮。

### 总结
**优点：**

1. 装饰这模式和继承的目的都是扩展对象的功能，但装饰者模式比继承更灵活
2. 通过使用不同的具体装饰类以及这些类的排列组合，设计师可以创造出很多不同行为的组合
3. 装饰者模式有很好地可扩展性

**缺点：**

装饰者模式会导致设计中出现许多小对象，如果过度使用，会让程序变的更复杂。并且更多的对象会是的差错变得困难，特别是这些对象看上去都很像。

装饰者模式采用对象组合而非继承的方式实现了再运行时动态地扩展对象功能的能力，而且可以根据需要扩展多个功能，避免了单独使用继承带来的 ”灵活性差“和”多子类衍生问题“。同时它很好地符合面向对象设计原则中 ”优先使用对象组合而非继承“和”开放-封闭“原则。

## Proxy代理模式-只在必要时生成实例
代理模式：为其他对象提供一种代理以便控制对这个对象的访问。

可以详细控制访问某个类(对象)的方法，在调用这个方法前作的前置处理(统一的流程代码放到代理中处理)。调用这个方法后做后置处理。

例如：明星的经纪人，租房的中介等等都是代理。

代理模式分类：

1. 静态代理(静态定义代理类，我们自己静态定义的代理类。比如我们自己定义一个明星的经纪人类)

2. 动态代理(通过程序动态生成代理类，该代理类不是我们自己定义的。而是由程序自动生成)

### 静态代理
Proxy有很多种变化形式：

- Virtual Proxy(虚拟代理)

虚拟代理，是根据需要创建开销很大的对象，通过它来存放实例化需要很长时间的真实对象。

例如《图解设计模式》中打印的示例，就是一个虚拟代理的应用。打印机类的实例化需要花费很多时间，所以使用代理进行初始化，需要打印时再进行实例化打印机。

- Remote Proxy(远程代理)

远程代理，也就是为了一个对象在不同的地址空间提供局部代表。这样可以隐藏一个对象存在于不同地址空间的事实。后面说道的SOA框架中WebService就是使用的这种代理方式。

- Access Proxy(安全代理)

安全代理，用来控制真实对象的访问时的权限。一般用于对象应该有不同的访问权限时。

**静态代理模式一般会有三个角色：**

Subject抽象角色：指代理角色(经纪人)和真实角色(明星)对外提供的公共方法，一般为一个接口

RealSubject真实角色：需要实现抽象角色接口，定义了真实角色所要实现的业务逻辑，以便供代理角色调用。也就是真正的业务逻辑在此。

Proxy代理角色：需要实现抽象角色接口，是真实角色的代理，通过真实角色的业务逻辑方法来实现抽象方法，并可以附加自己的操作。
* 
对应类图如下图所示：

![](..\assets\Design\Proxy.png)

《图解设计模式》中的打印示例属于VirtualProxy(虚拟代理),实现代码如下：
```cs
class Program
{
    static void Main(string[] args)
    {
        //如果是直接实例化Printer类，构造函数需要耗时等待，而通过代理方式，构造时无需等待，只需调用时等待
        IPrintable p = new PrinterProxy("jack");
        Console.WriteLine(p.Name);
        p.Name = "lucy";
        Console.WriteLine(p.Name);
        //调用代理方法时才会实例化主体类
        p.Print("hello world");
    }
}

/// <summary>
/// 打印接口-Subject主体接口或抽象类
/// </summary>
public interface IPrintable
{
    string Name { get; set; }
    void Print(string str);
}

/// <summary>
/// 打印机类-主体
/// </summary>
public class Printer : IPrintable
{
    public string Name { get; set; }

    public Printer(string name)
    {
        Name = name;
        HeavyJob("Printer实例(" + Name + ")生成中");
    }

    /// <summary>
    /// 打印带本机名称的消息
    /// </summary>
    /// <param name="str"></param>
    public void Print(string str)
    {
        Console.WriteLine("=== " + Name + " ===");
        Console.WriteLine(str);
    }

    /// <summary>
    /// 模拟耗时操作
    /// </summary>
    /// <param name="msg"></param>
    private void HeavyJob(string msg)
    {
        Console.Write(msg);
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine("完成");
    }
}

/// <summary>
/// 代理类Proxy
/// </summary>
public class PrinterProxy : IPrintable
{
    Printer real;

    string name;
    /// <summary>
    /// 设置代理属性的同时也设置实体的属性
    /// </summary>
    public string Name
    {
        get { return name; }
        set
        {
            if (real != null)
            {
                real.Name = value;
            }
            name = value;
        }
    }

    public PrinterProxy(string name)
    {
        Name = name;
    }

    public void Print(string str)
    {
        if (null == real)
        {
            real = new Printer(Name);
        }
        real.Print(str);
    }

}
```

以明星与经纪人的代理关系作为示例，静态实现如下：
```cs
class Program
{
    static void Main(string[] args)
    {
        Star baoqiang = new RealStar("王宝强");
        Star songzhe = new StarProxy("宋喆", baoqiang);

        songzhe.BookTicket();//可以由宋喆代理
        songzhe.MakeMovie();//宋喆不可以代理，只能宝强拍电影
    }
}

/// <summary>
/// Subject抽象角色 明星和经纪人
/// </summary>
public abstract class Star
{
    public Star(string name) { Name = name; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 订票，可以由经纪人代理
    /// </summary>
    public abstract void BookTicket();
    /// <summary>
    /// 拍电影，不能由经纪人代理
    /// </summary>
    public abstract void MakeMovie();
}

/// <summary>
/// RealSubject真实角色 明星
/// </summary>
public class RealStar : Star
{
    public RealStar(string name) : base(name) { }

    public override void BookTicket()
    {
        Console.WriteLine(Name + "买票");
    }

    public override void MakeMovie()
    {
        Console.WriteLine(Name + "拍电影");
    }
}

/// <summary>
/// Proxy代理角色 经纪人
/// </summary>
public class StarProxy : Star
{
    private Star real;

    /// <summary>
    /// 自定义构造函数，给真实角色real进行赋值
    /// </summary>
    /// <param name="name"></param>
    /// <param name="star"></param>
    public StarProxy(string name, Star star) : base(name)
    {
        Name = name;
        real = star;
    }

    public override void BookTicket()
    {
        Console.WriteLine(Name + "买票");
    }

    /// <summary>
    /// 不能代理，需要调用真实角色方法
    /// </summary>
    public override void MakeMovie()
    {
        real.MakeMovie();
    }
}
```

### 总结
优点：

1. 代理模式能够将调用用于真正被调用的对象隔离，在一定程度上降低了系统的耦合度；
2. 代理对象在客户端和目标对象之间起到一个中介的作用，这样可以起到对目标对象的保护。代理对象可以在对目标对象发出请求之前进行一个额外的操作，例如权限检查等。

缺点：

1. 由于在客户端和真实主题之间增加了一个代理对象，所以会造成请求的处理速度变慢。
2. 实现代理类也需要额外的工作，从而增加了系统的实现复杂度。

参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)


