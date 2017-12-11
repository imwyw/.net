<!-- TOC -->

- [LINQ](#linq)
    - [初识](#初识)
        - [查询操作符](#查询操作符)
        - [查询表达式](#查询表达式)

<!-- /TOC -->

<a id="markdown-linq" name="linq"></a>
# LINQ

<a id="markdown-初识" name="初识"></a>
## 初识
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


参考引用：

[LINQ之路系列博客导航](http://www.cnblogs.com/lifepoem/archive/2011/12/16/2288017.html)