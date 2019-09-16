<!-- TOC -->

- [高级编程](#高级编程)
    - [委托、Lambda表达式和事件](#委托lambda表达式和事件)
        - [委托](#委托)
        - [Lambda表达式](#lambda表达式)
        - [预定义的委托类型](#预定义的委托类型)
            - [泛型委托-Predicate](#泛型委托-predicate)
            - [泛型委托-Action](#泛型委托-action)
            - [泛型委托-Func](#泛型委托-func)
        - [事件](#事件)
    - [Enumerable支持标准查询的操作符](#enumerable支持标准查询的操作符)
        - [匿名类型和隐式类型](#匿名类型和隐式类型)
        - [IEnumerable<T>](#ienumerablet)
        - [标准查询操作符](#标准查询操作符)
            - [Where筛选](#where筛选)
            - [Select投射](#select投射)
            - [使用Count()对元素进行计数](#使用count对元素进行计数)
            - [OrderBy和ThenBy排序](#orderby和thenby排序)
            - [GroupBy分组](#groupby分组)
    - [扩展方法](#扩展方法)
    - [反射](#反射)
        - [反射(Reflection)](#反射reflection)
            - [加载同程序集](#加载同程序集)
            - [加载不同程序集](#加载不同程序集)
        - [自定义特性(Attribute)](#自定义特性attribute)
            - [特性是什么](#特性是什么)
            - [作用](#作用)
            - [Attribute 与注释的区别](#attribute-与注释的区别)
            - [使用](#使用)
    - [自定义集合](#自定义集合)

<!-- /TOC -->
<a id="markdown-高级编程" name="高级编程"></a>
# 高级编程

<a id="markdown-委托lambda表达式和事件" name="委托lambda表达式和事件"></a>
## 委托、Lambda表达式和事件

<a id="markdown-委托" name="委托"></a>
### 委托
委托是安全封装方法的类型，类似于 C 和 C++ 中的函数指针。 

与 C 函数指针不同的是，委托是面向对象的、类型安全的和可靠的。

说白了，委托是一个类，将方法作为实参传递，实际传递的是方法地址/引用。

先看这个例子：

```cs
/// <summary>
/// 定义委托类型
/// </summary>
/// <param name="array"></param>
/// <returns></returns>
delegate void GetNumberDelegate(int[] array);

class TestOperationNumber
{
    /// <summary>
    /// 显示运算结果
    /// </summary>
    /// <param name="dele">委托变量，符合委托定义的方法</param>
    public void Show(GetNumberDelegate dele)
    {
        int[] arr1 = { 1, 1, 2, 3, 5, 8 };
        dele(arr1);
    }

    public void GetMax(int[] array)
    {
        int max = array[0];
        foreach (int item in array)
        {
            if (max < item)
            {
                max = item;
            }
        }
        Console.WriteLine("比较得到最大值：" + max);
    }

    public void GetMin(int[] array)
    {
        int min = array[0];
        foreach (int item in array)
        {
            if (min > item)
            {
                min = item;
            }
        }
        Console.WriteLine("比较得到最小值：" + min);
    }

    public void GetSum(int[] array)
    {
        int sum = 0;
        foreach (int item in array)
        {
            sum += item;
        }
        Console.WriteLine("遍历求和结果：" + sum);
    }
}

class Program
{
    static void Main(string[] args)
    {
        TestOperationNumber opt = new TestOperationNumber();
        //opt.Show(opt.GetMax);
        //opt.Show(opt.GetMin);
        //opt.Show(opt.GetSum);

        GetNumberDelegate d1 = new GetNumberDelegate(opt.GetMax);
        d1 += opt.GetSum;
        opt.Show(d1);
    }
}
```


```cs
//委托调用的方法和委托的定义必须保持一致，如下面的几个示例
void Say(){}
delegate void DelegateTalk();

string Say(){}
delegate string DelegateTalk();

bool Say(int value){}
delegate bool DelegateTalk(int value);
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
/// 无参，返回值为void的委托
/// </summary>
public delegate void DelegateBack();

static void BackHome(DelegateBack action)
{
    action();
}

static void BuyTicket()
{
    Console.WriteLine("买火车票");
}

static void Subway()
{
    Console.WriteLine("换乘地铁");
}
```

针对上例中的Main方法也可以修改为以下方式，以合并委托(多路广播委托)的方式实现
```cs
static void Main(string[] args)
{
    DelegateBack action = new DelegateBack(BuyTicket);
    action += Subway;

    BackHome(action);

    Console.ReadKey();
}
```

<a id="markdown-lambda表达式" name="lambda表达式"></a>
### Lambda表达式
Lambda 表达式是一种可用于创建 委托 或 表达式目录树 类型的 匿名函数 。 

通过使用 lambda 表达式，可以写入可作为参数传递或作为函数调用值返回的本地函数。 

Lambda 表达式对于编写 LINQ 查询表达式特别有用。

[Lambda 表达式(C# 编程指南)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions)

在 2.0 之前的 C# 版本中，声明委托的唯一方法是使用命名方法。  C# 2.0 引入了匿名方法，而在 C# 3.0 及更高版本中。

Lambda 表达式取代了匿名方法，作为编写内联代码的首选方式。

在C#2.0之前就有委托了，在2.0之后又引入了匿名方法，C#3.0之后，又引入了Lambda表达式。

他们三者之间的顺序是：委托->匿名表达式->Lambda表达式。

以下分别是三种对应不同的实现：
```cs
/// <summary>
/// 计算委托的类型
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <returns></returns>
delegate int DelegateCalculate(int x, int y);

/// <summary>
/// 执行计算方法，但具体执行什么运算并不清楚
/// </summary>
/// <param name="func">委托传入的方法</param>
/// <param name="x">操作数1</param>
/// <param name="y">操作数2</param>
/// <returns></returns>
static int DoCalc(DelegateCalculate func, int x, int y)
{
    // 执行运算前的某些操作...
    return func.Invoke(x, y);
    // 执行运算后的某些操作...
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

static void Main(string[] args)
{
    /*
    委托
    加法运算，最简单的委托方式，先定义方法Add，再进行传入
    */
    int res1 = DoCalc(Add, 1, 2);

    /*
    匿名方法
    减法运算，使用匿名方法形式传入
    */
    int res2 = DoCalc(delegate (int x, int y)
    {
        return x - y;
    }, 1, 2);

    /*
    Lambda表达式
    乘法运算，【=>】左侧(x,y)为参数，【=>】右侧为代码块
    若要创建 Lambda 表达式，需要在 Lambda 运算符 => 左侧指定输入参数(如果有)，然后在另一侧输入表达式或语句块。
    */
    int res3 = DoCalc((x, y) =>
    {
        return x * y;
    }, 2, 3);

    //上述乘法运算也可以简写为：
    int res4 = DoCalc((x, y) => x * y, 2, 3);
}
```

Lambda表达式"是一个特殊的匿名函数，是一种高效的类似于函数式编程的表达式，Lambda简化了开发中需要编写的代码量。

它可以包含表达式和语句，并且可用于创建委托或表达式目录树类型，支持带有可绑定到委托或表达式树的输入参数的内联表达式。

所有Lambda表达式都使用Lambda运算符=>，该运算符读作"goes to"。

Lambda运算符的左边是输入参数(如果有)，右边是表达式或语句块。

Lambda表达式x => x * x读作"x goes to x times x"。

上述示例也可以使用.NET预定义的委托Func<>进行替代，不需要新定义DelegateCalculate委托，如下：
```cs
/// <summary>
/// 计算委托的类型
/// 注释该委托，使用Func<>委托代替
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <returns></returns>
//delegate int DelegateCalculate(int x, int y);

/// <summary>
/// 执行计算方法
/// Func<int,int,int>前两个int为参数，最后一个int为返回值类型
/// </summary>
/// <param name="fun">委托传入的方法 使用.NET预定义的委托</param>
/// <param name="x">操作数1</param>
/// <param name="y">操作数2</param>
/// <returns></returns>
static int DoCalc(Func<int, int, int> fun, int x, int y)
{
    // 执行运算前的某些操作...
    return func.Invoke(x, y);
    // 执行运算后的某些操作...
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

<a id="markdown-预定义的委托类型" name="预定义的委托类型"></a>
### 预定义的委托类型

<a id="markdown-泛型委托-predicate" name="泛型委托-predicate"></a>
#### 泛型委托-Predicate

表示定义一组条件并确定指定对象是否符合这些条件的方法。

此委托由 Array 和 List 类的几种方法使用，用于在集合中搜索元素。

`public delegate bool Predicate<T>(T obj);`

类型参数介绍：
* T： 要比较的对象的类型。
* obj： 要按照由此委托表示的方法中定义的条件进行比较的对象。
* 返回值：bool，如果 obj 符合由此委托表示的方法中定义的条件，则为 true；否则为 false。

```cs
static void Main()
{
    List<string> listStr = new List<string>() {
        "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
    string[] arrStr = listStr.ToArray();

    /*
    目标：筛选出长度小于等于3的元素
    */

    //1、传统方法-遍历
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

    // public delegate bool Predicate<T>(T obj);
    //2、使用 Predicate 预先定义好方法的方式
    Predicate<string> pred1 = new Predicate<string>(GetFilter);
    List<string> list1 = listStr.FindAll(pred1);
    Console.WriteLine(string.Join(",", list1));


    //3、使用lambda表达式，一步筛选得出结论
    List<string> list2 = listStr.FindAll(t => t.Length <= 3);
    Console.WriteLine(string.Join(",", list2));
}

/// <summary>
/// 筛选元素
/// </summary>
/// <param name="val"></param>
/// <returns></returns>
static bool GetFilter(string val)
{
    return val.Length <= 3;
}
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



<a id="markdown-事件" name="事件"></a>
### 事件
事件(Event) 基本上说是一个用户操作，如按键、点击、鼠标移动等等，或者是一些出现，如系统生成的通知。

应用程序需要在事件发生时响应事件。例如，中断。事件是用于进程间通信。

事件在类中声明且生成，且通过使用同一个类或其他类中的委托与事件处理程序关联。

包含事件的类用于发布事件。这被称为 **发布器(publisher) 类**。

其他接受该事件的类被称为 **订阅器(subscriber) 类**。

事件使用 **发布-订阅(publisher-subscriber) 模型**。

**发布器(publisher)** 是一个包含事件和委托定义的对象。事件和委托之间的联系也定义在这个对象中。发布器(publisher)类的对象调用这个事件，并通知其他的对象。

**订阅器(subscriber)** 是一个接受事件并提供事件处理程序的对象。在发布器(publisher)类中的委托调用订阅器(subscriber)类中的方法(事件处理程序)。

使用`Action<>`类型定义事件，添加事件监听并触发。

```cs
class Student
{
    /// <summary>
    /// 打招呼事件，等价于委托 delegate void DelegatePrint(string xxx); 
    /// 仅仅只是定义了可以进行打招呼，但具体怎么打招呼，由外部进行确定
    /// </summary>
    public Action<string> GreetEvent;
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();
        s1.GreetEvent = new Action<string>(SayHi);//添加注册事件
        s1.GreetEvent("jack");

        Console.WriteLine("===========================");
        Student s2 = new Student();
        s2.GreetEvent = new Action<string>(SayHi);//添加注册事件
        s2.GreetEvent += SayNice;//添加注册事件
        s2.GreetEvent("lucy");
    }

    static void SayHi(string name)
    {
        Console.WriteLine($"hi,{name}");
    }

    static void SayNice(string name)
    {
        Console.WriteLine($"nice to meet you,{name}");
    }
}
```

上面的示例中并未对【GreetEvent】添加event关键字，故可以直接通过【对象.事件名】进行触发。

普通委托添加【event】关键字后，只能由成员方法进行调用触发。

```cs
class Student
{
    /// <summary>
    /// 打招呼事件，等价于委托 delegate void DelegatePrint(string xxx); 
    /// 仅仅只是定义了可以进行打招呼，但具体怎么打招呼，由外部进行确定
    /// 使用event关键字为了避免直接在对象上进行委托实例的调用，如【stu1.GreetEvent("xxx");】
    /// 使用event关键，无法在类外部通过对象名进行重新赋值，只允许进行添加和移除操作
    /// </summary>
    public event Action<string> GreetEvent;

    /// <summary>
    /// 开始打招呼
    /// </summary>
    /// <param name="otherName"></param>
    public void StartGreet(string otherName)
    {
        if (GreetEvent != null)
        {
            GreetEvent.Invoke(otherName);
        }
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();
        //s1.GreetEvent = new Action<string>(SayHi);
        s1.GreetEvent += SayHi;
        //s1.GreetEvent("jack");
        s1.StartGreet("jack");

        Console.WriteLine("===========================");
        Student s2 = new Student();
        //s2.GreetEvent = new Action<string>(SayHi);
        s2.GreetEvent += SayHi;
        s2.GreetEvent += SayNice;
        //s2.GreetEvent("jack");
        s2.StartGreet("lucy");
    }

    static void SayHi(string name)
    {
        Console.WriteLine($"hi,{name}");
    }

    static void SayNice(string name)
    {
        Console.WriteLine($"nice to meet you,{name}");
    }
}
```

以上案例，【event】这样的设计规范，【event】只允许在类的成员方法中进行Invoke，为了明确事件发送对象。

【event】只允许通过【对象.event】添加/移除事件订阅，不允许重新实例化。

针对上面案例进一步修改，对某一对象添加事件订阅和移除事件订阅。

```cs
class Student
{
    /// <summary>
    /// 打招呼事件，等价于委托 delegate void DelegatePrint(string xxx); 
    /// 仅仅只是定义了可以进行打招呼，但具体怎么打招呼，由外部进行确定
    /// 使用event关键字为了避免直接在对象上进行委托实例的调用，如【stu1.GreetEvent("xxx");】
    /// 使用event关键，无法在类外部通过对象名进行重新赋值，只允许进行添加和移除操作
    /// </summary>
    public event Action<string> GreetEvent;

    /// <summary>
    /// 开始打招呼
    /// </summary>
    /// <param name="otherName"></param>
    public void StartGreet(string otherName)
    {
        if (GreetEvent != null)
        {
            GreetEvent.Invoke(otherName);
        }
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();
        s1.GreetEvent += SayHi;
        s1.StartGreet("jack");

        Console.WriteLine("===========================");
        Student s2 = new Student();
        s2.GreetEvent += SayHi;
        s2.GreetEvent += SayNice;

        // 以lambda表达式方式添加事件注册，仅一次性使用，无法进行注册移除
        s2.GreetEvent += (s) =>
        {
            Console.WriteLine($"你好，{s}");
        };
        s2.GreetEvent -= SayNice; //移除事件注册
        s2.StartGreet("lucy");
    }

    static void SayHi(string name)
    {
        Console.WriteLine($"hi,{name}");
    }

    static void SayNice(string name)
    {
        Console.WriteLine($"nice to meet you,{name}");
    }
}
```

<a id="markdown-enumerable支持标准查询的操作符" name="enumerable支持标准查询的操作符"></a>
## Enumerable支持标准查询的操作符

<a id="markdown-匿名类型和隐式类型" name="匿名类型和隐式类型"></a>
### 匿名类型和隐式类型
匿名类型是由编译器声明的数据类型，当编译器看到匿名类型时，会执行一些后台操作，生成这个类，并允许像已经显式声明过它那样使用。

```cs
var book1 = new { Title = "黄金时代", Auth = "王小波", Price = 29 };
var book2 = new { Title = book1.Title };
```

匿名类型纯粹是一个C#语言特性，不是"运行时"中的一种新类型。

需要注意的是，除非赋给变量的类型能一眼看出，否则应该只有在声明匿名类型（具体类型只有在编译时才能确定）时，才使用隐式类型的变量。

不要不分青红皂白地使用隐式类型(var)的变量，这里的var和JavaScript中的var是不一样的概念。

**匿名类型的安全性和不可变性**

```cs
var book1 = new { Title = "黄金时代", Auth = "王小波", Price = 29 };
var book2 = new { Title = book1.Title };

// 隐式转换类型
//book1 = book2;

// 无法为属性赋值，它是只读的
//book1.Title = "青铜时代";
```

匿名类型之间不兼容，并且匿名类型是不可变的，所以匿名类型一经实例化，就无法修改其属性值。

<a id="markdown-ienumerablet" name="ienumerablet"></a>
### IEnumerable<T>
集合实质上就是一个类，实现了`IEnumerable<T>`接口。

这个接口非常重要，要想支持对集合执行的遍历操作，最起码要求就是实现IEnumerable<T>接口。

C#编译器不要求一定要实现IEnumerable/IEnumerable<T>才能用foreach对一个数据类型进行迭代。

相反，编译器采用一个称为"Duck typing"的概念；也就是查找一个GetEnumerator()方法，这个方法返回包含Current属性和MoveNe×t()方法的一个类型。

Duck typing按名称查找方法，而不是依赖接口或显式方法调用。

如果找不到可枚举模式的恰当实现，编译器就检查集合是否实现了接口。

**foreach循环内不要修改集合！！！**

<a id="markdown-标准查询操作符" name="标准查询操作符"></a>
### 标准查询操作符

`IEnumerable<T>`上的每个方法都是一个标准查询操作符，用于为所操作的集合提供查询功能。

以下案例均基于Inventor和Patent类，代码如下：

```cs
/// <summary>
/// 商品类
/// </summary>
public class Product
{
    public Product(long prodId, string name, string productLocation, double price)
    {
        ProdId = prodId;
        Name = name;
        ProductLocation = productLocation;
        Price = price;
    }

    public long ProdId { get; set; }
    public string Name { get; set; }
    public string ProductLocation { get; set; }
    public double Price { get; set; }
    /// <summary>
    /// 重写ToString方法，方便打印输出
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Name}(产地：{ProductLocation},价格：{Price})";
    }
}

/// <summary>
/// 供应商
/// </summary>
public class Provider
{
    public Provider(long providerId, string name, string city, List<long> prodList)
    {
        ProviderId = providerId;
        Name = name;
        City = city;
        ProdList = prodList;
    }

    public long ProviderId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    /// <summary>
    /// 可供应商品
    /// </summary>
    public List<long> ProdList { get; set; }
    public override string ToString()
    {
        return $"{City}-{Name}";
    }
}


public static class TestData
{
    /// <summary>
    ///  商品数组，测试数据
    /// </summary>
    public static readonly Product[] ProductsArray = new Product[] {
        new Product(1, "黑人牙膏", "芜湖", 12),
        new Product(2, "佳洁士牙膏", "芜湖", 4.5),
        new Product(3, "黑人牙刷", "合肥", 5.5),
        new Product(4, "舒克牙刷", "合肥", 9.9),
        new Product(5, "心相印抽纸", "合肥", 10.9),
        new Product(6, "清风抽纸", "合肥", 12.9),
    };

    /// <summary>
    /// 供应商数组，测试数据
    /// </summary>
    public static readonly Provider[] ProvidersArray = new Provider[] {
        new Provider(101, "苏宁小店", "芜湖市弋江区", new List<long>() { 1, 2 }),
        new Provider(102, "苏宁小店", "芜湖市镜湖区", new List<long>() { 3 }),
        new Provider(103, "呆萝卜", "芜湖市镜湖区", new List<long>() { 3, 4 }),
        new Provider(104, "呆萝卜", "芜湖市鸠江区", new List<long>() { 4, 5 }),
        new Provider(105, "京东小店", "合肥市庐阳区", new List<long>() { 4, 5 }),
        new Provider(106, "京东小店", "合肥市蜀山区", new List<long>() { 6 }),
    };
}

public class Program
{
    static void Main(string[] args)
    {
        IEnumerable<Product> products = TestData.ProductsArray;
        Print(products);

        IEnumerable<Provider> providers = TestData.ProvidersArray;
        Print(providers);
    }

    /// <summary>
    /// 泛型方法，集合对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    static void Print<T>(IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            Console.WriteLine(item.ToString());
        }
    }
}
```

<a id="markdown-where筛选" name="where筛选"></a>
#### Where筛选
从集合中筛选出数据，需要提供筛选器方法返回true或false以指明特定的元素是否应该被包含进来。

获取一个实参并返回一个布尔值的lambda表达式称为**谓词**。

集合的`Where()`方法依据谓词来确定筛选条件，下面案例是筛选所有价格大于10的商品

```cs
// 筛选所有价格大于10的商品
var priceThan10 = TestData.ProductsArray.Where(t => t.Price > 10);
Console.WriteLine("****************筛选所有价格大于10的商品****************");
Print(priceThan10);
```

<a id="markdown-select投射" name="select投射"></a>
#### Select投射
由于`IEnumerable<T>.Where()`输出的是一个新的`IEnumerable<T>`集合，所以完全可以在这个集合的基础上再调用另一个标准查询操作符。

例如，从原始集合中筛选好数据后，可以接着对这些数据进行转换，如下所示：

```cs
var priceThan10 = TestData.ProductsArray.Where(t => t.Price > 10);
// 生成新的字符串结合
IEnumerable<string> prodInfo = priceThan10.Select(t => $"{t.Name}(价格：{t.Price})");
Console.WriteLine("****************筛选所有价格大于10的商品，新投影的列****************");
Print(prodInfo);
```

匿名类型，在创建`IEnumerable<T>`集合时，T可以是匿名类型，如下使用`Select()`投射匿名类型

```cs
// 获取当前路径下所有文件
IEnumerable<string> fileList = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory);
// 重新投影，选择文件名称和文件大小
var items = fileList.Select(t =>
{
    FileInfo info = new FileInfo(t);
    return new { FileName = info.Name, Size = $"{info.Length / 1024 }Kb" };
});
Console.WriteLine("****************获取当前debug目录下所有文件，重新投影名称和大小****************");
Print(items);
```

在为匿名类型生成的`ToString()`方法中，会自动添加用于显示属性名称及其值的代码。

使用`Select()`进行“投射"，这是非常强大的一个功能。

上一节讲述了如何使用`Where()`标准查询操作符在“垂直"方向上筛选集合工（减少集合中项的数量）。

现在，使用`Select()`标准查询操作符，还可以在“水平"方向上减小集合的规模（减少列的数量）或者对数据进行彻底的转换。

综合运用`Where()`和`Select()`，可以获得原始集合的一个子集，从而满足当前算法的要求。

这两个方法各自提供了一个功能强大的、对集合进行操纵的API。

<a id="markdown-使用count对元素进行计数" name="使用count对元素进行计数"></a>
#### 使用Count()对元素进行计数

```cs
Console.WriteLine($"Products Count:{TestData.ProductsArray.Count()}");
Console.WriteLine($@"Product's price than ￥10 :{
    TestData.ProductsArray.Count(t => t.Price > 10)}");
```

虽然`Count()`语句看起来简单，但IEnumerable<T>没有改变，所以真正执行的代码仍然会遍历集合中的所有项。

如果集合直接提供一个Count属性，就应该首选属性，而不要用LINQ的`Count()`方法（这是一个许多人都没有意识到的差异）。

幸好，`ICollection<T>`包含了Count属性，所以如果集合支持`ICollection<T>`，那么在它上面调用Count()方法，会对集合进行转型，并直接调用Count。

```cs
// 常用的 List 实现了`ICollection<T>`接口，即针对List可以直接使用Count属性
public class List<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable{}
```

然而，如果不支持`ICollection<T>`,`Enumerable.Count()`就会枚举集合中的所有项，而不是调用内建的Count机制。

如果计数的目的只是为了看这个计数是否大于0，那么首选的做法是使用Any()操作符。

Any()只尝试遍历集合中的一个项，如果成功就返回true,而不会遍历整个序列。如下例所示：

```cs
// 效率低
if(TestData.ProductsArray.Count() > 0) {...}

// 建议采用
if(TestData.ProductsArray.Any()) {...}
```

<a id="markdown-orderby和thenby排序" name="orderby和thenby排序"></a>
#### OrderBy和ThenBy排序

基于上面的案例，使用Price作为排序的键，返回的仍然是IEnumerable集合类型

```cs
var orderPrice = TestData.ProductsArray.OrderBy(t => t.Price);
Console.WriteLine("****************按price价格升序排列****************");
Print(orderPrice);
```

ThenBy用于多个列的分组，先按照产品产地排序，再按照价格进一步排序，如下所示：
```cs
var orderPrice = TestData.ProductsArray.OrderBy(t => t.ProductLocation)
    .ThenBy(t => t.Price);
Console.WriteLine("****************先按产地排序，再按照price价格排序****************");
Print(orderPrice);
```

<a id="markdown-groupby分组" name="groupby分组"></a>
#### GroupBy分组

```cs
var locationGroup = TestData.ProductsArray.GroupBy(t => t.ProductLocation);
Console.WriteLine("****************按照产地分组，再遍历分组明细****************");
// 遍历得到的分组
foreach (var item in locationGroup)
{
    Console.WriteLine($"key:{item.Key},Count:{item.Count()}");
    foreach (var prod in item)
    {
        Console.WriteLine(prod);
    }
}
```

注意，`GroupBy()`返回的是`IEnumerable<IGrouping<TKey, TSource>>`类型的数据项，

如果需要对多个列进行分组，参考如下代码：
```cs
// 按照多个列进行分组，产地和取整价格
var locationGroup = TestData.ProductsArray.GroupBy(prod => new
{
    Location = prod.ProductLocation,
    PriceFloor = Math.Floor(prod.Price)
});
Console.WriteLine("****************按照产地和取整价格进行分组****************");
// 遍历得到的分组
foreach (var item in locationGroup)
{
    Console.WriteLine($"分组Key:{item.Key},Count:{item.Count()}");
    foreach (var prod in item)
    {
        Console.WriteLine(prod);
    }
}
```

针对上述案例的petsList修改，可以通过第二个传参指定返回匿名类型
```cs
// 按照指定key进行group by，并返回新的匿名类型
var locationGroup = TestData.ProductsArray.GroupBy(prod => prod.ProductLocation,
    prod =>
    {
        // 投影新的查询对象
        return new { Name = $"{prod.Name}-{prod.ProductLocation}", PriceFloor = Math.Floor(prod.Price) };
    });
Console.WriteLine("****************按照产地和取整价格进行分组****************");
// 遍历得到的分组
foreach (var item in locationGroup)
{
    Console.WriteLine($"分组Key:{item.Key},Count:{item.Count()}");
    foreach (var prod in item)
    {
        Console.WriteLine(prod);
    }
}
```

在分组时也可以基于分组产生新的分组信息，如下：
```cs
/*
生成的不再是 IEnumerable<IGrouping<string, Product>> 类型，而是IEnumerable<>集合
第1个参数是按照哪个列进行分组，
第2个参数是每个分组里的数据处理，
第3个参数是分组后每组数据处理，产生新的匿名类型，(key,keyList)=>{ return new {}}
*/
var query = TestData.ProductsArray.GroupBy(prod => prod.ProductLocation,
    prod => prod,
    (location, groupProducts) =>
    {
        return new
        {
            Location = location,
            Size = groupProducts.Count(),
            MaxPrice = groupProducts.Max(t => t.Price),
            MinPrice = groupProducts.Min(t => t.Price),
        };
    });
Console.WriteLine("****************按照城市分组，并查找分组内最高/最低价格****************");
foreach (var item in query)
{
    Console.WriteLine(item);
}
```

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

<a id="markdown-反射" name="反射"></a>
## 反射
在运行期间处理和检测代码，反射指程序可以访问、检测和修改它本身状态或行为的一种能力。

程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。

使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性。

<a id="markdown-反射reflection" name="反射reflection"></a>
### 反射(Reflection)
<a id="markdown-加载同程序集" name="加载同程序集"></a>
#### 加载同程序集

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
    public void Say(string otherName)
    {
        Console.WriteLine("hi {0},how do u do,i'm {1}", otherName, Name);
    }
}

public class Program
{
    static void Main(string[] args)
    {
        //以下的type1、type2、type3的是相同的
        Student stu1 = new Student("");
        Type type1 = stu1.GetType();

        Type type2 = typeof(Student);

        // Reflection 为命名空间名称
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

}
```
看了上面的代码，也许会有疑问，既然在开发时就能够写好代码，干嘛还放到运行期去做，不光繁琐，而且效率也受影响。

很多设计模式是基于反射实现的，设计模式的好处是复用解决方案，可靠性高等。如何取舍是一个见仁见智的问题。。。

<a id="markdown-加载不同程序集" name="加载不同程序集"></a>
#### 加载不同程序集
通过加载其他程序集中的类，进行实例化对象和调用方法：

新建类库项目，默认命名【ClassLibrary1】，然后创建【Teacher】类，结构内容如下：

![](../assets/Programming/Assembly-Teacher.png)
```cs
namespace ClassLibrary1
{
    public class Teacher
    {
        public string Name { get; set; }
        public Teacher(string name) { Name = name; }
        public void Say(string msg)
        {
            Console.WriteLine($"hello {msg},i'm {Name}");
        }
    }
}
```

新增控制台应用程序项目，编译上一步中的类库项目，并拷贝【ClassLibrary1.dll】文件至控制台应用程序的【debug】目录

![](../assets/Programming/AssemblyLoad-1.png)

在控制台应用程序的【Main】方法中进行加载程序集并调用，代码如下：
```cs
// 装载程序集 ClassLibrary1.dll，在当前目录中
Assembly ass = Assembly.LoadFrom("ClassLibrary1.dll");

// 获取该程序集中定义的 Teacher类，注意要写全名FullName，如 ClassLibrary1.Teacher
Type t = ass.GetType("ClassLibrary1.Teacher");

// 需要考虑找不到该类型的情况
if (null != t)
{
    object jack = Activator.CreateInstance(t, "宋小宝");
    MethodInfo method = t.GetMethod("Say");
    method.Invoke(jack, new object[] { "这是通过assembly反射进行调用的方法" });
}
```

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

    // 获取Student类上自定义特性
    FieldChNameAttribute attr = type.GetCustomAttribute(typeof(FieldChNameAttribute), false) as FieldChNameAttribute;

    //该类有FieldChaNameAttribute自定义特性，则获取设置的属性值ChName
    if (null != attr)
    {
        // 学生类上的标签注解
        Console.WriteLine(attr.ChName);
    }

    // 获取类型的所有公开属性
    PropertyInfo[] props = type.GetProperties();
    
    // 遍历公开属性
    foreach (PropertyInfo pp in props)
    {
        // 获取属性上的自定义特性
        FieldChNameAttribute ppAttr = pp.GetCustomAttribute(typeof(FieldChNameAttribute), false) as FieldChNameAttribute;
        // 如属性有自定义特性，则获取设置的属性值ChName
        if (null != ppAttr)
        {
            Console.WriteLine(ppAttr.ChName);
        }
    }
}
```

<a id="markdown-自定义集合" name="自定义集合"></a>
## 自定义集合
在System.Collections 命名空间下，常用的集合类中，有两个类不属于集合，而是作为自定义集合类的基类。 

* CollectionBase:为强类型集合提供abstract 基类 
* DictionaryBase:为键/值对的强类型集合提供abstract基类。 

如果我们对自定义集合有更多要求的话，比如：
* 能够通过索引号去访问集合中的某个元素，则需要定义集合的**索引器**
* 能够通过foreach循环遍历每一个元素，则需要定义集合的**迭代器**

```cs
class Program
{
    static void Main(string[] args)
    {
        StudentCollection stuCollection = new StudentCollection();
        stuCollection.Add(new Student("jack"));
        stuCollection.Add(new Student("lucy"));

        //使用迭代器，因为CollectionBase实现了IEnumerable接口，所以可以直接使用foreach
        foreach (Student item in stuCollection)
        {
            item.SayHi();
        }

        //使用索引器进行方法调用
        stuCollection[1].SayHi();
    }
}

/// <summary>
/// 自定义CollectionBase集合
/// </summary>
public class StudentCollection : CollectionBase
{
    /// <summary>
    /// 重写父类中的Add方法，因为父类Add为私有方法，元数据中不可见
    /// CollectionBase源码中可见父类中实现了Add方法
    /// https://referencesource.microsoft.com/#mscorlib/system/collections/collectionbase.cs
    /// </summary>
    /// <param name="stu"></param>
    /// <returns></returns>
    public int Add(Student stu)
    {
        return List.Add(stu);
    }

    /// <summary>
    /// Remove方法同上Add方法，都是私有实现
    /// </summary>
    /// <param name="stu"></param>
    public void Remove(Student stu)
    {
        List.Remove(stu);
    }

    /// <summary>
    /// 父类中为普通方法，不可重写，只能使用new进行隐藏
    /// </summary>
    /// <param name="index"></param>
    public new void RemoveAt(int index)
    {
        List.RemoveAt(index);
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Student this[int index]
    {
        get { return List[index] as Student; }
        set { List[index] = value; }
    }
}

public class Student
{
    public Student(string name) { Name = name; }
    public string Name { get; set; }
    public void SayHi() { Console.WriteLine($"hello i'm {Name}"); }
}
```

关于迭代，foreach遍历是C#常见的功能，C#使用yield关键字让自定义集合实现foreach遍历的方法

一般来说当我们创建自定义集合的时候为了让其能支持foreach遍历，就只能让其实现IEnumerable接口（可能还要实现IEnumerator接口）

但是我们也可以通过使用yield关键字构建的迭代器方法来实现foreach的遍历，且自定义的集合不用实现IEnumerable接口

```cs
class Program
{
    static void Main(string[] args)
    {
        StudentList sts = new StudentList();
        foreach (Student item in sts)
        {
            item.SayHi();
        }
    }
}

public class StudentList
{
    private Student[] arr = new Student[3];
    public StudentList()
    {
        arr[0] = new Student("张三");
        arr[1] = new Student("李四");
        arr[2] = new Student("王富贵");
    }

    public IEnumerator GetEnumerator()
    {
        foreach (Student item in arr)
        {
            // yield return 作用就是返回集合的一个元素,并移动到下一个元素上
            yield return item;
        }
    }
}

public class Student
{
    public Student(string name) { Name = name; }
    public string Name { get; set; }
    public void SayHi() { Console.WriteLine($"hello i'm {Name}"); }
}
```

注意：**虽然不用实现IEnumerable接口 ，但是迭代器的方法必须命名为GetEnumerator()，返回值也必须是IEnumerator类型。**

---

参考引用：

[C# 中自定义Attribute值的获取与优化](http://kb.cnblogs.com/page/87531/)

[关于C# 中的Attribute 特性](http://blog.csdn.net/hegx2001/article/details/50352225)
