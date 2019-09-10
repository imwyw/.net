<!-- TOC -->

- [常用类库](#常用类库)
    - [字符串](#字符串)
        - [成员方法和属性](#成员方法和属性)
        - [静态方法](#静态方法)
        - [不可变性](#不可变性)
        - [字符串池（只针对字符串常量）](#字符串池只针对字符串常量)
        - [StringBuilder](#stringbuilder)
        - [String和string](#string和string)
        - [string.Empty/空字符串/null](#stringempty空字符串null)
    - [特殊字符](#特殊字符)
        - [$字符串内插](#字符串内插)
        - [@逐字字符串标识符](#逐字字符串标识符)
    - [DateTime](#datetime)
    - [数据集合DataCollection](#数据集合datacollection)
        - [Array](#array)
        - [ArrayList](#arraylist)
        - [Hashtable](#hashtable)
        - [排序列表SortedList](#排序列表sortedlist)
        - [堆栈Stack](#堆栈stack)
        - [队列Queue](#队列queue)
    - [泛型](#泛型)
        - [泛型类](#泛型类)
        - [泛型特性Generic](#泛型特性generic)
        - [泛型方法](#泛型方法)
        - [性能和类型安全](#性能和类型安全)
        - [类型推断](#类型推断)
        - [泛型约束](#泛型约束)
            - [引用类型约束](#引用类型约束)
            - [值类型约束](#值类型约束)
            - [构造函数类型约束](#构造函数类型约束)
            - [转换类型约束](#转换类型约束)
            - [组合约束](#组合约束)
    - [泛型字典`Dictionay<Tkey,Tvalue>`](#泛型字典dictionaytkeytvalue)
    - [异常处理](#异常处理)
        - [bug和debug](#bug和debug)
        - [异常案例](#异常案例)
        - [捕获与处理](#捕获与处理)
        - [自定义异常和异常处理链](#自定义异常和异常处理链)
        - [开发中异常处理建议](#开发中异常处理建议)

<!-- /TOC -->

<a id="markdown-常用类库" name="常用类库"></a>
# 常用类库

<a id="markdown-字符串" name="字符串"></a>
## 字符串
<a id="markdown-成员方法和属性" name="成员方法和属性"></a>
### 成员方法和属性

方法/属性 | 说明
---------------------|-----------------
Contains(String str) | 判断字符串中是否包含指定字符串。
StartsWith(String str) | 判断字符串对象是否以指定字符串开头。
EndWith(String str) | 判断字符串对象是否以指定字符串结尾。
Length属性 | 获取字符串的长度
IndexOf(String str) | 获取指定字符/字符串.....在对象字符串中第一次出现的位置，从0开始
LastIndexOf(String str) | 获取指定字符/字符串....在对象字符串中最后一次 出现的位置。
SubString(int start) | 从指定位置，截取字符串。
SubString(int strat, int length) | 从指定位置，截取length长度字符串
ToLower()/ToUpper() | 将字符串转换成小写/大写，返回一个新的全小写/大写的字符串。
Replace(string oldStr,string newStr) | 用新的字符串，替换对象字符串中老的字符串部分。
Trim()/TrimStart()/TrimEnd() | 去掉对象字符串两端/开始/结尾的空格
Split() | 把对象字符串，按照指定字符分割成一个字符串数组！


<a id="markdown-静态方法" name="静态方法"></a>
### 静态方法

方法名 | 说明
----|---
string.IsNullOrEmpty(string) | 判断某字符串是否为null，或者为空字符串，为null或空时返回真值
string.Equals(string,string,StringComparison.OrdianlIgnore) | 忽略大小写比较两个字符串是否相同。
string.Join(string,string[]) | 把一个数组按照指定字符串，拼接成一个字符串。、
string.Format()  |  将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。

<a id="markdown-不可变性" name="不可变性"></a>
### 不可变性
string，首先是引用类型，由于字符串是不可变的，每次修改字符串，都是创建了一个单独字符串副本（拷贝了一个字符串副本）。

之所以发生改变只是因为指向了一块新的地址。

```cs
string str1 = "hello";
string str2 = "hello";

/* 即时窗口中调试代码：
&str1
0x0632e9c8
    *&str1: {41186072}
&str2
0x0632e9c4
    *&str2: {41186072}


(41186072).ToString("x8")
"02747318"
*/
```

![](../assets/programming/string-内存地址.png)

<a id="markdown-字符串池只针对字符串常量" name="字符串池只针对字符串常量"></a>
### 字符串池（只针对字符串常量）
当一个程序中有多个相同的字符串常量时，多个变量指向的是内存中同一块字符串！

这个特性叫字符串池。不会造成程序混乱，是因为字符串的不可变性。

<a id="markdown-stringbuilder" name="stringbuilder"></a>
### StringBuilder
在需要对字符串执行重复修改的情况下，与创建新的 String 对象相关的系统开销可能会非常昂贵。

如果要修改字符串而不创建新的对象，则可以使用System.Text.StringBuilder 类

例如，当在一个循环中将许多字符串连接在一起时，使用 StringBuilder 类可以提升性能。

<a id="markdown-string和string" name="string和string"></a>
### String和string
String是.NET  Framework里面的String，小写的string是C#语言中的string。

用C#编写代码的情况下尽量使用小写的string，比较符合规范，

如果在追求效率的情况下可以使用大写的String，因为最终通过编译后，小写的string会变成大写的String，可以给编译减少负荷，从而运行效率提高。

MSDN中对string的说明：string is an alias for String in the .NET Framework

<a id="markdown-stringempty空字符串null" name="stringempty空字符串null"></a>
### string.Empty/空字符串/null
**""与string.Empty在用法与性能上基本没区别。**string.Empty是在语法级别对""的优化。

这个结论可以使用string不可变性的方式查看内存地址来进行校验。

**string.Empty会在堆上占用一个长度为0的空间，而null不会。**

```cs
string str1 = "";

string str2 = null;
```

str1会在栈上保存一个地址,这个地址占4字节，指向内存堆中的某个长度为0的空间，这个空间保存的是str1的实际值。

str2同样会在栈上保存一个地址,这个地址也占4字节，但是这个地址是没有明确指向的，它哪也不指，其内容为0x00000000。

<a id="markdown-特殊字符" name="特殊字符"></a>
## 特殊字符

<a id="markdown-字符串内插" name="字符串内插"></a>
### $字符串内插
`$` 特殊字符将字符串文本标识为内插字符串。 此功能在 C# 6 及该语言的更高版本中可用。

与使用string.Format()中`{0}`占位方式相比，字符串内插提供的语法更具可读性，且更加方便。

```cs
string name = "jack";
//需要在字符串前加 $ 符号
Console.WriteLine($"hello {name}");// hello jack
```

<a id="markdown-逐字字符串标识符" name="逐字字符串标识符"></a>
### @逐字字符串标识符
1、@ 字符可作为代码元素的前缀，编译器将把此代码元素解释为标识符而非 C# 关键字。

下面的示例使用 @ 字符定义其在 for 循环中使用的名为 for 的标识符。

```cs
int @for = 1;
```

但通常来说都不会这样使用

2、指示将原义解释字符串。
```cs
string filename1 = @"c:\documents\files\u0066.txt";
string filename2 = "c:\\documents\\files\\u0066.txt";
```

<a id="markdown-datetime" name="datetime"></a>
## DateTime
DateTime是.NET中的时间类型，可以通过DateTime完成诸如获取当前的系统时间等操作。

需要注意的是，DateTime在.NET中是一个结构体，而并不是一个类。

方法/属性 | 说明
------|---
Now | 获取当前系统时间。格式：2018/9/5 8:27:02
Today | 获取今天的日期。格式：2018/9/5 0:00:00
Now.Year | 获取年
Now.Month | 获取月
Now.Day | 获取日
Now.Hour | 获取小时
Now.Minute | 获取分钟
Now.Second | 获取秒
DayOfWeek | 获取当前日期是星期几
DayOfYear | 以及获取当前日期是一年中的第几天
Parse() | 转换为DateTime对象
TryParse() | 判断是否是时间类型，参数中有一个out可以输出一个DateTime对象。
AddDays()、AddHours() | 在当前时间基础上加几天/几个小时，返回一个DateTime
Subtract(DateTime.Now) | 比较两个时间的 时间差 | 返回一个TimeSpan

DateTime.ToString()的各种日期格式
```cs
//常用的预定义模式
DateTime.Now.ToString("d");// ShortDatePattern  2018/9/5
DateTime.Now.ToString("D");// LongDatePattern  2018年9月5日
DateTime.Now.ToString("f");// 完整日期和时间（长日期和短时间） 2018年9月5日 9:03
DateTime.Now.ToString("F");// FullDateTimePattern（长日期和长时间）  2018年9月5日 9:05:15
DateTime.Now.ToString("g");// 常规（短日期和短时间） 2018/9/5 9:05
DateTime.Now.ToString("G");// 常规（短日期和长时间） 2018/9/5 9:05:54
DateTime.Now.ToString("m");//DateTime.Now.ToString("M");  9月5日
```

DateTime的字符串格式除了预定义还有自定义的模式，如下：
* d 月中的某一天。一位数的日期没有前导零。 
* dd 月中的某一天。一位数的日期有一个前导零。 
* ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。 
* dddd 周中某天的完整名称，在 DayNames 中定义。 
* M 月份数字。一位数的月份没有前导零。 
* MM 月份数字。一位数的月份有一个前导零。 
* MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。 
* MMMM 月份的完整名称，在 MonthNames 中定义。 
* y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。 
* yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。 
* yyyy 包括纪元的四位数的年份。 
* gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。 
* h 12 小时制的小时。一位数的小时数没有前导零。 
* hh 12 小时制的小时。一位数的小时数有前导零。 
* H 24 小时制的小时。一位数的小时数没有前导零。 
* HH 24 小时制的小时。一位数的小时数有前导零。 
* m 分钟。一位数的分钟数没有前导零。 
* mm 分钟。一位数的分钟数有一个前导零。 
* s 秒。一位数的秒数没有前导零。 
* ss 秒。一位数的秒数有一个前导零。

例如：
```cs
DateTime.Now.ToString("yyyy-MM-dd");//2018-09-05
DateTime.Now.ToString("yyyy-MM-dd HH");//2018-09-05 09
```

<a id="markdown-数据集合datacollection" name="数据集合datacollection"></a>
## 数据集合DataCollection

<a id="markdown-array" name="array"></a>
### Array
Array类是一个抽象类，无法实例化该类。主要作为基类进行扩展，实际使用中很少直接使用Array类。

但可以通过它的一些静态方法来进行数组的创建、遍历、拷贝、排序等操作，下面给出部分示例：
```cs
/*
以下两种初始化的方式是等效的
*/
string[] arr = { "a", "b", "c" };

Array arr = Array.CreateInstance(typeof(string), 3);
arr.SetValue("a", 0);
arr.SetValue("b", 1);
arr.SetValue("c", 2);
```

<a id="markdown-arraylist" name="arraylist"></a>
### ArrayList
在System.Collections命名空间下，同时继承了IList接口。

ArrayList对象的大小可按照其中存储的数据进行动态增减，所以在声明ArrayList对象时并不需要指定它的长度。

但是由于ArrayList会把插入其中的所有数据当作为object类型来处理，在存储或检索值类型时会发生装箱和拆箱操作，带来很大的性能耗损。

另外，ArrayList没有泛型的实现，也就是ArrayList不是类型安全的，在我们使用ArrayList处理数据时，很可能会报类型不匹配的错误。

因为ArrayList存在不安全类型与装箱拆箱的缺点，所以出现了泛型的概念。
```cs
//没有类型的约束，数值、字符串、布尔值均可以作为元素
ArrayList arrList = new ArrayList();
arrList.Add(1);//装箱 int->object
arrList.Add("abc");//装箱 string->object
arrList.Add(true);//装箱 bool->object
```

<a id="markdown-hashtable" name="hashtable"></a>
### Hashtable
Hashtable也并非类型安全的，用于处理和表现类似key-value的键值对，其中key通常可用来快速查找，同时key是区分大小写；

value用于存储对应于key的值。Hashtable中key-value键值对均为object类型，所以Hashtable可以支持任何类型的key-value键值对.

```cs
Hashtable ht = new Hashtable();
ht.Add(0, 1);
ht.Add("key", "name");
ht.Add(1.23, true);

//添加一个keyvalue键值对：
ht.Add(key,value);

//移除某个keyvalue键值对：
ht.Remove(key);

//移除所有元素：           
ht.Clear(); 

// 判断是否包含特定键key：
ht.Contains(key);
```

什么情况下应该使用Hashtable：
1. 某些数据会被高频率查询
2. 数据量大
3. 查询字段包含字符串类型
4. 数据类型不唯一

<a id="markdown-排序列表sortedlist" name="排序列表sortedlist"></a>
### 排序列表SortedList
SortedList 类代表了一系列按照键来排序的键/值对，这些键值对可以通过键和索引来访问。

排序列表是数组和哈希表的组合。它包含一个可使用键或索引访问各项的列表。

如果您使用索引访问各项，则它是一个动态数组（ArrayList）；

如果您使用键访问各项，则它是一个哈希表（Hashtable）。

集合中的各项总是按键值排序。

```cs
//需要添加 using System.Collections;
SortedList sl = new SortedList();
sl.Add("002", "钱二");
sl.Add("001", "赵一");
sl.Add("003", "孙三");
sl.Add("004", "李四");

//通过key值进行访问
Console.WriteLine(sl["001"]);

//虽然添加时并未排序，但遍历时会按照key进行排序
foreach (var item in sl.Keys)
{
    Console.WriteLine("遍历：" + sl[item]);
}
```

<a id="markdown-堆栈stack" name="堆栈stack"></a>
### 堆栈Stack
堆栈（Stack）代表了一个后进先出LIFO的对象集合。当您需要对各项进行后进先出的访问时，则使用堆栈。

当您在列表中添加一项，称为推入元素，当您从列表中移除一项时，称为弹出元素。

* Count：返回栈中所包含的元素个数
* Clear：删除所有的项
* Peek：返回一个处于调用栈顶端的对象的引用，但不删除它。
* Pop：返回并删除顶端的对象
* Push：向栈中添加指定的对象

```cs
//需要添加 using System.Collections;
Stack st = new Stack();
st.Push(1);
st.Push("abc");
st.Push(true);
st.Push(new object());

Console.WriteLine("长度：" + st.Count);

Console.WriteLine(st.Peek());
Console.WriteLine("长度：" + st.Count);

Console.WriteLine(st.Pop());
Console.WriteLine("长度：" + st.Count);
```

<a id="markdown-队列queue" name="队列queue"></a>
### 队列Queue
队列（Queue）代表了一个先进先出的对象集合。

当您需要对各项进行先进先出的访问时，则使用队列。

当您在列表中添加一项，称为入队，当您从列表中移除一项时，称为出队。类似排队的效果。

```cs
//需要添加 using System.Collections;
Queue qu = new Queue();
qu.Enqueue("赵一");
qu.Enqueue("钱二");
qu.Enqueue("孙三");
qu.Enqueue("李四");

foreach (var item in qu)
{
    Console.WriteLine("入队后：" + item);
}
qu.Dequeue();
foreach (var item in qu)
{
    Console.WriteLine("出队：" + item);
}
```

<a id="markdown-泛型" name="泛型"></a>
## 泛型
**泛型（Generic）**允许您延迟编写类或方法中的编程元素的数据类型的规范，直到实际在程序中使用它的时候。

换句话说，泛型允许您编写一个可以与任何数据类型一起工作的类或方法。

泛型本来代表的就是通用类型，国内翻译作泛型，其实可以理解为一个模子。

在生活中，我们经常会看到模子，像我们平常生活中用的桶就是一个模子，
 
我们可以用桶子装水，也可以用来装油，牛奶等等，然而把这些都装进桶子里面之后，
 
它们都会具有桶的形状（水，牛奶和油本来是没有形的），即具有模子的特征。

<a id="markdown-泛型类" name="泛型类"></a>
### 泛型类
定义一个泛型类：
```cs
/// <summary>
/// 泛型数组类
/// 数组类型由调用者决定
/// </summary>
/// <typeparam name="T"></typeparam>
public class MyGenericArray<T>
{
    private T[] array;
    public MyGenericArray(int size)
    {
        array = new T[size + 1];
    }
    public T getItem(int index)
    {
        return array[index];
    }
    public void setItem(int index, T value)
    {
        array[index] = value;
    }
}
```

在Main方法中调用：
```cs
// 声明一个整型数组
MyGenericArray<int> intArray = new MyGenericArray<int>(5);
// 设置值
for (int c = 0; c < 5; c++)
{
    intArray.setItem(c, c*5);
}
// 获取值
for (int c = 0; c < 5; c++)
{
    Console.Write(intArray.getItem(c) + " ");
}
Console.WriteLine();
// 声明一个字符数组
MyGenericArray<char> charArray = new MyGenericArray<char>(5);
// 设置值
for (int c = 0; c < 5; c++)
{
    charArray.setItem(c, (char)(c+97));
}
// 获取值
for (int c = 0; c < 5; c++)
{
    Console.Write(charArray.getItem(c) + " ");
}
Console.WriteLine();
Console.ReadKey();
```

当上面的代码被编译和执行时，它会产生下列结果：

```
0 5 10 15 20
a b c d e
```

<a id="markdown-泛型特性generic" name="泛型特性generic"></a>
### 泛型特性Generic

使用泛型是一种增强程序功能的技术，具体表现在以下几个方面：
1. 它有助于最大限度地重用代码、保护类型的安全以及提高性能。
2. 可以创建泛型集合类。.NET 框架类库在 System.Collections.Generic 命名空间中包含了一些新的泛型集合类。您可以使用这些泛型集合类来替代 System.Collections 中的集合类。
3. 可以创建自己的泛型接口、泛型类、泛型方法、泛型事件和泛型委托。
4. 可以对泛型类进行约束以访问特定数据类型的方法。
5. 关于泛型数据类型中使用的类型的信息可在运行时通过使用反射获取。

<a id="markdown-泛型方法" name="泛型方法"></a>
### 泛型方法

```cs
/// <summary>
/// 泛型方法
/// 同样的交换逻辑，不同数据类型的交换不需要重复的定义方法，调用的时候决定类型即可
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="t1"></param>
/// <param name="t2"></param>
static void Swap<T>(ref T t1, ref T t2)
{
    T temp = t1;
    t1 = t2;
    t2 = temp;
}

static void Main()
{
    int a = 1, b = 2;
    string str1 = "hello", str2 = "world";

    Swap<int>(ref a, ref b);
    Swap<string>(ref str1, ref str2);

    Console.WriteLine("交换后a:{0}\t b:{1}", a, b);
    Console.WriteLine("交换后str1:{0}\t str2:{1}", str1, str2);
}
```

<a id="markdown-性能和类型安全" name="性能和类型安全"></a>
### 性能和类型安全
泛型可以实现代码的重用，还可以提供更好的性能和类型安全。

```cs
// 计时
Stopwatch watcher = new Stopwatch();
int COUNT = 10 * 1000 * 1000;

// 非泛型数组
ArrayList arraylist = new ArrayList();

// 泛型数组
List<int> genericlist = new List<int>();

watcher.Start();
for (int i = 1; i < COUNT; i++)
{
    genericlist.Add(i);
}
watcher.Stop();

// 输出所用的时间
Console.WriteLine($"泛型集合运行的时间：{watcher.ElapsedMilliseconds}(毫秒)");

watcher.Start();
for (int i = 1; i < COUNT; i++)
{
    arraylist.Add(i);
}
watcher.Stop();

// 输出所用的时间
Console.WriteLine($"非泛型集合运行的时间：{watcher.ElapsedMilliseconds}(毫秒)");
```

从两个结果中就可以明显看出 向泛型数组中的加入数据的效率远高于非泛型数组。

这样就充分说明泛型的另一个好处【高性能】，并且泛型类型也保证了类型安全，上例中的genericlist无法添加string类型的值。

<a id="markdown-类型推断" name="类型推断"></a>
### 类型推断
类型推断，意味着编译器会在调用一个泛型方法时自动判断要使用的类型。

要注意的是：类型推断只使用于泛型方法，不适用于泛型类型。

```cs
static void Main(string[] args)
{
    int n1 = 1;
    int n2 = 2;
    // 没有类型推断时需要写的代码
    GenericMethodTest<int>(ref n1, ref n2);

    // 有了类型推断后需要写的代码
    // 此时编译器可以根据传递的实参 1和2来判断应该使用Int类型实参来调用泛型方法
    // 可以看出有了类型推断之后少了<>,这样代码多的时候可以增强可读性
    GenericMethodTest(ref n1, ref n2);
    Console.WriteLine("n1的值现在为：" + n1);
    Console.WriteLine("n2的值现在为：" + n2);
    Console.Read();

    //string t1 = "123";
    //object t2 = "456";
    //// 此时编译出错，不能推断类型
    //// 使用类型推断时，C#使用变量的数据类型，而不是使用变量引用对象的数据类型
    //// 所以下面的代码会出错，因为C#编译器发现t1是string，而t2是一个object类型
    //// 即使 t2引用的是一个string,此时由于t1和t2是不同数据类型，编译器所以无法推断出类型，所以报错。
    //GenericMethodTest(ref t1, ref t2);
}

/// <summary>
/// 类型推断的Demo
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="t1"></param>
/// <param name="t2"></param>
static void GenericMethodTest<T>(ref T t1, ref T t2)
{
    T temp = t1;
    t1 = t2;
    t2 = temp;
}
```

<a id="markdown-泛型约束" name="泛型约束"></a>
### 泛型约束
在声明泛型方法/泛型类的时候，可以给泛型加上一定的约束来满足我们特定的一些条件。

C#中有4种约束可以使用，然而这4种约束的语法都差不多。（约束要放在泛型方法或泛型类型声明的末尾，并且要使用Where关键字）

<a id="markdown-引用类型约束" name="引用类型约束"></a>
#### 引用类型约束
表示形式为 `T:class`， 确保传递的类型实参必须是引用类型

```cs
public class SampleReference<T> where T : Stream
{
    public void Test(T stream)
    {
        stream.Close();
    }
}
```

上面代码中类型参数T设置了引用类型约束，Where T:stream的意思就是告诉编译器，

传入的类型实参必须是System.IO.Stream或者从Stream中派生的一个类型.

如果一个类型参数没有指定约束，则默认T为System.Object类型，但是并不能显示的指定System.Object约束。

但是注意不能指定下面特殊的引用类型：
```cs
System.Object,System.Array,System.Delegate,System.MulticastDelegate,System.ValueType,System.Enum,System.Void.
```

<a id="markdown-值类型约束" name="值类型约束"></a>
#### 值类型约束
表示形式为 `T:struct`，确保传递的类型实参时值类型，其中包括枚举，但是不包括可空类型。

```cs
public class SampleValueType<T> where T : struct
{
    public static T CreateValue()
    {
        return new T();
    }
}
```

在上面代码中，`new T()`是可以通过编译的。

因为T 是一个值类型，而所有值类型都有一个公共的无参构造函数。

如果T不约束，或约束为引用类型时，此时上面的代码就会报错，因为有的引用类型没有公共的无参构造函数的。

<a id="markdown-构造函数类型约束" name="构造函数类型约束"></a>
#### 构造函数类型约束
表示形式为 `T:new()`，如果类型参数有多个约束时，此约束必须为最后指定。

确保指定的类型实参有一个公共无参构造函数的非抽象类型，适用于所有值类型，所有非静态、非抽象、没有显示声明的构造函数的类。

<a id="markdown-转换类型约束" name="转换类型约束"></a>
#### 转换类型约束
表示形式：
* `T:基类名` （确保指定的类型实参必须是基类或派生自基类的子类）
* `T:接口名`（确保指定的类型实参必须是接口或实现了该接口的类）
* `T:U`（为 T 提供的类型参数必须是为 U 提供的参数或派生自为 U 提供的参数）

声明 | 已构造类型的例子
---|---------
`Class Sample<T> where T: Stream` | `Sample<Stream>`有效的，`Sample<string>`无效的
`Class Sample<T> where T: IDisposable` | `Sample<Stream>`有效的，`Sample<StringBuilder>`无效的
`Class Sample<T,U> where T: U` | `Sample<Stream,IDispsable>`有效的，`Sample<string,IDisposable>`无效的

<a id="markdown-组合约束" name="组合约束"></a>
#### 组合约束
将多个不同种类的约束合并在一起的情况就是组合约束了。

如果存在多个转换类型约束时，如果其中一个是类，则类必须放在接口的前面。

不同的类型参数可以有不同的约束，但是他们分别要由一个单独的where关键字。

```cs
// 有效的组合约束
class Sample<T> where T:class, IDisposable, new() {}
class Sample<T,U> where T:class where U: struct {}

// 无效的组合约束
class Sample<T> where T: class, struct {} //(没有任何类型即时引用类型又是值类型的,所以为无效的)

class Sample<T> where T: Stream, class {} //(引用类型约束应该为第一个约束，放在最前面,所以为无效的)

class Sample<T> where T: new(), Stream {} //(构造函数约束必须放在最后面，所以为无效)

class Sample<T> where T: IDisposable, Stream {} //(类必须放在接口前面，所以为无效的)

class Sample<T,U> where T: struct where U:class, T {} //(类型形参“T”具有“struct”约束，因此“T”不能用作“U”的约束,所以为无效的)

class Sample<T,U> where T:Stream, U:IDisposable {} //(不同的类型参数可以有不同的约束，但是他们分别要由一个单独的where关键字,所以为无效的)
```

<a id="markdown-泛型字典dictionaytkeytvalue" name="泛型字典dictionaytkeytvalue"></a>
## 泛型字典`Dictionay<Tkey,Tvalue>`
在初始化的时候也必须指定其类型，而且他还需要指定一个Key,并且这个Key是唯一的。

正因为这样，Dictionary的索引速度非常快。但是也因为他增加了一个Key,Dictionary占用的内存空间比其他类型要大。

他是通过Key来查找元素的，元素的顺序是不定的。

```cs
Dictionary<string, string> dicRes = new Dictionary<string, string>();
dicRes.Add("zhangsan","张三");
dicRes.Add("zhaofugui", "赵富贵");
```

<a id="markdown-异常处理" name="异常处理"></a>
## 异常处理

<a id="markdown-bug和debug" name="bug和debug"></a>
### bug和debug

![](../assets/programming/bug.png)

**Bug** : 程序代码中隐藏的错误

**Debug** ：修正代码中存在的错误的过程，简称“程序调试”

**开发中可能会遇到的几种错误**

* 语法错误：在编译时发现，修改代码后可以顺利通过编译。
* 运行时错误：程序代码能顺利通过编译，但在运行时出现错误（比如用户输入了无效的数据），如果处理不当，程序可能会非法退出。
* 逻辑错误：程序代码能顺利通过编译，在运行时也没有出现错误，但就是结果不对 ……


<a id="markdown-异常案例" name="异常案例"></a>
### 异常案例

异常是在程序执行期间出现的问题。C# 中的异常是对程序运行时出现的特殊情况的一种响应，比如尝试除以零。

![](../assets/programming/divide-by-zero-exception.png)

程序将会中断，也就是常说的崩掉了，最终CLR会结束整个进程。

<a id="markdown-捕获与处理" name="捕获与处理"></a>
### 捕获与处理

输入一个正整数，针对用户的输入通常需要判断：
1. 未进行输入直接回车
2. 输入字符串无法转换为数字
3. 输入过大的数字
4. 输入不符合要求的数字

C# 异常处理时建立在四个关键词之上的：try、catch、finally 和 throw。

* try：一个 try 块标识了一个将被激活的特定的异常的代码块。后跟一个或多个 catch 块。
* catch：程序通过异常处理程序捕获异常。catch 关键字表示异常的捕获。
* finally：finally 块用于执行给定的语句，不管异常是否被抛出都会执行。例如，如果您打开一个文件，不管是否出现异常文件都要被关闭。
* throw：当问题出现时，程序抛出一个异常。使用 throw 关键字来完成。

```cs
static void Main(string[] args)
{
    Console.WriteLine("请输入一个正整数：");
    try
    {
        // 将用户输入内容转换为数字
        int value = int.Parse(Console.ReadLine());
        if (value <= 0)
        {
            throw new InvalidOperationException("你输入的不是正整数！");
        }
        Console.WriteLine("你输入的数字是：{0}", value);
    }
    catch (FormatException formatEx)
    {
        Console.WriteLine("输入的字符串无法转换为数字");
    }
    catch (OverflowException overEx)
    {
        Console.WriteLine("你输入的数字太大了");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.WriteLine("按任意键退出。。。");
    }
}
```

可以列出多个 catch 语句捕获不同类型的异常，以防 try 块在不同的情况下生成多个异常。

**异常基类Exception的重要属性和方法：**

名称 | 用途
---|---
e.GetType() | 获取异常的类型。
e.Message | 告诉用户发生了什么事。
e.StackTrace | 确定错误发生的位置， 如果有可用的调试信息（即有<程序名>.pdb文件存在） ， 还可显示源文件名和程序行号。

**异常的作用：**
* 从异常中恢复-比如遇到数据库连接错误，不能让程序崩溃，而是截获这个异常，提示用户并回到正常的运行轨道上来。
* 事务回滚-比如进行一系列的数据操作，突然其中某一个数据操作发生错误，应该能让此系列操作全部撤销。

**常用异常类型：**

异常 | 说明
---|---
ArithmeticException | 在 算 术 运 算 期 间 发 生 的 异 常 （ 如DivideByZeroException和OverflowException） 的基类。
DivideByZeroException | 在试图用零作除数时引发。
IndexOutOfRangeException | 在试图使用小于零或超出数组界限的下标索引数组时引发。
InvalidCastException | 从基类型或接口到派生类型的显式转换在运行时失败， 引发此异常。
NullReferenceException | 尝试使用未创建的对象， 引发此异常。
OutOfMemoryException | 分配内存（通过new） 失败时引发。
OverflowException | 在checked上下文中的算术运算溢出时引发。
StackOverflowException | 当执行堆栈由于保存了太多挂起的方法调用而耗尽时， 就会引发此异常， 这通常表明存在非常深或无限的递归。

**如果你不知道发生了异常怎么处理，那就不要进行try...catch...处理！**

```cs
try
{
    int[] arr = { 1, 2, 3 };
    Console.WriteLine(arr[3]);
}
catch (Exception ex)
{
    Console.WriteLine("发生异常：" + ex);
    //throw new Exception("索引超出数组界限");
}
Console.WriteLine("运行完毕");
```

_异常的发生是件好事，它让程序员知道自己的程序可能存在着Bug，_

_并且异常的出现还会通知用户有错误发生，他的数据有可能被破坏，从而让用户有机会考虑补救措施。_

_这远比将所有“错误”包起来不让用户知道要理智得多。_


<a id="markdown-自定义异常和异常处理链" name="自定义异常和异常处理链"></a>
### 自定义异常和异常处理链
创建自己的异常类：
```cs
// 创建异常类
class MyException : Exception
{

}

class SomeClass
{
    void SomeMethod()
    {
        if (condition)
        {
            // 抛出自定义异常对象
            throw new MyException();
        }
    }
}
```

在实际运行时，最底层的组件捕获一些异常，进行简单处理之后，可以将其转换为自定义的异常类型，

再抛出供上层组件处理，而上层组件又可以重复这个工作，再把工作委托给再上层的组件处理……，由此即可构成一个“异常处理链”。

```cs
static void Main(string[] args)
{
    Method1();
}

static void Method1()
{
    try
    {
        Method2();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Method1");
        Console.WriteLine(ex);
    }
}

static void Method2()
{
    try
    {
        Method3();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Method2");
        throw ex;
    }
}

static void Method3()
{
    try
    {
        throw new InvalidDataException("输入格式有误");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Method3");
        throw ex;
    }
}
```

<a id="markdown-开发中异常处理建议" name="开发中异常处理建议"></a>
### 开发中异常处理建议

* 引发异常只是为了处理确实异常的情况，而不是为了处理可预知的事件或实现某种程序流程控制。
* 自身能够处理的异常，不要再向外界抛出。
* 尽可能地在靠近异常发生的地方捕获并处理异常。
* 在中间层组件中抛出异常，在界面层组件中捕获异常。
* 尽可能地捕获最具体的异常类型，不要在中间层用catch(Exception)“吃掉”所有异常
* 在底层组件中捕获CLR抛出的“只有程序员能看懂的”异常，转换为中间层的业务逻辑异常，再由界面层捕获以提供有意义的信息
* 在开发阶段捕获并显示所有异常信息，发布阶段要移除部分代码，以避免“过于专业”的异常信息困扰用户，特别地，系统发布之后，不要将服务端异常的详细信息显示给客户端，以免被黑客利用。

---

参考引用：

[.NET常用类库知识总结](https://www.cnblogs.com/1030351096zzz/p/6234896.html)
