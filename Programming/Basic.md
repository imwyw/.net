<!-- TOC -->

- [编程基础](#编程基础)
    - [数据类型](#数据类型)
        - [变量](#变量)
            - [值类型](#值类型)
            - [引用类型](#引用类型)
            - [类型系统](#类型系统)
        - [常量](#常量)
            - [const](#const)
            - [枚举](#枚举)
            - [const/readonly](#constreadonly)
        - [类型转换](#类型转换)
            - [隐式转换](#隐式转换)
            - [显式转换](#显式转换)
            - [用户定义的转换](#用户定义的转换)
            - [使用帮助程序类进行转换](#使用帮助程序类进行转换)
        - [装箱(boxing)、拆箱(unboxing)](#装箱boxing拆箱unboxing)
        - [结构体](#结构体)
            - [结构体和类](#结构体和类)
    - [流程控制](#流程控制)
        - [顺序](#顺序)
        - [分支](#分支)
        - [循环](#循环)
    - [数组](#数组)
        - [一维数组](#一维数组)
        - [循环](#循环-1)
        - [多维数组](#多维数组)
    - [方法](#方法)
        - [形参实参](#形参实参)
        - [重载](#重载)
        - [参数传递](#参数传递)
            - [值传递](#值传递)
            - [引用传递](#引用传递)
            - [输出参数](#输出参数)
            - [params](#params)

<!-- /TOC -->

<a id="markdown-编程基础" name="编程基础"></a>
# 编程基础

<a id="markdown-数据类型" name="数据类型"></a>
## 数据类型

<a id="markdown-变量" name="变量"></a>
### 变量
一个变量只不过是一个供程序操作的存储区的名字。

在 C# 中，每个变量都有一个特定的类型，类型决定了变量的内存大小和布局。

例如字符串的定义：
```cs
string str  = "Hello iflytek";
```

<a id="markdown-值类型" name="值类型"></a>
#### 值类型
原类型（sbyte、byte、short、ushort、int、uint、long、ulong、char、float、double、bool、decimal）、枚举(enum)、结构(struct)

**整形类型**，下表描述的是常见的整型类型：byte、short、int、long

| C#类型 | 位数 | CTS类型      | 取值范围                                 |
| ------ | ---- | ------------ | ---------------------------------------- |
| byte   | 8    | System.Byte  | 0~255                                    |
| short  | 16   | System.Int16 | -32768~32767                             |
| int    | 32   | System.Int32 | -2147483648~2147483647                   |
| long   | 64   | System.Int64 | -9223372036854775808~9223372036854775807 |

**浮点类型**

| C#类型  | 位数 | CTS类型        | 取值范围                             |
| ------- | ---- | -------------- | ------------------------------------ |
| float   | 32   | System.Single  | 1.5E-45~3.4E+38，以 f 结束           |
| double  | 64   | System.Double  | 5E-324~1.7E+308，默认不写或以 d 结束 |
| decimal | 128  | System.Decimal | 1E-28~7.9E+28，以 m 结束             |

**其它类型**

| C#类型     | 位数 | CTS类型        | 取值范围                           |
| ---------- | ---- | -------------- | ---------------------------------- |
| char       | 16   | System.Char    | 0~65535                            |
| **string** | 16   | System.String  | 可变大小的内存，因此没有字符数上限 |
| bool       | 32   | System.Boolean | true、false                        |

值类型就是在栈中分配内存，在申明的同时就初始化，以确保数据不为NULL；

```cs
int a;
Console.WriteLine(a);//会报错，a未进行赋值

//在类属性中的应用，既是不赋值也是有初始值的
class Student
{
    public int Age {get;set;}
    public bool IsMarry {get;set;}
}
//实例化一个对象，不进行任何的赋值操作，对象属性会有初始值
Student s1 = new Student();
Console.WriteLine(s1.Age);
Console.WriteLine(s1.IsMarry);
```

<a id="markdown-引用类型" name="引用类型"></a>
#### 引用类型
类、数组、接口、委托、字符串等。

引用型是在堆中分配内存，初始化为null，引用型是需要GC(GARBAGE COLLECTION)来回收内存的。值类型不需要GC，超出了作用范围，系统就会自动释放！

<a id="markdown-类型系统" name="类型系统"></a>
#### 类型系统
![](../assets/Programming/类型系统.jpg)

<a id="markdown-常量" name="常量"></a>
### 常量

<a id="markdown-const" name="const"></a>
#### const
常量就是值不可被更改的一种数据，常量一经声明和初始化之后，就不能再对其进行赋值操作，也不可以更改它的内容。

区别于变量，在类型前加了关键字`const`

常量定义方式：`const <type> <varName> = <initialValue>;`

```cs
const double PI = 3.14;
```

<a id="markdown-枚举" name="枚举"></a>
#### 枚举
枚举是一组命名整型常量。枚举类型是使用 enum 关键字声明的。

C# 枚举是值类型。换句话说，枚举包含自己的值，且不能继承或传递继承。

语法如下：
```cs
//enum_name 指定枚举的类型名称。
enum <enum_name>
{ 
    //enumeration list 是一个用逗号分隔的标识符列表。
    enumeration list 
};
```

枚举列表中的每个符号代表一个整数值，一个比它前面的符号大的整数值。

默认情况下，第一个枚举符号的值是 0.例如：

```cs
enum Week
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

static void Main(string[] args)
{
    Console.WriteLine(Week.Monday);//Monday
    Console.WriteLine((int)Week.Monday);//1

    Week today = Week.Tuesday;
    //1、枚举类型转换为数值
    int today1 = (int)today;
    //2、数值转换为枚举类型
    Week resWeek = (Week)Enum.Parse(typeof(Week), "5");//resWeek为5对应的Friday
    //3、数值转换为枚举字符串
    string str = Enum.GetName(typeof(Week), 6);//Saturday
}
```

<a id="markdown-constreadonly" name="constreadonly"></a>
#### const/readonly
readonly为运行时常量，程序运行时进行赋值，赋值完成后便无法更改，因此也有人称其为只读变量。

const为编译时常量，程序编译时将对常量值进行解析，并将所有常量引用替换为相应值。

下面声明两个常量：
```cs
public static readonly int A = 2; //A为运行时常量
public const int B = 3; //B为编译时常量
```

下面的表达式：
```cs
int C = A + B;
```

经过编译后与下面的形式等价：
```cs
int C = A + 3;
```
其中的const常量B被替换成字面量3，而readonly常量A则保持引用方式。

readonly常量只能声明为类字段，支持实例类型或静态类型，可以在声明的同时初始化或者在构造函数中进行初始化，初始化完成后便无法更改。

const常量除了可以声明为类字段之外，还可以声明为方法中的局部常量，默认为静态类型(无需用static修饰，否则将导致编译错误)，但必须在声明的同时完成初始化。

性能上来说：const直接以字面量形式参与运算，性能要略高于readonly，但对于一般应用而言，这种性能上的差别可以说是微乎其微。

在下面两种情况下可以使用const常量，除此之外的其他情况都应该优先采用readonly常量：
1. 取值永久不变(比如圆周率、一天包含的小时数、地球的半径等)
2. 对程序性能要求非常苛刻

<a id="markdown-类型转换" name="类型转换"></a>
### 类型转换

由于 C# 是在编译时静态类型化的，因此变量在声明后就无法再次声明，或无法分配另一种类型的值，除非该类型可以隐式转换为变量的类型。 

例如，string 无法隐式转换为 int。 因此，在将 i 声明为 int 后，无法将字符串“Hello”分配给它，如以下代码所示：
```cs
int i;  
i = "Hello"; // error CS0029: Cannot implicitly convert type 'string' to 'int'
```
在 C# 中，可以执行以下几种类型的转换：隐式转换、显式转换（强制转换）、用户定义的转换、使用帮助程序类进行转换

<a id="markdown-隐式转换" name="隐式转换"></a>
#### 隐式转换
对于内置数值类型，如果要存储的值无需截断或四舍五入即可适应变量，则可以进行隐式转换。 

例如，long 类型的变量（64 位整数）能够存储 int（32 位整数）可存储的任何值。
```cs
int num = 2147483647;
long bigNum = num;
```

<a id="markdown-显式转换" name="显式转换"></a>
#### 显式转换
如果进行转换可能会导致信息丢失，则编译器会要求执行显式转换，显式转换也称为强制转换。
```cs
//下面的程序将 double 强制转换为 int
double x = 1234.7;
int a;
// Cast double to int.
a = (int)x;
Console.WriteLine(a);// Output: 1234
```

<a id="markdown-用户定义的转换" name="用户定义的转换"></a>
#### 用户定义的转换
可以定义一些特殊的方法来执行用户定义的转换，使不具有基类和派生类关系的自定义类型之间可以显式和隐式转换。

<a id="markdown-使用帮助程序类进行转换" name="使用帮助程序类进行转换"></a>
#### 使用帮助程序类进行转换
int、bool、DateTime类型提供的Parse方法，以及System.Convert 类提供的标准转换方法。

<a id="markdown-装箱boxing拆箱unboxing" name="装箱boxing拆箱unboxing"></a>
### 装箱(boxing)、拆箱(unboxing)
装箱就是隐式的将一个值型转换为引用型对象。
```cs
//将i进行装箱操作
int i = 0;
object obj = i;
```

拆箱就是将一个引用型对象转换成任意值型
```cs
针对上例中的obj进行拆箱
int res = (int)obj;
```

```cs
int i = 0;//L1
object obj = i;//L2
Console.WriteLine(i + "," + (int)obj);//L3
```
上述代码实际进行了三次装箱、一次拆箱

L2行，i装箱为obj，1次装箱；

L3行，WriteLine参数应该为字符串，即i又进行一次装箱，变为string引用类型，2次装箱；

L3行，(int)obj对obj对象进行拆箱转换为值类型，1次拆箱；

L3行，(int)obj的结果为值类型，控制台打印输出需要又一次的装箱为string类型，3次装箱；

频繁的装拆箱会造成性能的损耗！！！

<a id="markdown-结构体" name="结构体"></a>
### 结构体
结构体是值类型数据结构。它使得一个单一变量可以存储各种数据类型的相关数据。struct 关键字用于创建结构体。

结构存放在栈中并按值传递，和存放在堆中的类对象相比，它们具有性能上的优势。
1. 值类型的分配优于引用类型。
2. 存放在栈中的值一离开作用域就立即被收回。不用等待垃圾回收器来完成工作。

```cs
//定义结构体
public struct Books
{
    //定义字段
    public string _field;
    
    //定义属性
    public string PropertyName { get; set;}
    
    //定义方法
    public void Say(){ }
};  
```

<a id="markdown-结构体和类" name="结构体和类"></a>
#### 结构体和类
类和结构有以下几个基本的不同点：
* 类是引用类型，结构是值类型。
* 结构不支持继承。
* 结构不能声明默认的构造函数。

以下代码体现了，结构体在参数传递时实际传递的是副本，而不是像类一样传递引用。
```cs
//定义Student类
public class Student
{
    public string Name { get; set; }
}

//定义StructStu结构体
public struct StructStu
{
    public string Name { get; set; }
}

class Program
{
    static void ResetName(StructStu stu)
    {
        stu.Name = "default";
    }

    static void ResetName(Student stu)
    {
        stu.Name = "default";
    }

    static void Main(string[] args)
    {
        Student stu1 = new Student();
        stu1.Name = "jack";

        StructStu stu2 = new StructStu();
        stu2.Name = "jack";

        ResetName(stu1);
        ResetName(stu2);

        Console.WriteLine("修改后的值：" + stu1.Name);
        Console.WriteLine("修改后的值：" + stu2.Name);
    }
}
```

和引用类型相比，结构越复杂，复制造成的性能开销越大。因此，结构应该只用来表示小的数据结构。

<a id="markdown-流程控制" name="流程控制"></a>
## 流程控制
<a id="markdown-顺序" name="顺序"></a>
### 顺序
程序渐进的流程，一条语句一条语句地逐条推进，按预先设置好的步骤来完成功能

<a id="markdown-分支" name="分支"></a>
### 分支
if...else...

switch

if语句每执行一次都要先判断条件表达式是true还是false，为true时执行相应语句，若为false则继续判断下一个表达式，直到最后一个else结束。线性执行。 

switch语句只需在入口时计算表达式的值，然后查找跳转表，执行对应语句，否则default。类似二叉树。

需要注意的几点：
1. switch…case…只能用于case值为常量的分支结构，而if…else…更加灵活。
2. if判断条件为逻辑表达式，可以是布尔类型的合法表达式、可以是常量、枚举等。而switch 通常处理算术表达式，或字符。。
3. switch 进行一次条件判断后直接执行到程序的条件语句。而if…else 有几种条件，就得判断多少次。
4. 相比if语句，switch语句是以空间换时间的分支结构。因为它要生成跳转表，所以占用较多的代码空间。当case常量分布范围很大但实际有效值又比较少的情况，switch…case的空间利用率将变得很低。
5. 分支较多时，使用switch的效率高于if，除非第一个if条件就为真。。

最终，当分支较多时，使用if…else…代码可读性不如switch…case…高

<a id="markdown-循环" name="循环"></a>
### 循环
- do{}while()
- while(true){}
- for(){}

跳出循环：

* continue
* break
* return

<a id="markdown-数组" name="数组"></a>
## 数组
数组是一个存储相同类型元素的固定大小的顺序集合。数组是用来存储数据的集合，通常认为数组是一个同一类型变量的集合。
数组中某个指定的元素是通过索引来访问的。所有的数组都是由连续的内存位置组成的。最低的地址对应第一个元素，最高的地址对应最后一个元素。
这也是为什么在初始化数组的时候就需要指定数组的大小。

<a id="markdown-一维数组" name="一维数组"></a>
### 一维数组
在内存中是顺序连续存储的，所以它的索引速度非常快，赋值与修改元素也很简单。数组也是一个引用类型，初始化需要使用new关键字。

```cs
//使用字面值
int[] array = { 1, 2, 3, 4, 5 };
//使用new操作符
int[] array = new int[5];
//两者结合
int[] array = new int[] { 1, 2, 3, 4, 5 };
```

<a id="markdown-循环-1" name="循环-1"></a>
### 循环
```cs
//索引遍历（for循环）
for (int i = 0; i < array.Length; i++){..array[i]..}

/*
只读遍历
foreach 是自动迭代的，不需要事先获取数组的长度，但是只能读数据而不能写数据
只有实现了 IEnumerable 接口的类才能使用 foreach
*/
foreach (int data in array){..data..}
foreach (var data in array){..data..}

//示例
int[] array = { 1, 2, 3, 4, 5 };
for (int i = 0; i < array.Length; i++)
{
    Console.WriteLine(i);
}
foreach (int item in array)
{
    Console.WriteLine(item);
}
```

<a id="markdown-多维数组" name="多维数组"></a>
### 多维数组
多维数组最简单的形式是二维数组。一个二维数组，在本质上，是一个一维数组的列表。

一个二维数组可以被认为是一个带有 x 行和 y 列的表格。下面是一个二维数组，包含 3 行和 4 列：

![](../assets/Programming/two_dimensional_arrays.jpg)

规则数组，即每行的元素个数都是相同的
```cs
//int[,]这样声明的二维数组，需要通过相同索引器进行访问：array1[1,1]
int[,] array1 = { { 1, 2, 3, 4 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
int[,] array2 = new int[3, 4];
int[,] array3 = new int[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };

//循环遍历
foreach (int item in array1)
{
    Console.Write(item + "\t");
}
Console.WriteLine();
```

锯齿数组，即不规则数组
```cs
//锯齿数组，同样可以使用for和foreach进行遍历，通过索引器访问 array[1][1]
int[][] array = new int[3][] { new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7 }, new int[] { 8, 9 } };
int[][] array = new int[][] { new int[] { 1, 2 }, new int[] { 1, 2, 3 }, new int[] { } };

//循环遍历
foreach (int[] item in array)
{
    foreach (int v in item)
    {
        Console.Write(v + "\t");
    }
    Console.WriteLine();
}
```

<a id="markdown-方法" name="方法"></a>
## 方法
一个方法是把一些相关的语句组织在一起，用来执行一个任务的语句块。每一个 C# 程序至少有一个带有 Main 方法的类。

定义方法：
```cs
/*
<Access Specifier> 访问修饰符
<Return Type> 返回类型
<Method Name> 方法名
Parameter List 形参列表
*/
<Access Specifier> <Return Type> <Method Name>(Parameter List)
{
   Method Body
}
```

调用方法：`MethodName(<parameter>);`

<a id="markdown-形参实参" name="形参实参"></a>
### 形参实参
```cs
//方法的定义，返回类型int，方法名Add，形参列表：int a, int b
int Add(int a, int b)
{
    return a + b;
}

//方法的调用，传入实参 1和2
int sum = Add(1, 2);
```

<a id="markdown-重载" name="重载"></a>
### 重载
```cs
public int Calculate(int x, int y) {......}
public double Calculate(double x, double y) {......}
```
特点（两必须一可以）
1. 方法名必须相同
2. 参数列表必须不相同
3. 返回值类型可以不相同

<a id="markdown-参数传递" name="参数传递"></a>
### 参数传递
当调用带有参数的方法时，您需要向方法传递参数。在 C# 中，有三种向方法传递参数的方式：

| 方式     | 描述                                                                                                                                                             |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 值参数   | 这种方式复制参数的实际值给函数的形式参数，实参和形参使用的是两个不同内存中的值。在这种情况下，当形参的值发生改变时，不会影响实参的值，从而保证了实参数据的安全。 |
| 引用参数 | 这种方式复制参数的内存位置的引用给形式参数。这意味着，当形参的值发生改变时，同时也改变实参的值。                                                                 |
| 输出参数 | 这种方式可以返回多个值。                                                                                                                                         |

<a id="markdown-值传递" name="值传递"></a>
#### 值传递
值参数，实际在传递时发生了拷贝，即方法调用时实际改变你的是变量的副本。如下例所示：
```cs
static void Swap(int x, int y)
{
    int temp = x;
    x = y;
    y = temp;
}

static void Main(string[] args)
{
    int a = 1;
    int b = 2;
    Swap(a, b);
    Console.WriteLine("交换后：{0}\t{1}", a, b);
}
```

<a id="markdown-引用传递" name="引用传递"></a>
#### 引用传递
引用参数是一个对变量的内存位置的引用。当按引用传递参数时，与值参数不同的是，它不会为这些参数创建一个新的存储位置。

引用参数表示与提供给方法的实际参数具有相同的内存位置。

下面案例中的数组`arr1`是一个引用类型，在参数传递时并未发生拷贝，传递的是地址，所以在方法内的改变也会影响到对象本身。

```cs
static void Main(string[] args)
{
    int[] arr1 = { 1, 2, 3 };
    Change(arr1);
    foreach (int item in arr1)
    {
        Console.WriteLine(item);
    }
}

static void Change(int[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = 666;
    }
}
```

针对上例中的交换方法Swap难道没有办法进行引用传递吗？即传递是变量本身，而非值的副本。

当然，我们可以使用ref关键字来声明参数是引用传递，需要特别注意方法声明和调用处都需要添加ref关键字，ref是reference的简写。如下示例：
```cs
static void Swap(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}

static void Main(string[] args)
{
    int a = 1;
    int b = 2;
    Swap(ref a, ref b);
    Console.WriteLine("交换后：{0}\t{1}", a, b);
}
```

<a id="markdown-输出参数" name="输出参数"></a>
#### 输出参数
return 语句可用于只从函数中返回一个值。但是，可以使用 输出参数 来从函数中返回两个值。
输出参数会把方法输出的数据赋给自己，其他方面与引用参数相似。
```cs
static void AddValue(out int val)
{
    //模拟返回值，通过out关键字，可以返回多个值
    val = 5;
}

static void Main(string[] args)
{
    int res;
    AddValue(out res);
}
```

综合前面说到的ref，和out都会将传参强制传递引用，区别如下：
1. ref传参时，传入的参数必须是初始化之后的数据；而out，必须在方法中对其完成初始化；
2. 使用out可以返回一个值

<a id="markdown-params" name="params"></a>
#### params
有时，当声明一个方法时，您不能确定要传递给函数作为参数的参数数目。
在使用数组作为形参时，C# 提供了 params 关键字，使调用数组为形参的方法时，既可以传递数组实参，也可以传递一组数组元素。

params 的使用格式为：`public 返回类型 方法名称( params 类型名称[] 数组名称 )`
```cs
static int GetSum(params int[] arr)
{
    int sum = 0;
    foreach (int i in arr)
    {
        sum += i;
    }
    return sum;
}

static void Main(string[] args)
{
    int sum = GetSum(1, 2, 3, 4, 5);
    Console.WriteLine("总和是： {0}", sum);
}
```
需要注意的是：
1. 带 params 关键字的参数类型必须是一维数组，不能使用在多维数组上；
2. 不允许和 ref、out 同时使用；
3. 带 params 关键字的参数必须是最后一个参数，并且在方法声明中只允许一个 params 关键字。
4. 不能仅使用 params 来使用重载方法。
5. 没有 params 关键字的方法的优先级高于带有params关键字的方法的优先级

---

参考引用：

[类型和变量](https://docs.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/types-and-variables)

