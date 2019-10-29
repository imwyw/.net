# C#编码规范

## 概述

### 规范制定原则

* 方便代码的交流和维护。
* 不影响编码的效率，不与大众习惯冲突。
* 使代码更美观、阅读更方便。
* 使代码的逻辑更清晰、更易于理解。

### 术语定义

**Pascal 大小写**

将标识符的首字母和后面连接的每个单词的首字母都大写。可以对三字符或更多字符的标识符使用Pascal 大小写。例：

`BackColor`

**Camel 大小写**

标识符的首字母小写，而每个后面连接的单词的首字母都大写。例：

`backColor`

**匈牙利命名法**

匈牙利命名法是一名匈牙利程序员发明的，而且他在微软工作了多年。此命名法就是通过微软的各种产品和文档传出来的。多数有经验的程序员，不管他们用的是哪门儿语言，都或多或少在使用它。

这种命名法的基本原则是：

> 变量名＝属性＋类型＋对象描述

即一个变量名是由三部分信息组成，这样，程序员很容易理解变量的类型、用途，而且便于记忆。

下边是一些推荐使用的规则例子，你可以挑选使用，也可以根据个人喜好作些修改再用之。

 - 属性部分：
  - 全局变量： g_
  - 常量 ： c_
  - 类成员变量： m_

 - 类型部分：
  - 指针： p
  - 句柄： h
  - 布尔型： b
  - 浮点型： f
  - 无符号： u

 - 描述部分：
  - 初始化： Init
  - 临时变量： Tmp
  - 目的对象： Dst
  - 源对象： Src
  - 窗口： Wnd

下边举例说明：

- hwnd： h表示句柄，wnd表示窗口，合起来为“窗口句柄”。
- m_bFlag： m表示成员变量，b表示布尔，合起来为：“某个类的成员变量，布尔型，是一个状态标志”。

## 代码外观

### 列宽

代码列宽控制在120字符左右。

### 换行

当表达式超出或即将超出规定的列宽，遵循以下规则进行换行

- 在逗号后换行。
- 在操作符前换行。
- 规则1优先于规则2。

当以上规则会导致代码混乱的时候自己采取更灵活的换行规则。

### 缩进 

缩进应该是每行一个Tab(4个空格)，不要在代码中使用Tab字符。

Visual Studio 设置：工具->选项->文本编辑器->C#->制表符->插入空格，制表符大小=4，缩进大小=4

### 空行

空行是为了将逻辑上相关联的代码分块，以便提高代码的可阅读性。

**在以下情况下使用两个空行**

- 接口和类的定义之间。
- 枚举和类的定义之间。
- 类与类的定义之间。

**在以下情况下使用一个空行**

- 方法与方法、属性与属性之间。
- 方法中变量声明与语句之间。
- 方法与方法之间。
- 方法中不同的逻辑块之间。
- 方法中的返回语句与其他的语句之间。
- 属性与方法、属性与字段、方法与字段之间。
- 注释与它注释的语句间不空行，但与其他的语句间空一行。

### 空格

**在以下情况中要使用到空格**

