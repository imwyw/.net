<!-- TOC -->

- [行为型设计模式](#行为型设计模式)
    - [Iterator 迭代器模式-一个一个的遍历](#iterator-迭代器模式-一个一个的遍历)
        - [实现](#实现)
        - [.NET中迭代器模式的应用](#net中迭代器模式的应用)
        - [IEnumerator接口](#ienumerator接口)
    - [Observer 观察者模式-发送状态变化的通知](#observer-观察者模式-发送状态变化的通知)
        - [适用场景](#适用场景)
        - [优缺点](#优缺点)
    - [Visitor 访问者模式-访问数据结果并处理数据](#visitor-访问者模式-访问数据结果并处理数据)

<!-- /TOC -->
<a id="markdown-行为型设计模式" name="行为型设计模式"></a>
# 行为型设计模式
<a id="markdown-iterator-迭代器模式-一个一个的遍历" name="iterator-迭代器模式-一个一个的遍历"></a>
## Iterator 迭代器模式-一个一个的遍历
迭代器模式提供了一种方法顺序访问一个聚合对象（理解为集合对象）中各个元素，而又无需暴露该对象的内部表示，这样既可以做到不暴露集合的内部结构，又可让外部代码透明地访问集合内部的数据。

<a id="markdown-实现" name="实现"></a>
### 实现
既然，迭代器模式承担了遍历集合对象的职责，则该模式自然存在2个类，一个是聚合类，一个是迭代器类。在面向对象涉及原则中还有一条是针对接口编程，所以，在迭代器模式中，抽象了2个接口，一个是聚合接口，另一个是迭代器接口，这样迭代器模式中就四个角色了，具体的类图如下所示：

![](..\assets\Design\Iterator.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        IAggregate c1 = new ConcreteAggregate();
        IIterator ite = c1.CreateIterator();

        while (ite.HasNext())
        {
            Console.Write(ite.Next());
        }
    }
}

/// <summary>
/// 迭代器接口
/// </summary>
public interface IIterator
{
    /// <summary>
    /// 是否有下一个元素
    /// </summary>
    /// <returns></returns>
    bool HasNext();

    /// <summary>
    /// 返回下一个元素
    /// </summary>
    /// <returns></returns>
    object Next();
}

/// <summary>
/// 集合类接口
/// </summary>
public interface IAggregate
{
    /// <summary>
    /// 构造迭代器返回
    /// </summary>
    /// <returns></returns>
    IIterator CreateIterator();
}

/// <summary>
/// 具体集合类
/// </summary>
public class ConcreteAggregate : IAggregate
{
    string[] collections;

    public ConcreteAggregate()
    {
        collections = new string[] { "h", "e", "l", "l", "o" };
    }

    /// <summary>
    /// 构造一个迭代器
    /// </summary>
    /// <returns></returns>
    public IIterator CreateIterator()
    {
        return new ConcreteIterator(this);
    }

    /// <summary>
    /// 返回集合的长度
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        return collections.Length;
    }

    /// <summary>
    /// 返回指定索引位置的元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string ElementAt(int index)
    {
        return collections[index];
    }

}

/// <summary>
/// 具体迭代器
/// </summary>
public class ConcreteIterator : IIterator
{
    int index;
    ConcreteAggregate aggregate;

    public ConcreteIterator(ConcreteAggregate agg)
    {
        aggregate = agg;
    }

    public bool HasNext()
    {
        return index < aggregate.GetLength();
    }

    public object Next()
    {
        string element = aggregate.ElementAt(index++);
        return element;
    }
}
```

<a id="markdown-net中迭代器模式的应用" name="net中迭代器模式的应用"></a>
### .NET中迭代器模式的应用
在.NET下，迭代器模式中的聚集接口和迭代器接口都已经存在了，其中IEnumerator接口扮演的就是迭代器角色，IEnumberable接口则扮演的就是抽象聚集的角色，只有一个GetEnumerator()方法，关于这两个接口的定义可以自行参考MSDN。在.NET 1.0中，.NET 类库中很多集合都已经实现了迭代器模式，大家可以用反编译工具Reflector来查看下mscorlib程序集下的System.Collections命名空间下的类，这里给出ArrayList的定义代码，具体实现代码可以自行用反编译工具查看，具体代码如下所示：

```cs
public class ArrayList : IList, ICollection, IEnumerable, ICloneable
{
    // Fields
    private const int _defaultCapacity = 4;
    private object[] _items;
    private int _size;
    [NonSerialized]
    private object _syncRoot;
    private int _version;
    private static readonly object[] emptyArray;

    public virtual IEnumerator GetEnumerator();
    public virtual IEnumerator GetEnumerator(int index, int count);

    // Properties
    public virtual int Capacity { get; set; }
    public virtual int Count { get; }
    //.............. 更多代码请自行用反编译工具Reflector查看
}
```

<a id="markdown-ienumerator接口" name="ienumerator接口"></a>
### IEnumerator接口
使用IEnumerator接口方式实现一个迭代器：

```cs
class Program
{
    static void Main(string[] args)
    {
        ConcreteAggregate agg = new ConcreteAggregate();
        StrIterator ite = new StrIterator(agg);
        while (ite.MoveNext())
        {
            Console.Write(ite.Current);
        }
        Console.WriteLine();
        
        //foreach迭代
        foreach (var item in agg)
        {
            Console.Write(item);
        }
    }
}

/// <summary>
/// 具体集合类
/// 实现IEnumerable接口可以用foreach进行遍历
/// 依靠MoveNext和Current来达到Foreach的遍历，返回自定义的一个迭代器
/// </summary>
public class ConcreteAggregate : IEnumerable
{
    string[] collections;

    public ConcreteAggregate()
    {
        collections = new string[] { "h", "e", "l", "l", "o" };
    }

    /// <summary>
    /// 返回集合的长度
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        return collections.Length;
    }

    /// <summary>
    /// 返回指定索引位置的元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string ElementAt(int index)
    {
        return collections[index];
    }

    /// <summary>
    /// 构造一个自定义迭代器
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
        return new StrIterator(this);
    }
}

/// <summary>
/// 自定义一个迭代器，必须实现IEnumerator接口
/// 实现IEnumerator就是为了集合类的调用
/// </summary>
public class StrIterator : IEnumerator
{
    ConcreteAggregate aggregate;
    int index;
    string current;

    public StrIterator(ConcreteAggregate agg)
    {
        aggregate = agg;
    }

    public object Current
    {
        get
        {
            return current;
        }
    }

    public bool MoveNext()
    {
        if (index + 1 > aggregate.GetLength())
        {
            return false;
        }
        else
        {
            current = aggregate.ElementAt(index);
            index++;
            return true;
        }
    }

    public void Reset()
    {
        index = 0;
    }
}
```

https://www.cnblogs.com/fangyz/p/5721269.html

<a id="markdown-observer-观察者模式-发送状态变化的通知" name="observer-观察者模式-发送状态变化的通知"></a>
## Observer 观察者模式-发送状态变化的通知

观察者模式定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象，这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己的行为。

对应类图如下：

![](..\assets\Design\Observer.png)

**抽象主题角色(Subject)**：抽象主题把所有观察者对象的引用保存在一个列表中，并提供增加和删除观察者对象的操作，抽象主题角色又叫做抽象被观察者角色，一般由抽象类或接口实现。
**抽象观察者角色(Observer)**：为所有具体观察者定义一个接口，在得到主题通知时更新自己，一般由抽象类或接口实现。
**具体主题角色(ConcreteSubject)**：实现抽象主题接口，具体主题角色又叫做具体被观察者角色。
**具体观察者角色(ConcreteObserver)**：实现抽象观察者角色所要求的接口，以便使自身状态与主题的状态相协调。

引用《图解设计模式》示例，观察数值生成，通过观察者对象作出不同的响应，该示例类图与代码如下所示：

![](..\assets\Design\ObserverNumberDemo.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        NumberGenerator rdGene = new RandomNumberGenerator();

        IObserver dgObs = new DigitObserver();
        rdGene.AddObserver(dgObs);

        IObserver ghObs = new GraphObserver();
        rdGene.AddObserver(ghObs);

        rdGene.Excute();
    }
}

/// <summary>
/// 观察者接口
/// </summary>
public interface IObserver
{
    void Update(NumberGenerator generator);
}

/// <summary>
/// 抽象观察主体
/// </summary>
public abstract class NumberGenerator
{
    protected List<IObserver> lstObservers = new List<IObserver>();

    /// <summary>
    /// 添加观察对象
    /// </summary>
    /// <param name="obs"></param>
    public void AddObserver(IObserver obs)
    {
        lstObservers.Add(obs);
    }

    /// <summary>
    /// 移除观察对象
    /// </summary>
    /// <param name="obs"></param>
    public void RemoveObserver(IObserver obs)
    {
        lstObservers.Remove(obs);
    }

    /// <summary>
    /// 通知更新
    /// </summary>
    public void NotifyObserver()
    {
        foreach (IObserver item in lstObservers)
        {
            item.Update(this);
        }
    }

    /// <summary>
    /// 获取数值
    /// </summary>
    /// <returns></returns>
    public abstract int GetNumber();

    /// <summary>
    /// 生成数值
    /// </summary>
    public abstract void Excute();
}

/// <summary>
/// 具体观察对象，被观察
/// </summary>
public class RandomNumberGenerator : NumberGenerator
{
    Random rd = new Random();

    //当前数值
    int number;

    /// <summary>
    /// 生成20个随机数
    /// </summary>
    public override void Excute()
    {
        for (int i = 0; i < 20; i++)
        {
            number = rd.Next(0, 50);
            NotifyObserver();
        }
    }

    /// <summary>
    /// 获取当前数
    /// </summary>
    /// <returns></returns>
    public override int GetNumber()
    {
        return number;
    }
}

/// <summary>
/// 具体观察类，数字响应
/// </summary>
public class DigitObserver : IObserver
{
    public void Update(NumberGenerator generator)
    {
        Console.WriteLine("DigitObserver out:" + generator.GetNumber());
        Thread.Sleep(100);
    }
}

/// <summary>
/// 具体观察类，模拟图像响应
/// </summary>
public class GraphObserver : IObserver
{
    public void Update(NumberGenerator generator)
    {
        Console.WriteLine("GraphObserver out:");
        for (int i = 0; i < generator.GetNumber(); i++)
        {
            Console.Write("*");
        }
        Thread.Sleep(100);
    }
}
```

下面以微信订阅号的例子来说明观察者模式的实现。现在要实现监控腾讯游戏订阅号的状态的变化。
类图及实现如下：

![](..\assets\Design\ObserverWechat.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        Tencent game = new TencentGame();
        IObserverSubscriber jack = new ConcreteSubscriber("jack");
        game.AddObserver(jack);

        //模拟发布消息
        game.Publish("王者荣耀新皮肤！双11特价");
    }
}

/// <summary>
/// 订阅者接口
/// </summary>
public interface IObserverSubscriber
{
    void Update(Tencent tencent);
}

/// <summary>
/// 腾讯订阅号抽象类
/// </summary>
public abstract class Tencent
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 保存订阅者列表
    /// </summary>
    List<IObserverSubscriber> lstObservers = new List<IObserverSubscriber>();

    /// <summary>
    /// 添加订阅者
    /// </summary>
    /// <param name="obs"></param>
    public void AddObserver(IObserverSubscriber obs)
    {
        lstObservers.Add(obs);
    }

    /// <summary>
    /// 删除订阅者
    /// </summary>
    /// <param name="obs"></param>
    public void RemoveObserver(IObserverSubscriber obs)
    {
        lstObservers.Remove(obs);
    }

    /// <summary>
    /// 通知所有订阅者
    /// </summary>
    public void NotifyObservers()
    {
        foreach (IObserverSubscriber item in lstObservers)
        {
            item.Update(this);
        }
    }

    /// <summary>
    /// 模拟订阅号发布消息
    /// </summary>
    /// <param name="msg"></param>
    public abstract void Publish(string msg);
}

public class TencentGame : Tencent
{
    public override void Publish(string msg)
    {
        Message = msg;
        Console.WriteLine(Message);
        NotifyObservers();
    }
}

/// <summary>
/// 具体订阅者类
/// </summary>
public class ConcreteSubscriber : IObserverSubscriber
{
    /// <summary>
    /// 订阅者名称
    /// </summary>
    public string Name { get; set; }

    public ConcreteSubscriber(string name)
    {
        Name = name;
    }

    public void Update(Tencent tencent)
    {
        Console.Write(Name + "\t收到新的推送消息\t");
        Console.WriteLine("订阅消息发送至邮箱，内容：" + tencent.Message);
    }
}
```

在.NET中我们还可以使用委托和事件来简化Observer模式的实现，上面腾讯订阅号使用委托和事件的实现如下：
```cs
class Program
{
    static void Main(string[] args)
    {
        Tencent game = new TencentGame();
        ConcreteSubscriber jack = new ConcreteSubscriber("jack");
        game.AddObserver(jack.Update);

        ConcreteSubscriber lucy = new ConcreteSubscriber("lucy");
        game.AddObserver(lucy.Update);

        game.Publish("快和好友一起来农药吧");
    }
}

/// <summary>
/// 委托 充当观察者接口
/// </summary>
/// <param name="sender"></param>
public delegate void NotifyEventHandler(Tencent sender);

/// <summary>
/// 腾讯订阅号抽象类
/// </summary>
public abstract class Tencent
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 充当 观察者列表
    /// </summary>
    NotifyEventHandler notifyEvents;

    /// <summary>
    /// 添加订阅者
    /// </summary>
    /// <param name="obs"></param>
    public void AddObserver(NotifyEventHandler ob)
    {
        notifyEvents += ob;
    }

    /// <summary>
    /// 删除订阅者
    /// </summary>
    /// <param name="obs"></param>
    public void RemoveObserver(NotifyEventHandler ob)
    {
        notifyEvents -= ob;
    }

    /// <summary>
    /// 通知所有订阅者
    /// </summary>
    public void NotifyObservers()
    {
        notifyEvents(this);
    }

    /// <summary>
    /// 模拟订阅号发布消息
    /// </summary>
    /// <param name="msg"></param>
    public abstract void Publish(string msg);
}

public class TencentGame : Tencent
{
    public override void Publish(string msg)
    {
        Message = msg;
        Console.WriteLine(Message);
        NotifyObservers();
    }
}

/// <summary>
/// 具体订阅者类
/// </summary>
public class ConcreteSubscriber
{
    /// <summary>
    /// 订阅者名称
    /// </summary>
    public string Name { get; set; }

    public ConcreteSubscriber(string name)
    {
        Name = name;
    }

    public void Update(Tencent tencent)
    {
        Console.Write(Name + "\t收到新的推送消息\t");
        Console.WriteLine("订阅消息发送至邮箱，内容：" + tencent.Message);
    }
}
```

<a id="markdown-适用场景" name="适用场景"></a>
### 适用场景
* 当一个抽象模型有两个方面，其中一个方面依赖于另一个方面，将这两者封装在独立的对象中以使它们可以各自独立地改变和复用的情况下。从方面的这个词中可以想到，观察者模式肯定在AOP（面向方面编程）中有所体现，更多内容参考：[Observern Pattern in AOP](http://www.cnblogs.com/idior/articles/229590.html)
* 当对一个对象的改变需要同时改变其他对象，而又不知道具体有多少对象有待改变的情况下。
* 当一个对象必须通知其他对象，而又不能假定其他对象是谁的情况下。

<a id="markdown-优缺点" name="优缺点"></a>
### 优缺点
优点：
1. 观察者模式实现了表示层和数据逻辑层的分离，并定义了稳定的更新消息传递机制，并抽象了更新接口，使得可以有各种各样不同的表示层，即观察者。
2. 观察者模式在被观察者和观察者之间建立了一个抽象的耦合，被观察者并不知道任何一个具体的观察者，只是保存着抽象观察者的列表，每个具体观察者都符合一个抽象观察者的接口。
3. 观察者模式支持广播通信。被观察者会向所有的注册过的观察者发出通知。

缺点：
1. 如果一个被观察者有很多直接和间接的观察者时，将所有的观察者都通知到会花费很多时间。
2. 虽然观察者模式可以随时使观察者知道所观察的对象发送了变化，但是观察者模式没有相应的机制使观察者知道所观察的对象是怎样发生变化的。
3. 如果在被观察者之间有循环依赖的话，被观察者会触发它们之间进行循环调用，导致系统崩溃，在使用观察者模式应特别注意这点。

<a id="markdown-visitor-访问者模式-访问数据结果并处理数据" name="visitor-访问者模式-访问数据结果并处理数据"></a>
## Visitor 访问者模式-访问数据结果并处理数据
访问者模式是封装一些施加于某种数据结构之上的操作。一旦这些操作需要修改的话，接受这个操作的数据结构则可以保存不变。访问者模式适用于数据结构相对稳定的系统， 它把数据结构和作用于数据结构之上的操作之间的耦合度降低，使得操作集合可以相对自由地改变。

从上面描述可知，访问者模式是用来封装某种数据结构中的方法。具体封装过程是：每个元素接受一个访问者的调用，每个元素的Accept方法接受访问者对象作为参数传入，访问者对象则反过来调用元素对象的操作。具体的访问者模式结构图如下所示。

![](..\assets\Design\Visitor.png)

下面我们以一个财务相关账单示例，在一个账单中会有多个交易流水，交易类型可能是工资、饮食、购物、股票、开房等等各种，但可以划分两个固定的类型，收入和支出。
那么在这样的账单示例中，交流流水的收入和支出类型属于不会变动的元素，属于一种数据结构。
现要求针对账单里的所有交易流水进行操作，操作的种类并不固定，so 我们并不能将对数据的操作放在数据结构中进行封装，而是应该将对数据的操作和数据结构本身分离开来，这就是Vistor模式。

类图和代码实现如下所示：

![](..\assets\Design\VisitorBillDemo.png)

```cs
class Program
{
    static void Main(string[] args)
    {
        //实例化一个账单
        BillStructure bills = new BillStructure();

        //访问者-老板
        Visitor boss = new Boss();

        //访问者-财务
        Visitor cpa = new CPA();

        bills.CheckAllBills(boss);
        bills.CheckAllBills(cpa);

        (boss as Boss).ShowTotal();
    }
}

/// <summary>
/// 访问者抽象类
/// 依赖于所访问的数据结构，固定的支出OutBill和收入InBill流水
/// </summary>
public abstract class Visitor
{
    /// <summary>
    /// 收入交易
    /// </summary>
    /// <param name="bill"></param>
    public abstract void Visit(OutBillDetail bill);
    /// <summary>
    /// 支出交易
    /// </summary>
    /// <param name="bill"></param>
    public abstract void Visit(InBillDetail bill);
}

/// <summary>
/// 账单详情抽象类
/// </summary>
public abstract class BillDetail
{
    /// <summary>
    /// 交易名称 如采购、工资、股票等
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// 交易发生金额
    /// </summary>
    public int Amount { get; set; }
    public BillDetail(string content, int amount)
    {
        Content = content;
        Amount = amount;
    }

    /// <summary>
    /// 接收访问者访问
    /// </summary>
    /// <param name="v"></param>
    public abstract void Accept(Visitor v);
}

/// <summary>
/// 接收访问者访问的接口
/// 该接口并入抽象类BillDetail
/// </summary>
//public interface IBill
//{
//    void Accept(Visitor v);
//}

/// <summary>
/// 支出流水
/// </summary>
public class OutBillDetail : BillDetail
{
    public OutBillDetail(string content, int amount) : base(content, amount) { }

    public override void Accept(Visitor v)
    {
        v.Visit(this);
    }
}

/// <summary>
/// 收入流水
/// </summary>
public class InBillDetail : BillDetail
{
    public InBillDetail(string content, int amount) : base(content, amount) { }

    public override void Accept(Visitor v)
    {
        v.Visit(this);
    }
}

/// <summary>
/// 老板，属于一种访问者
/// 老板只关心 支出总计和收入总计，是否盈利
/// </summary>
public class Boss : Visitor
{
    /// <summary>
    /// 总支出
    /// </summary>
    public double TotalOut { get; private set; }
    /// <summary>
    /// 总收入
    /// </summary>
    public double TotalIn { get; private set; }

    public override void Visit(InBillDetail bill)
    {
        TotalIn += bill.Amount;
    }

    public override void Visit(OutBillDetail bill)
    {
        TotalOut += bill.Amount;
    }

    /// <summary>
    /// 老板特有方法
    /// </summary>
    public void ShowTotal()
    {
        Console.WriteLine("共计收入{0}元，支出{1}元，盈亏{2}元", TotalIn, TotalOut, TotalIn - TotalOut);
    }
}

/// <summary>
/// 会计，属于一种访问者
/// 关心每一笔记录，是否有错记漏记
/// </summary>
public class CPA : Visitor
{
    int cntIn;
    int cntOut;
    public override void Visit(InBillDetail bill)
    {
        cntIn++;
        Console.WriteLine("第{0}笔收入，交易详情：{1}，交易额：￥{2}元", cntIn, bill.Content, bill.Amount);
    }

    public override void Visit(OutBillDetail bill)
    {
        cntOut++;
        Console.WriteLine("第{0}笔支出，交易详情：{1}，交易额：￥{2}元", cntOut, bill.Content, bill.Amount);
    }
}

/// <summary>
/// 模拟构造一个账单模板
/// </summary>
public class BillStructure
{
    List<BillDetail> lstBillDetails = new List<BillDetail>();

    /// <summary>
    /// 自定义构造，随机生成交易流水
    /// </summary>
    public BillStructure()
    {
        Random rd = new Random();
        //模拟构造一个账单，随机生成多个交易
        for (int i = 0; i < 10; i++)
        {
            if (rd.Next(0, 2) % 2 == 0)
            {
                lstBillDetails.Add(new InBillDetail("收入", rd.Next(0, 101)));
            }
            else
            {
                lstBillDetails.Add(new OutBillDetail("支出", rd.Next(0, 101)));
            }
        }
    }

    /// <summary>
    /// 核查所有交易流水
    /// </summary>
    /// <param name="v"></param>
    public void CheckAllBills(Visitor v)
    {
        foreach (BillDetail item in lstBillDetails)
        {
            item.Accept(v);
        }
    }
}
```

参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)


