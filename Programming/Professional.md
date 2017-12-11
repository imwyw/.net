<!-- TOC -->

- [高级编程](#高级编程)
    - [委托、Lambda表达式和事件](#委托lambda表达式和事件)
        - [委托](#委托)
            - [泛型委托-Predicate](#泛型委托-predicate)
            - [泛型委托-Action](#泛型委托-action)
            - [泛型委托-Func](#泛型委托-func)
        - [Lambda表达式](#lambda表达式)
        - [事件](#事件)
    - [扩展方法](#扩展方法)
    - [LINQ](#linq)
        - [查询操作符](#查询操作符)
        - [查询表达式](#查询表达式)
    - [反射](#反射)
        - [反射(Reflection)](#反射reflection)
        - [自定义特性(Attribute)](#自定义特性attribute)
            - [特性是什么](#特性是什么)
            - [作用](#作用)
            - [Attribute 与注释的区别](#attribute-与注释的区别)
            - [使用](#使用)
    - [多线程](#多线程)
        - [什么是进程？](#什么是进程)
        - [什么是线程？](#什么是线程)
        - [线程创建](#线程创建)
        - [线程阻塞](#线程阻塞)
        - [lock](#lock)

<!-- /TOC -->
<a id="markdown-高级编程" name="高级编程"></a>
# 高级编程

<a id="markdown-委托lambda表达式和事件" name="委托lambda表达式和事件"></a>
## 委托、Lambda表达式和事件

<a id="markdown-委托" name="委托"></a>
### 委托
委托是安全封装方法的类型，类似于 C 和 C++ 中的函数指针。 与 C 函数指针不同的是，委托是面向对象的、类型安全的和可靠的。

说白了，委托是一个类，字面理解就是托人干事。将方法作为实参传递，实际传递的是方法地址/引用。

```cs
//委托调用的方法和委托的定义必须保持一致，如下面的几个示例
void Say(){}
delegate void DelegateTalk();

string Say(){}
delegate string DelegateTalk();

bool Say(int value){}
delegate bool DelegateTalk(int vlaue);
```

方法作为参数进行传递
```cs
static void Main(string[] args)
{
    BackHome(BuyTicket);
    BackHome(Subway);

    Console.ReadKey();
}

/// <summary>
/// 无参，返回值为void的委托 和下面的Action等同
/// </summary>
//public delegate void DelegateBack();

static void BackHome(Action action)
{
    action();
}

static void BuyTicket()
{
    Console.WriteLine("买火车票");
}

static void Subway()
{
    Console.WriteLine("再换乘地铁");
}
```

针对上例中的Main方法也可以修改为以下方式，以合并委托(多路广播委托)的方式实现
```cs
static void Main(string[] args)
{
    Action action = new Action(BuyTicket);
    action += Subway;

    BackHome(action);

    Console.ReadKey();
}
```

<a id="markdown-泛型委托-predicate" name="泛型委托-predicate"></a>
#### 泛型委托-Predicate

表示定义一组条件并确定指定对象是否符合这些条件的方法。此委托由 Array 和 List 类的几种方法使用，用于在集合中搜索元素。

`public delegate bool Predicate<T>(T obj);`

类型参数介绍：
   T： 要比较的对象的类型。
   obj： 要按照由此委托表示的方法中定义的条件进行比较的对象。
   返回值：bool，如果 obj 符合由此委托表示的方法中定义的条件，则为 true；否则为 false。

```cs
List<string> listStr = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
string[] arrStr = listStr.ToArray();

/*
筛选 长度小于3的元素
*/

//1、原始方法
foreach (var item in listStr)
{
    if (item.Length <= 3)
    {
        Console.WriteLine(item);
    }
}
foreach (var item in arrStr)
{
    if (item.Length <= 3)
    {
        Console.WriteLine(item);
    }
}

//2、使用Predicate
List<string> list1 = listStr.FindAll(t => t.Length <= 3);
Console.WriteLine(string.Join(",", list1));

string[] arr1 = Array.FindAll(arrStr, t => t.Length <= 3);
Console.WriteLine(string.Join(",", arr1));
```

<a id="markdown-泛型委托-action" name="泛型委托-action"></a>
#### 泛型委托-Action
Action泛型委托代表了一类方法：可以有0个到16个输入参数，输入参数的类型是不确定的，但不能有返回值。
```cs
//Action 等效
delegate void Delegate1();

//Action<int, string, bool, object> 等效
delegate void Delegate2(int num, string str, bool isa, object obj);
```

<a id="markdown-泛型委托-func" name="泛型委托-func"></a>
#### 泛型委托-Func
为了弥补Action泛型委托，不能返回值的不足，.net提供了Func泛型委托。

相同的是它也是最多0到16个输入参数，参数类型由使用者确定，不同的是它规定要有一个返回值，返回值的类型也由使用者确定。

```cs
//Func<string> 等效
delegate string Delegate1();

//Func<string, bool> 等效
delegate bool Delegate2(string name);

//Func<string, bool, object, int> 等效
delegate int Delegate3(string str, bool isa, object obj);
```

<a id="markdown-lambda表达式" name="lambda表达式"></a>
### Lambda表达式
Lambda 表达式是一种可用于创建 委托 或 表达式目录树 类型的 匿名函数 。 通过使用 lambda 表达式，可以写入可作为参数传递或作为函数调用值返回的本地函数。 Lambda 表达式对于编写 LINQ 查询表达式特别有用。

[Lambda 表达式(C# 编程指南)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions)

在 2.0 之前的 C# 版本中，声明委托的唯一方法是使用命名方法。  C# 2.0 引入了匿名方法，而在 C# 3.0 及更高版本中，Lambda 表达式取代了匿名方法，作为编写内联代码的首选方式。在C#2.0之前就有委托了，在2.0之后又引入了匿名方法，C#3.0之后，又引入了Lambda表达式，他们三者之间的顺序是：委托->匿名表达式->Lambda表达式。

以下分别是三种对应不同的实现：
```cs
static void Main(string[] args)
{
    /*
    委托
    加法运算，最简单的委托方式，先定义方法Add，再进行传入
    */
    DoCalc(Add, 1, 2);

    /*
    匿名方法
    减法运算，使用匿名方法形式传入
    */
    DoCalc(delegate (int x, int y)
    {
        return x - y;
    }, 1, 2);

    /*
    Lambda表达式
    乘法运算，【=>】左侧(x,y)为参数，【=>】右侧为代码块
    若要创建 Lambda 表达式，需要在 Lambda 运算符 => 左侧指定输入参数(如果有)，然后在另一侧输入表达式或语句块。
    */
    DoCalc((x, y) =>
    {
        return x * y;
    }, 2, 3);

    //上述乘法运算也可以简写为：
    DoCalc((x, y) => x * y, 2, 3);
}

/// <summary>
/// 计算委托的类型
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <returns></returns>
delegate int Calculate(int x, int y);

/// <summary>
/// 执行计算方法
/// </summary>
/// <param name="fun">委托传入的方法</param>
/// <param name="x">操作数1</param>
/// <param name="y">操作数2</param>
static void DoCalc(Calculate fun, int x, int y)
{
    fun.Invoke(x, y);
}

/// <summary>
/// 加法
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <returns></returns>
static int Add(int x, int y)
{
    return x + y;
}
```

"Lambda表达式"是一个特殊的匿名函数，是一种高效的类似于函数式编程的表达式，Lambda简化了开发中需要编写的代码量。它可以包含表达式和语句，并且可用于创建委托或表达式目录树类型，支持带有可绑定到委托或表达式树的输入参数的内联表达式。所有Lambda表达式都使用Lambda运算符=>，该运算符读作"goes to"。Lambda运算符的左边是输入参数(如果有)，右边是表达式或语句块。Lambda表达式x => x * x读作"x goes to x times x"。

上述示例也可以使用.NET预定义的委托Func<>进行替代，不需要新定义Calculate委托，如下：
```cs
/// <summary>
/// 计算委托的类型
/// 注释该委托，使用Func<>委托代替
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <returns></returns>
//delegate int Calculate(int x, int y);

/// <summary>
/// 执行计算方法
/// Func<int,int,int>前两个int为参数，最后一个int为返回值类型
/// </summary>
/// <param name="fun">委托传入的方法 使用.NET预定义的委托</param>
/// <param name="x">操作数1</param>
/// <param name="y">操作数2</param>
static void DoCalc(Func<int, int, int> fun, int x, int y)
{
    fun.Invoke(x, y);
}
```

仅当 lambda 只有一个输入参数时，括号才是可选的；否则括号是必需的。 括号内的两个或更多输入参数使用逗号加以分隔：
```cs
(x, y) => x == y
x=> x*x
```

使用空括号指定零个输入参数：
```cs
() => SomeMethod()
```

<a id="markdown-事件" name="事件"></a>
### 事件
事件(Event) 基本上说是一个用户操作，如按键、点击、鼠标移动等等，或者是一些出现，如系统生成的通知。应用程序需要在事件发生时响应事件。例如，中断。事件是用于进程间通信。

事件在类中声明且生成，且通过使用同一个类或其他类中的委托与事件处理程序关联。包含事件的类用于发布事件。这被称为 **发布器(publisher) 类**。其他接受该事件的类被称为 **订阅器(subscriber) 类**。事件使用 **发布-订阅(publisher-subscriber) 模型**。

**发布器(publisher)** 是一个包含事件和委托定义的对象。事件和委托之间的联系也定义在这个对象中。发布器(publisher)类的对象调用这个事件，并通知其他的对象。

**订阅器(subscriber)** 是一个接受事件并提供事件处理程序的对象。在发布器(publisher)类中的委托调用订阅器(subscriber)类中的方法(事件处理程序)。

<a id="markdown-扩展方法" name="扩展方法"></a>
## 扩展方法
如果想给一个类型增加行为，一定要通过继承的方式实现吗？不一定的！

比如我们想要给String类添加打印输出到控制台的方法，可以通过如下方式实现：
```cs
static class Program
{
    /// <summary>
    /// 扩展方法，针对string类型增加打印输出到控制台的方法
    /// 注意，扩展方法必须放在非泛型静态类中，方法也需要声明为静态方法
    /// </summary>
    /// <param name="source"></param>
    public static void PrintConsole(this string source)
    {
        Console.WriteLine(source);
    }

    static void Main(string[] args)
    {
        string name = "不识美妻刘强东";
        //扩展方法的调用
        name.PrintConsole();
    }
}
```

<a id="markdown-linq" name="linq"></a>
## LINQ
LINQ是.NET 3.5中新增的一种技术，这个技术扩展了.NET平台上的编程语言，使其可以更加方便的进行数据查询，单纯的LINQ技术主要完成对集合对象（如System.Collection下或System.Collection.Generic命名空间下的对象）的查询。结合LINQ Provider可以实现对XML文件（使用LINQ to XML – 位于System.Xml.Linq命名空间下的类），数据库（可以使用LINQ to SQL或下文要详细介绍的LINQ to Entity）等对象的操作。

LINQ是一种运行时无关的技术，其运行于CLR2.0之上，微软对C#3.0与VB9.0的编译器进性扩展，从而使其可以将LINQ编写的程序编译为可以被CLR2.0的JIT所理解的MSIL。

> https://msdn.microsoft.com/zh-cn/library/bb397906(v=vs.110).aspx

<a id="markdown-查询操作符" name="查询操作符"></a>
### 查询操作符
.net的设计者在类库中定义了一系列的扩展方法，方便操作集合对象，这些扩展方法构成了LINQ的查询操作符。

这一系列的扩展方法，比如：Where，Max，Select，Sum，Any，Average，All，Concat等。都是针对IEnumerable的对象进行扩展的，也就是说，只要实现了IEnumerable接口，就可以使用这些扩展方法。

```cs
List<int> arr = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

//Where、Sum就是针对IEnumerable接口的扩展方法
var result = arr.Where(a => { return a > 3; }).Sum();
Console.WriteLine(result);
```

<a id="markdown-查询表达式" name="查询表达式"></a>
### 查询表达式
上面我们已经提到，使用查询操作符表示的扩展方法来操作集合；虽然已经很方便了，但在可读性和代码的语义来考虑，仍有不足；于是就产生了查询表达式的写法。虽然这很像SQL语句，但他们却有着本质的不同。

针对上例中筛选后进行求和的操作转换为查询表达式为：
```cs
(from v in arr where v > 3 select v).Sum();
```

所有 LINQ 查询操作都由以下三个不同的操作组成：
1. 获取数据源。
2. 创建查询。
3. 执行查询。

```cs
// The Three Parts of a LINQ Query:
//  1. Data source. 数据源
int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

// 2. Query creation.创建查询
// numQuery is an IEnumerable<int>
var numQuery =
    from num in numbers
    where (num % 2) == 0
    select num;

// 3. Query execution.执行查询
foreach (int num in numQuery)
{
    Console.Write("{0,1} ", num);
}
```

下图显示了完整的查询操作。 在 LINQ 中，查询的执行与查询本身截然不同；换句话说，如果只是创建查询变量，则不会检索任何数据。

![](..\assets\Programming\linq1.png)

<a id="markdown-反射" name="反射"></a>
## 反射
在运行期间处理和检测代码，反射指程序可以访问、检测和修改它本身状态或行为的一种能力。

程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。

您可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性。

<a id="markdown-反射reflection" name="反射reflection"></a>
### 反射(Reflection)
```cs
public class Student
{
    public Student(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
    public void Say(string name)
    {
        Console.WriteLine("hi {0},how do u do,i'm {1}", name, Name);
    }
}

static void Main(string[] args)
{
    //以下的type1、type2、type3的是相同的
    Student stu1 = new Student("");
    Type type1 = stu1.GetType();

    Type type2 = typeof(Student);

    Type type3 = Type.GetType("Reflection.Student");

    /*
    加载程序集中类型，并打印显示到控制台
    */
    Assembly a = Assembly.LoadFrom("Reflection.exe");
    Type[] types = a.GetTypes();
    foreach (Type item in types)
    {
        Console.WriteLine("类型名称为：" + item.Name);
    }

    /*
    反射查看类内的成员
    */
    Type type = typeof(Student);
    //获取类内的所有公开字段
    FieldInfo[] fieldInfos = type.GetFields();
    //获取类内所有公开属性
    PropertyInfo[] propInfos = type.GetProperties();
    //获取类内所有公开方法
    MethodInfo[] methodInfos = type.GetMethods();
    //获取类内所有公开成员，包含了字段、属性、方法等
    MemberInfo[] memInfos = type.GetMembers();
    foreach (MemberInfo item in memInfos)
    {
        Console.WriteLine("MemberType:{0},Name:{1}", item.MemberType, item.Name);
    }

    /*
    通过反射构造对象，调用方法
    */
    //使用指定类型的默认构造函数来创建该类型的实例，实例化一个对象
    object obj = Activator.CreateInstance(type, new object[] { "宋小宝" });
    //获取指定的方法 
    MethodInfo sayMethod = type.GetMethod("Say");
    //执行Student类中的Say方法
    var result = sayMethod.Invoke(obj, new object[] { "王富贵" });
}
```
看了上面的代码，也许会有疑问，既然在开发时就能够写好代码，干嘛还放到运行期去做，不光繁琐，而且效率也受影响。

很多设计模式是基于反射实现的，设计模式的好处是复用解决方案，可靠性高等。如何取舍是一个见仁见智的问题。。。

<a id="markdown-自定义特性attribute" name="自定义特性attribute"></a>
### 自定义特性(Attribute)
<a id="markdown-特性是什么" name="特性是什么"></a>
#### 特性是什么
Attribute 是一种可由用户自由定义的修饰符(Modifier)，可以用来修饰各种需要被修饰的目标。

简单的说，Attribute就是一种“附着物” —— 就像牡蛎吸附在船底或礁石上一样。

这些附着物的作用是为它们的附着体追加上一些额外的信息(这些信息就保存在附着物的体内)—— 比如这个属性对应数据库中哪个字段，这个类对应数据库中哪张表等等。

<a id="markdown-作用" name="作用"></a>
#### 作用
特性Attribute 的作用是添加元数据。

元数据可以被工具支持，比如：编译器用元数据来辅助编译，调试器用元数据来调试程序。

<a id="markdown-attribute-与注释的区别" name="attribute-与注释的区别"></a>
#### Attribute 与注释的区别
- 注释是对程序源代码的一种说明，主要目的是给人看的，在程序被编译的时候会被编译器所丢弃，因此，它丝毫不会影响到程序的执行。
- Attribute是程序代码的一部分，不但不会被编译器丢弃，而且还会被编译器编译进程序集(Assembly)的元数据(Metadata)里，在程序运行的时候，你随时可以从元数据里提取出这些附加信息来决策程序的运行。

<a id="markdown-使用" name="使用"></a>
#### 使用
自定义特性的定义：
```cs
public sealed class FieldChNameAttribute : Attribute
{
    public string ChName { get; set; }
    public FieldChNameAttribute() { }
    public FieldChNameAttribute(string chName)
    {
        ChName = chName;
    }
}
```

如何使用自定义特性：
```cs
[FieldChName(ChName = "学生实体")]
public class Student
{
    [FieldChName(ChName = "姓名")]
    public string Name { get; set; }

    [FieldChName(ChName = "年龄")]
    public int Age { get; set; }
    public string Course { get; set; }
}
```

检查类、属性是否有标记特性，以及获取特性的属性值：
```cs
static void Main(string[] args)
{
    Type type = typeof(Student);

    //获取Student类上自定义特性
    Attribute stuAttr = type.GetCustomAttribute(typeof(FieldChNameAttribute), false);

    //该类有FieldChaNameAttribute自定义特性，则获取设置的属性值ChName
    if (null != stuAttr)
    {
        FieldChNameAttribute stuChAttr = stuAttr as FieldChNameAttribute;
        Console.WriteLine(stuChAttr.ChName);//学生实体
    }

    //获取类型的所有公开属性
    PropertyInfo[] props = type.GetProperties();
    //遍历公开属性
    foreach (PropertyInfo pp in props)
    {
        //获取属性上的自定义特性
        Attribute ppAttr = pp.GetCustomAttribute(typeof(FieldChNameAttribute), false);
        //如属性有自定义特性，则获取设置的属性值ChName
        if (null != ppAttr)
        {
            FieldChNameAttribute ppChAttr = ppAttr as FieldChNameAttribute;
            Console.WriteLine(ppChAttr.ChName);
        }
    }
}
```

参考以下文章：

[C# 中自定义Attribute值的获取与优化](http://kb.cnblogs.com/page/87531/)

[关于C# 中的Attribute 特性](http://blog.csdn.net/hegx2001/article/details/50352225)

<a id="markdown-多线程" name="多线程"></a>
## 多线程
<a id="markdown-什么是进程" name="什么是进程"></a>
### 什么是进程？
当一个程序开始运行时，它就是一个进程，进程包括运行中的程序和程序所使用到的内存和系统资源。而一个进程又是由多个线程所组成的。

<a id="markdown-什么是线程" name="什么是线程"></a>
### 什么是线程？
线程是程序中的一个执行流，每个线程都有自己的专有寄存器(栈指针、程序计数器等)，但代码区是共享的，即不同的线程可以执行同样的函数。

线程初体验：
```cs
static void Main(string[] args)
{
    //设置当前线程名称，默认为null
    Thread.CurrentThread.Name = "My Thread Demo";
    Console.WriteLine(Thread.CurrentThread.Name);//打印当前线程名称
    Console.WriteLine(Thread.CurrentThread.ThreadState);//打印当前线程状态
}
```

<a id="markdown-线程创建" name="线程创建"></a>
### 线程创建
调用线程Thread类的构造函数进行创建：
```cs
static void Main(string[] args)
{
    /*
    ThreadStart是一个线程委托，可以理解为一个方法指针，指向一个方法的地址
    注意这个ThreadStart委托是无返回值无参，传递的方法也应该无返回值无参
    public delegate void ThreadStart();
    */
    Thread threadVoid = new Thread(new ThreadStart(Say));
    //启动该线程
    threadVoid.Start();

    /*
    同样的，ParameterizedThreadStart也是一个线程委托，无返回值，参数为object，传递的方法也需要满足这个条件
    public delegate void ParameterizedThreadStart(object obj);
    */
    Thread threadParam = new Thread(new ParameterizedThreadStart(Talk));
    //启动有参数线程
    threadParam.Start("王富贵");
}

static void Say()
{
    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine("Say Hi,time:" + DateTime.Now.ToString("mm:ss.fff"));
    }
}

static void Talk(object obj)
{
    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine(obj.ToString() + "'s Talk Show,time:" + DateTime.Now.ToString("mm:ss.fff"));
    }
}
```

<a id="markdown-线程阻塞" name="线程阻塞"></a>
### 线程阻塞
Thread.Sleep()和实例方法Join()，Sleep(int xxx)没有重载，而Join()方法有多个重载：
```cs
static void Main(string[] args)
{
    Thread worker = new Thread(delegate ()
    {
        Thread.Sleep(100);
        Console.WriteLine("new");
    });
    worker.Start();
    Console.WriteLine("main1");
    /*
    Join方法不传参数的时候，默认等待子线程执行完
    子线程的join会阻塞主线程，直到子线程终止。结果如下：
    main1
    new
    main2
    注释下面这行，如果没有子线程阻塞的话，则是：
    main1
    main2
    new
    */
    worker.Join();
    Console.WriteLine("main2");
}
```

<a id="markdown-lock" name="lock"></a>
### lock
```cs
/// <summary>
/// 用于多线程操作时锁，火车票有余票100张，多个线程同时进行买票操作，如何保证同时操作的时候余票的显示正确的
/// </summary>
static object lockObj = new object();

static int ticketCount = 100;

//随机对象，用于随机买票
static Random random = new Random();

static void Main(string[] args)
{
    Thread buyWork1 = new Thread(BuyTicket);
    buyWork1.Name = "王富贵";
    Thread buyWork2 = new Thread(BuyTicket);
    buyWork2.Name = "赵有才";
    Thread buyWork3 = new Thread(BuyTicket);
    buyWork3.Name = "郑钱花";

    buyWork1.Start();
    buyWork2.Start();
    buyWork3.Start();
}


/// <summary>
/// 买票操作，用于多线程委托
/// </summary>
static void BuyTicket()
{
    /*
    每个线程循环买票，一直买到没票为止
    while包含lock，每次循环间隙其他线程可以介入操作共享资源
    */
    while (ticketCount > 0)
    {
        lock (lockObj)
        {
            //生成一个[1,5]的随机数
            int cnt = random.Next(1, 5);
            Console.WriteLine("{0}需要购买{1}张票", Thread.CurrentThread.Name, cnt);
            if (ticketCount < cnt)
            {
                Console.WriteLine("!!!系统余票不足，请重新输入需要购买的票数!!!");
                return;
            }
            ticketCount -= cnt;
            Console.WriteLine("##余票提示##购买{0}张，剩余{1}张", cnt, ticketCount);
        }
    }
}
```

[Microsoft-“锁定”语句(C# 参考)](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/lock-statement)

推荐阅读：

[5天不再惧怕多线程系列](http://www.cnblogs.com/huangxincheng/archive/2012/03/14/2395279.html)