- 关键字和左括符 “(” 应该用空格隔开。如：

`while (true)`

注意在方法名和左括符 “(” 之间不要使用空格，这样有助于辨认代码中的方法调用与关键字。

- 多个参数用逗号隔开，每个逗号后都应加一个空格。
- 除了 . 之外，所有的二元操作符都应用空格与它们的操作数隔开。一元操作符、++及--与操作数间不需要空格。如：

```cs
a += c + d;
a = (a + b)/(c*d);

while (d++ == s++)
{
    n++;
}

PrintSize("size is " + size + "\n");
```

- 语句中的表达式之间用空格隔开。如：

` for (expr1; expr2; expr3) `

### 括号 - ()

- 左括号“(”不要紧靠关键字，中间用一个空格隔开。
- 左括号“(”与方法名之间不要添加任何空格。
- 没有必要的话不要在返回语句中使用()。

### 花括号 - {}

- 左花括号 “{” 放于关键字或方法名的下一行并与之对齐。如：

```cs
if (condition)
{
}

public int Add(int x, int y)
{
}
```

- 左花括号 “{” 要与相应的右花括号 “}”对齐。
- 通常情况下左花括号 “{”单独成行，不与任何语句并列一行。
- if、while、do语句后一定要使用{}，即使{}号中为空或只有一条语句。如：

```cs
if (somevalue == 1)
{
    somevalue = 2;
}
```

- 右花括号 “}” 后建议加一个注释以便于方便的找到与之相应的 {。如:

```cs
while(1)
{
    if(valid)
    {
    } // if valid
    else
    {
    } // not valid
} // end forever
```

## 程序注释

### 注释概述

- 修改代码时，总是使代码周围的注释保持最新。
- 在每个例程的开始，提供标准的注释样本以指示例程的用途、假设和限制很有帮助。注释样本应该是解释它为什么存在和可以做什么的简短介绍.
- 避免在代码行的末尾添加注释；行尾注释使代码更难阅读。不过在批注变量声明时，行尾注释是合适的；在这种情况下，将所有行尾注释在公共制表位处对齐。 
- 避免杂乱的注释，如一整行星号。而是应该使用空白将注释同代码分开。 
- 避免在块注释的周围加上印刷框。这样看起来可能很漂亮，但是难于维护。
- 在部署发布之前，移除所有临时或无关的注释，以避免在日后的维护工作中产生混乱。
- 如果需要用注释来解释复杂的代码节，请检查此代码以确定是否应该重写它。尽一切可能不注释难以理解的代码，而应该重写它。尽管一般不应该为了使代码更简单以便于人们使用而牺牲性能，但必须保持性能和可维护性之间的平衡。
- 在编写代码时就注释，因为以后很可能没有时间这样做。另外，如果有机会复查已编写的代码，在今天看来很明显的东西六周以后或许就不明显了。
- 避免多余的或不适当的注释，不应包含个人情绪内容，如幽默的不主要的备注。
- 在编写注释时使用完整的句子。注释应该阐明代码，而不应该增加多义性。 
- 使用注释来解释代码的意图。它们不应作为代码的联机翻译。 
- 注释代码中不十分明显的内容。
- 为了防止问题反复出现，对错误修复和解决方法代码总是使用注释。
- 对由循环和逻辑分支组成的代码使用注释。这些是帮助源代码读者的主要方面。 
- 在整个应用程序中，使用具有一致的标点和结构的统一样式来构造注释。 
- 用空白将注释同注释分隔符分开。在没有颜色提示的情况下查看注释时，这样做会使注释很明显且容易被找到。
- 代码修改变更记录不应使用注释标明修改日期和修改人，注释应只针对代码不记录版本，代码版本应该使用代码版本系统进行管理
- 为了使层次清晰，在闭合的右花括号后注释该闭合所对应的起点。

```cs
namespace SCB.Framework.Web 
{
} // namespace SCB.Framework.Web
```

### 文档型注释

该类注释采用.Net已定义好的Xml标签来标记，在声明接口、类、方法、属性、字段都应该使用该类注释，以便代码完成后直接生成代码文档，让别人更好的了解代码的实现和接口。如

```cs
/// <summary> MyMethod is a method in the MyClass class.
/// <para> Here's how you could make a second paragraph in a description.
/// <see cref="System.Console.WriteLine"/> 
/// for information about output statements.
/// </para>
/// <seealso cref="MyClass.Main"/>
/// </summary>
public static void MyMethod(int Int1)
{
}
```

注释标签的使用请参考:[http://msdn.microsoft.com/zh-cn/library/5ast78ax.aspx](http://msdn.microsoft.com/zh-cn/library/5ast78ax.aspx "Microsoft Recommended Tags for Documentation Comments (C# Programming Guide)")

### 类c注释

该类注释用于

- 复杂程序逻辑说明与技术事项

**用法**

```cs
/*
动态路由算法使用Round-robin算法，原理是...
*/
```

### 单行注释

该类注释用于

- 方法内的代码注释。如变量的声明、代码或代码段的解释。例：

```cs
//
// 注释语句
// 
private int number;

或

// 注释语句
private int number;
```
 
- 方法内变量的声明或花括号后的注释, 例：

```cs
if (1 == 1) // always true
{
    statement; 
} // always true
```

## 声明

### 每行声明数

一行只建议作一个声明，并按字母顺序排列。如：

```cs
int level; // 推荐
int size; // 推荐
int x, y; // 不推荐
```

### 初始化

建议在变量声明时就对其做初始化。

### 位置

变量建议置于块的开始处，不要总是在第一次使用它们的地方做声明。如：

```cs
void MyMethod()
{
    int int1 = 0;

    if (condition)
    {
        int int2 = 0;
        ...
    }
}
```

**例外情况**

```cs
for (int i = 0; i < maxLoops; i++)
{
    ...
}
```

**应避免不同层次间的变量重名**，如：

```cs
int count;
...

void MyMethod()
{
    if (condition)
    {
        int count = 0; // 避免
        ...
    }
    ...
}
```

### 类和接口的声明

- 在方法名与其后的左括号间没有任何空格。
- 左花括号 “{” 出现在声明的下行并与之对齐，单独成行。
- 方法间用一个空行隔开。

### 字段的声明

不要使用是 public 或 protected 的实例字段。如果避免将字段直接公开给开发人员，可以更轻松地对类进行版本控制，原因是在维护二进制兼容性时字段不能被更改为属性。考虑为字段提供 get 和set 属性访问器，而不是使它们成为公共的。 get 和 set 属性访问器中可执行代码的存在使得可以进行后续改进，如在使用属性或者得到属性更改通知时根据需要创建对象。下面的代码示例阐释带有 get 和 set 属性访问器的私有实例字段的正确使用。例：

```cs
public class Control: Component
{
    private int handle;

    public int Handle
    {
        get
        {
            return handle; 
        }
    }
}
```

## 命名规范

### 命名概述

> 名称应该说明“什么”而不是“如何”。通过避免使用公开基础实现（它们会发生改变）的名称，可以保留简化复杂性的抽象层。例如，可以使用 `GetNextStudent()`，而不是 `GetNextArrayElement`()。 

**命名原则是：**

> 选择正确名称时的困难可能表明需要进一步分析或定义项的目的。使名称足够长以便有一定的意义，并且足够短以避免冗长。唯一名称在编程上仅用于将各项区分开。表现力强的名称是为了帮助人们阅读；因此，提供人们可以理解的名称是有意义的。不过，请确保选择的名称符合适用语言的规则和标准。

**以下几点是推荐的命名方法：**

- 避免容易被主观解释的难懂的名称，如方面名 `AnalyzeThis()`，或者属性名 `xxK8`。这样的名称会导致多义性。
- 在类属性的名称中包含类名是多余的，如 `Book.BookTitle`。而是应该使用 `Book.Title`。
- 只要合适，在变量名的末尾或开头加计算限定符`（Avg、Sum、Min、Max、Index）`。
- 在变量名中使用互补对，如 `min/max、begin/end` 和 `open/close`。 
- 布尔变量名应该包含 `Is`，这意味着 `Yes/No` 或 `True/False` 值，如 `fileIsFound`。
- 在命名状态变量时，避免使用诸如 `Flag` 的术语。状态变量不同于布尔变量的地方是它可以具有两个以上的可能值。不是使用 `documentFlag`，而是使用更具描述性的名称，如 `documentFormatType`。
- 即使对于可能仅出现在几个代码行中的生存期很短的变量，仍然使用有意义的名称。仅对于短循环索引使用单字母变量名，如 i 或 j。 可能的情况下，尽量不要使用原义数字（幻数）或原义字符串，如
`for (int i = 1; i < 7; i++)`。而是使用命名常数，如 `for (int i = 1; i < NUM_DAYS_IN_WEEK; i++)` 以便于维护和理解。

### 大小写规则

**大写**

- 组织名缩写使用大写
- 两个或者更少字母组成的标识符使用大写。例：

```cs
System.IO
System.Web.UI
SCB.Framework.UI
```

下表汇总了大写规则，并提供了不同类型的标识符的示例。

<table>
    <thead>
        <tr>
            <th>标识符</th>
            <th>大小写</th>
            <th>示例</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>类</td>
            <td>Pascal</td>
            <td>AppDomain</td>
        </tr>
        <tr>
            <td>枚举类型</td>
            <td>Pascal</td>
            <td>ErrorLevel</td>
        </tr>
        <tr>
            <td>枚举值</td>
            <td>Pascal</td>
            <td>FatalError</td>
        </tr>
        <tr>
            <td>事件</td>
            <td>Pascal</td>
            <td>ValueChange</td>
        </tr>
        <tr>
            <td>异常类</td>
            <td>Pascal</td>
            <td>WebException <br/>注意: 总是以 Exception 后缀结尾。</td>
        </tr>
        <tr>
            <td>只读的静态字段</td>
            <td>Pascal</td>
            <td>RedValue</td>
        </tr>
        <tr>
            <td>接口</td>
            <td>Pascal</td>
            <td>IDisposable <br/>注意: 总是以 I 前缀开始。</td>
        </tr>
        <tr>
            <td>方法</td>
            <td>Pascal</td>
            <td>ToString</td>
        </tr>
        <tr>
            <td>命名空间</td>
            <td>Pascal</td>
            <td>System.Drawing</td>
        </tr>
        <tr>
            <td>属性</td>
            <td>Pascal</td>
            <td>BackColor</td>
        </tr>
        <tr>
            <td>公共实例字段</td>
            <td>Pascal</td>
            <td>RedValue <br/>注意: 应优先使用属性。</td>
        </tr>
        <tr>
            <td>受保护的实例字段</td>
            <td>Camel</td>
            <td>redValue <br/>注意: 应优先使用属性。</td>
        </tr>
        <tr>
            <td>私有的实例字段</td>
            <td>Camel</td>
            <td>redValue</td>
        </tr>
        <tr>
            <td>参数</td>
            <td>Camel</td>
            <td>typeName</td>
        </tr>
        <tr>
            <td>方法内的变量</td>
            <td>Camel</td>
            <td>backColor</td>
        </tr>
    </tbody>
</table>

### 缩写

为了避免混淆和保证跨语言交互操作，请遵循有关区缩写的使用的下列规则： 

- 不要将缩写或缩略形式用作标识符名称的组成部分。例如，使用 `GetWindow`，而不要使用 `GetWin`。 
- 不要使用计算机领域中未被普遍接受的缩写。 
- 在适当的时候，使用众所周知的缩写替换冗长的词组名称。例如，用 `UI` 作为 `User Interface` 缩
写，用 `OLAP` 作为 `On-line Analytical Processing` 的缩写。 
- 在使用缩写时，对于超过两个字符长度的缩写请使用 Pascal 大小写或 Camel 大小写。例如使用 `HtmlButton` 或 `HTMLButton`；但是，应当大写仅有两个字符的缩写，如：`System.IO`，而不是 `System.Io`。
- 不要在标识符或参数名称中使用缩写。如果必须使用缩写，对于由多于两个字符所组成的缩写请使用Camel 大小写。

### 命名空间

- 命名命名空间时的一般性规则是使用公司名称，后跟技术名称和可选的功能与设计，如：

`CompanyName.TechnologyName[.Feature][.Design]`

例如：

```cs
namespace SCB.SupplierChain  // 赛酷比公司的供应链系统
namespace SCB.SupplierChain.DataRules // 赛酷比公司的供应链系统的业务规则模块
```

- 命名空间使用Pascal大小写。
- `TechnologyName` 指的是该项目的英文缩写或软件名。
- 命名空间和类不能使用同样的名字。例如，有一个类被命名为`Debug`后，就不要再使用`Debug`作为一个名称空间名。
 
### 类

- 使用 Pascal 大小写。
- 用名词或名词短语命名类。
- 使用全称避免缩写，除非缩写已是一种公认的约定，如`URL`、`HTML`
- 不要使用类型前缀，如在类名称上对类使用 C 前缀。例如，使用类名称 `FileStream`，而不是 
`CFileStream`。
- 不要使用下划线字符 (_)。
- 有时候需要提供以字母 I 开始的类名称，虽然该类不是接口。只要 I 是作为类名称组成部分的整个单词的第一个字母，这便是适当的。例如，类名称 `IdentityStore` 是适当的。在适当的地方，使用复合单词命名派生的类。派生类名称的第二个部分应当是基类的名称。例如，`ApplicationException` 对于从名为 `Exception` 的类派生的类是适当的名称，原因`ApplicationException` 是一种`Exception`。请在应用该规则时进行合理的判断。例如，`Button` 对于从 `Control` 派生的类是适当的名称。尽管按钮是一种控件，但是将 `Control` 作为类名称的一部分将使名称不必要地加长。

```cs
public class FileStream
public class Button
public class String
```

### 接口

- 用名词或名词短语，或者描述行为的形容词命名接口。例：
  - 接口名称 `IComponent` 使用描述性名词
  - 接口名称 `ICustomAttributeProvider` 使用名词短语
  - 接口名称 `IPersistable` 使用形容词。
- 使用 Pascal 大小写。
- 少用缩写。
- 给接口名称加上字母 I 前缀，以指示该类型为接口。在定义类/接口对（其中类是接口的标准实现）时使用相似的名称。两个名称的区别应该只是接口名称上有字母 I 前缀。
- 不要使用下划线字符 (_)。

```cs
 public interface IServiceProvider
 public interface IFormatable
```

**以下代码示例阐释如何定义 IComponent 接口及其标准实现 Component 类。**

```cs
public interface IComponent
{
    // Implementation code goes here.
}

public class Component: IComponent 
{
    // Implementation code goes here.
}
```

### 属性类 (Attribute)

应该总是将后缀 Attribute 添加到自定义属性类。例：

```cs
public class ObsoleteAttribute
{
}
```

### 枚举 (Enum)

枚举 (Enum) 值类型从 Enum 类继承。 

- 对于 Enum 类型和值名称使用 Pascal 大小写。
- 少用缩写。
- 不要在 Enum 类型名称上使用 Enum 后缀。
- 对大多数 Enum 类型使用单数名称，但是对作为位域的 Enum 类型使用复数名称。
- 总是将 `FlagsAttribute` 添加到位域 Enum 类型。

### 参数
 
- 使用描述性参数名称。参数名称应当具有足够的描述性，以便参数的名称及其类型可用于在大多数情况下确定它的含义。
- 对参数名称使用 Camel 大小写。
- 使用描述参数的含义的名称，而不要使用描述参数的类型的名称。开发工具将提供有关参数的类型的有意义的信息。因此，通过描述意义，可以更好地使用参数的名称。
- 不要给参数名称加匈牙利语类型表示法的前缀，仅在适合使用它们的地方使用它们。
- 不要使用保留的参数。保留的参数是专用参数，如果需要，可以在未来的版本中公开它们。相反，如果在类库的未来版本中需要更多的数据，请为方法添加新的重载。

```cs
Type GetType(string typeName)
string Format(string format, object args)
```

### 方法
 
- 使用动词或动词短语命名方法。 
- 使用 Pascal 大小写。

```cs
RemoveAll()
GetCharArray()
Invoke()
```

### 属性 (property)

- 使用名词或名词短语命名属性。 
- 使用 Pascal 大小写。 
- 考虑用与属性的基础类型相同的名称创建属性。例如，如果声明名为 `Color` 的属性，则属
性的类型同样应该是 Color。 

```cs
public class SampleClass
{
    public Color BackColor 
    {
        // Code for Get and Set accessors goes here.
    }
}
```

以下代码示例阐释提供其名称与类型相同的属性。

```cs
public enum Color 
{
    // Insert code for Enum here.
}

public class Control
{
    public Color Color 
    { 
        get
        {
            // Insert code here.
        }

        set
        {
            // Insert code here.
        } 
    }
}
```

### 事件
 
- 对事件处理程序名称使用 `EventHandler` 后缀。 
- 指定两个名为 `sender` 和 `e` 的参数。`sender` 参数表示引发事件的对象。`sender` 参数始
终是 `object` 类型的，即使在可以使用更为特定的类型时也如此。与事件相关联的状态封装
在名为 `e` 的事件类的实例中。对 `e` 参数类型使用适当而特定的事件类。 
- 用 `EventArgs` 后缀命名事件参数类。 
- 考虑用动词命名事件。 
- 使用动名词（动词的“ing”形式）创建表示事件前的概念的事件名称，用过去式表示事
件后。例如，可以取消的 `Close` 事件应当具有 `Closing` 事件和 `Closed` 事件。不要使用
`BeforeXxx/AfterXxx` 命名模式。 
- 不要在类型的事件声明上使用前缀或者后缀。例如，使用 `Close`，而不要使用 `OnClose`。 
- 通常情况下，对于可以在派生类中重写的事件，应在类型上提供一个受保护的方法（称为 
`OnXxx`）。此方法只应具有事件参数 `e`，因为发送方总是类型的实例。 

```cs

public delegate void MouseEventHandler(object sender, MouseEventArgs e);

public class MouseEventArgs : EventArgs 
{
    int x;
    int y;

    public MouseEventArgs(int x, int y) 
    {
        this.x = x;
        this.y = y; 
    }

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }
}
```

### 常量 (const)
 
所有单词大写，多个单词之间用 "_" 隔开。 如：

`public const string PAGE_TITLE = "Welcome";`

### 字段
 
- private、protected 使用 Camel 大小写。
- public 使用 Pascal 大小写。
- 拼写出字段名称中使用的所有单词。仅在开发人员一般都能理解时使用缩写。

```cs
class SampleClass
{
    string url;
    string destinationUrl;
}
```

- 不要对字段名使用匈牙利语表示法。好的名称描述语义，而非类型。
- 不要对字段名或静态字段名应用前缀。具体说来，不要对字段名称应用前缀来区分静态和非静态字段。例如，应用 `g_` 或 `s_` 前缀是不正确的。
- 对预定义对象实例使用公共静态只读字段。如果存在对象的预定义实例，则将它们声明为
对象本身的公共静态只读字段。使用 Pascal 大小写，原因是字段是公共的。

```cs
public struct Color
{
    public static readonly Color Red = new Color(0x0000FF);

    public Color(int rgb)
    {
        // Insert code here.
    }

    public Color(byte r, byte g, byte b)
    {
        // Insert code here.
    }

    public byte RedValue 
    {
        get
        {
            return Color;
        }
    }
}
```

### 静态字段
 
- 使用名词、名词短语或者名词的缩写命名静态字段。
- 使用 Pascal 大小写。
- 建议尽可能使用静态属性而不是公共静态字段。

### 集合

集合是一组组合在一起的类似的类型化对象，如哈希表、查询、堆栈、字典和列表，集合的命名建议用复数。

### 措词

避免使用与常用的 .NET 框架命名空间重复的类名称。例如，不要将以下任何名称用作类名称：System、Collections、Forms 或 UI。有关 .NET 框架命名空间的列表，请参阅类库。

另外，避免使用与C#语言关键字冲突的标识符。

## 语句

### 每行一个语句

每行最多包含一个语句。如：

```cs
a++; // 推荐
b--; // 推荐
a++; b--; // 不推荐
```

### 复合语句

复合语句是指包含"父语句{子语句;子语句;}"的语句，使用复合语句应遵循以下几点

- 子语句要缩进。
- 左花括号“{” 在复合语句父语句的下一行并与之对齐，单独成行。
- 即使只有一条子语句要不要省略花括号“ {}”。 如：

```cs
while(d += s++)
{
    n++;
}
```

### return 语句

return语句中不使用括号，除非它能使返回值更加清晰。如：

```cs
return;
return myDisk.size();
return (size ? size : defaultSize);
```

### if、 if-else、if else-if 语句
 
if、 if-else、if else-if 语句使用格式

```cs
if (condition)
{
    statements;
}

if (condition)
{
    statements;
}
else
{
    statements;
}

if (condition)
{
    statements;
}
else if (condition)
{
    statements;
}
else
{
    statements;
}
```

### for、foreach 语句

**for 语句使用格式**

```cs
for (initialization; condition; update)
{
    statements;
}
```

**空的 for 语句**（所有的操作都在`initialization`、`condition` 或 `update`中实现）使用格式

`for (initialization; condition; update); // update user id`

**foreach 语句使用格式**

```cs
foreach (object obj in array)
{
    statements;
}
```

*注意*

- 在循环过程中不要修改循环计数器。
- 对每个空循环体给出确认性注释。

### while 语句

**while 语句使用格式**

```cs
while (condition)
{
    statements;
}
```

**空的 while 语句使用格式**

`while (condition);`
 
### do - while 语句

do - while 语句使用格式

```cs
do
{
    statements;
} while (condition);
```

### switch - case 语句

switch - case语句使用格式

```cs
switch (condition)
{
    case 1:
        statements;
        break;

    case 2:
        statements;
        break;

    default:
        statements;
        break;
}
```

*注意：*

- 语句switch中的每个case各占一行。
- 为所有switch语句提供default分支。 
- 所有的非空 case 语句必须用 break; 语句结束。

### try - catch 语句

try - catch语句使用格式

```cs
try
{
    statements;
}
catch (ExceptionClass e)
{
    statements;
}
finally
{
    statements;
}
```

### using 块语句

using 块语句使用格式

```cs
using (object)
{
    statements;
}
```

## 控件命名规则

### 命名方法

控件名简写+英文描述，英文描述首字母大写

### 主要控件名简写对照表

- 控件名 => 简写
- Label => lbl
- TextBox => txt
- Button => btn
- LinkButton => lnkbtn
- ImageButton => imgbtn
- DropDownList => ddl
- ListBox => lst
- DataGrid => dg
- DataList => dl
- CheckBox => chk
- CheckBoxList => chkls
- RadioButton => rdo
- RadioButtonList => rdolt
- Image => img
- Panel => pnl
- Calender => cld
- AdRotator => ar
- Table => tbl
- RequiredFieldValidator => rfv
- CompareValidator => cv
- RangeValidator => rv
- RegularExpressionValidator => rev
- ValidatorSummary => vs
- CrystalReportViewer => rptvew

## 其他

### 表达式

- 避免在表达式中用赋值语句
- 避免对浮点类型做等于或不等于判断

### 类型转换

- 尽量避免强制类型转换。
- 如果不得不做类型转换，尽量用显式方式。


--- 

引用自：

[C#编码规范](https://gist.github.com/zhuqling/a2700703d088b8746f0c)

---

另推荐阅读：

[C#命名规则和编码规范](https://www.jianshu.com/p/dc26cb8ffcb9)

[Internal Coding Guidelines](https://blogs.msdn.microsoft.com/brada/2005/01/26/internal-coding-guidelines/)

