<!-- TOC -->

- [编码规范](#编码规范)
    - [为什么要有规范](#为什么要有规范)
    - [术语介绍](#术语介绍)
        - [Pascal](#pascal)
        - [Camel](#camel)
        - [匈牙利](#匈牙利)
    - [命名规范](#命名规范)
        - [命名的原则](#命名的原则)
        - [大小写规则](#大小写规则)
        - [类与方法](#类与方法)
        - [布尔型相关规范](#布尔型相关规范)

<!-- /TOC -->

<a id="markdown-编码规范" name="编码规范"></a>
# 编码规范

<a id="markdown-为什么要有规范" name="为什么要有规范"></a>
## 为什么要有规范
* 方便代码的交流和维护。
* 不影响编码的效率，不与大众习惯冲突。
* 使代码更美观、阅读更方便。
* 使代码的逻辑更清晰、更易于理解。

<a id="markdown-术语介绍" name="术语介绍"></a>
## 术语介绍

<a id="markdown-pascal" name="pascal"></a>
### Pascal

Pascal规则是指名称中单词的首字母大写 ,如EmployeeSalary、 ConfimationDialog、PlainTextEncoding。

用Pascal规则来命名属性、方法、事件和类名
```cs
public class HelloWorld
{
    public void SayHello(string name)
    {
    }
}
```
<a id="markdown-camel" name="camel"></a>
### Camel

Camel规则类似于Pascal规则 ,但名称中第一个单词的首字母不大写 ,如employeeSalary、 confimationDialog、plainTextEncoding。

<a id="markdown-匈牙利" name="匈牙利"></a>
### 匈牙利

匈牙利命名法是一名匈牙利程序员发明的，而且他在微软工作了多年。

此命名法就是通过微软的各种产品和文档传出来的。多数有经验的程序员，不管他们用的是哪门儿语言，都或多或少在使用它。

这种命名法的基本原则是：`变量名＝属性＋类型＋对象描述`

会造成名称和类型之间不必要的耦合，现已不再流行。

用Camel规则来命名成员变量、局部变量和方法的参数
```cs
public class Product
{
    private string productId;
    private string productName;
    
    public void AddProduct(string productId,string productName)
    {
    }
}
```

不要使用匈牙利命名法

不要给成员变量加任何前缀（如、m、s_等等）。如果想要区分局部变量和成员变量，可以使用this关键字。

<a id="markdown-命名规范" name="命名规范"></a>
## 命名规范

名称应该说明“什么”而不是“如何”。通过避免使用公开基础实现（它们会发生改变）的名称，可以保留简化复杂性的抽象层。

例如，可以使用 GetNextStudent()，而不是 GetNextArrayElement()。

<a id="markdown-命名的原则" name="命名的原则"></a>
### 命名的原则
* 避免容易被主观解释的难懂的名称，如方面名 AnalyzeThis()，或者属性名 xxK8。这样的名称会导致多义性。
* 在类属性的名称中包含类名是多余的，如 Book.BookTitle。而是应该使用 Book.Title。
* 只要合适，在变量名的末尾或开头加计算限定符（Avg、Sum、Min、Max、Index）。
* 在变量名中使用互补对，如 min/max、begin/end 和 open/close。
* 布尔变量名应该包含 Is，这意味着 Yes/No 或 True/False 值，如 fileIsFound。
* 在命名状态变量时，避免使用诸如 Flag 的术语。状态变量不同于布尔变量的地方是它可以具有两个以上的可能值。不是使用 documentFlag，而是使用更具描述性的名称，如 documentFormatType。
* 即使对于可能仅出现在几个代码行中的生存期很短的变量，仍然使用有意义的名称。仅对于短循环索引使用单字母变量名，如 i 或 j。 可能的情况下，尽量不要使用原义数字（幻数）或原义字符串，如 for (int i = 1; i < 7; i++)。而是使用命名常数，如 for (int i = 1; i < NUM_DAYS_IN_WEEK; i++) 以便于维护和理解。

<a id="markdown-大小写规则" name="大小写规则"></a>
### 大小写规则
标识符 | 大小写 | 示例
----|-----|---
类 | Pascal | AppDomain
枚举类型 | Pascal | ErrorLevel
枚举值 | Pascal | FatalError
事件 | Pascal | ValueChange
异常类 | Pascal | WebException  注意: 总是以 Exception 后缀结尾。
自定义特性  |  Pascal | CusAttribute  注意: 总是以 Attribute 后缀结尾。
只读的静态字段 | Pascal | RedValue
接口 | Pascal | IDisposable  注意: 总是以 I 前缀开始。
方法 | Pascal | ToString
命名空间 | Pascal | System.Drawing
属性 | Pascal | BackColor
公共实例字段 | Pascal | RedValue  注意: 应优先使用属性。
受保护的实例字段 | Camel | redValue  注意: 应优先使用属性。
私有的实例字段 | Camel | redValue
参数 | Camel | typeName
方法内的变量 | Camel | backColor

<a id="markdown-类与方法" name="类与方法"></a>
### 类与方法
类的命名。用名词或名词短语来命名类名。
```cs
 public class Employee
 {
 }
 public class BusinessLocation
 {
 }
 public class DocumentCollection
 {
 }
```

方法的命名。一般将其命名为动宾短语。
```cs
public class File
 {
     public void CreateFile(string filePath)
     {
     }
     public void GetPath(string path)
     {
     }
 }
```

特别注意：

**局部变量的名称要有意义。不要直接用用i,j,k,l,m,n,x,y,z等做变量名，但for循环除外。**

**如果一个方法超过25行，就需要考虑是否可以重构和拆分成多个方法。方法命名要见名知意，好的方法名可以省略多余的注释。方法功能尽量单一。**

<a id="markdown-布尔型相关规范" name="布尔型相关规范"></a>
### 布尔型相关规范
变量或者方法一般可以用is、can、has或者should做前缀。如，isFinished, canWork等。




