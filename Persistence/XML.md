<!-- TOC -->

- [XML](#xml)
    - [XML标准](#xml标准)
        - [语法规则](#语法规则)
            - [声明](#声明)
            - [注释](#注释)
            - [所有元素都须有关闭标签](#所有元素都须有关闭标签)
            - [标签对大小写敏感](#标签对大小写敏感)
            - [必须正确地嵌套](#必须正确地嵌套)
            - [文档必须有根元素](#文档必须有根元素)
            - [属性值须加引号](#属性值须加引号)
            - [实体引用](#实体引用)
        - [格式](#格式)
            - [树结构](#树结构)
            - [属性和元素表示](#属性和元素表示)
    - [读写XML](#读写xml)
        - [基于流的分析XmlReader/XmlReaderSettings](#基于流的分析xmlreaderxmlreadersettings)
        - [基于流的创建XmlWriter/XmlWriterSettings](#基于流的创建xmlwriterxmlwritersettings)
        - [内存中的处理XmlDocument](#内存中的处理xmldocument)
            - [查询](#查询)
            - [修改](#修改)
            - [创建](#创建)
            - [删除](#删除)
        - [XmlDocument和XmlReader](#xmldocument和xmlreader)
        - [XDocument(方便、快捷)](#xdocument方便快捷)
    - [增加对配置文件操作示例](#增加对配置文件操作示例)

<!-- /TOC -->
<a id="markdown-xml" name="xml"></a>
# XML
<a id="markdown-xml标准" name="xml标准"></a>
## XML标准
可扩展标记语言（英语：Extensible Markup Language，简称：XML），是一种标记语言。

标记指计算机所能理解的信息符号，通过此种标记，计算机之间可以处理包含各种信息的文章等。

XML在.NET Framework中有重要作用。不仅允许在应用程序中使用XML，.NET Framework本身也在配置文件和源代码文档中使用XML。

对XML处理的支持由.NET中System.Xml名称空间的类提供。

它和超文本标记语言(HTML)语法区别：


超文本标记语言的标记不是所有的都需要成对出现，它则要求所有的标记必须成对出现；

HTML标记不区分大小写，它则大小敏感，即区分大小写。

**XML 被设计用来结构化、存储以及传输信息。**

<a id="markdown-语法规则" name="语法规则"></a>
### 语法规则

<a id="markdown-声明" name="声明"></a>
#### 声明
通过第一行xml的声明，定义xml版本和所使用的编码，如下：
```xml
<?xml version="1.0" encoding="utf-8"?>
```

<a id="markdown-注释" name="注释"></a>
#### 注释

同html的注释，如下：
```xml
<!-- 注释 --> 
```

<a id="markdown-所有元素都须有关闭标签" name="所有元素都须有关闭标签"></a>
#### 所有元素都须有关闭标签

省略关闭标签是非法的：
```xml
<!-- 错误的，p标签未关闭 -->
<p>This is a paragraph
<p>This is another paragraph

<!-- 合法的表示 -->
<p>This is a paragraph</p>
<p>This is another paragraph</p>  
```

<a id="markdown-标签对大小写敏感" name="标签对大小写敏感"></a>
#### 标签对大小写敏感

标签的大小写是严格限定的，标签 `<Letter>` 与标签 `<letter>` 是不同的。
```xml
<Message>这是错误的。</message>

<message>这是正确的。</message> 
```

<a id="markdown-必须正确地嵌套" name="必须正确地嵌套"></a>
#### 必须正确地嵌套
所有元素必须正确的按顺序关闭，如下：
```xml
<!-- 错误的表示，因为name在stu内部打开，关闭时应先关闭name再关闭stu标签 -->
<Students>
    <stu>
        <name></stu>
    </name>
</Students>

<!-- 正确的关闭 -->
<Students>
    <stu>
        <name></name>
    </stu>
</Students>
```

<a id="markdown-文档必须有根元素" name="文档必须有根元素"></a>
#### 文档必须有根元素

必须有一个元素是所有其他元素的父元素。该元素称为根元素。
```xml
<root>
  <child>
    <subchild>.....</subchild>
  </child>
</root>
```

<a id="markdown-属性值须加引号" name="属性值须加引号"></a>
#### 属性值须加引号
属性值是以key-value形式进行存储，属性值上需要加引号
```xml
<!-- 错误 -->
<stu name=zhangsan>

<!-- 规范 -->
<stu name="zhangsan">
```

<a id="markdown-实体引用" name="实体引用"></a>
#### 实体引用
如果，某元素内容包含特殊字符`<`，对于这样属性的解析会发生错误，

因为`<`是标签符号，会造成解析错误，我们通过实体定义可以解决这样的问题。

```xml
<!-- 解析失败 -->
<stu name="zhangsan"> 1<9 </stu>

<!-- &lt; 替换 < -->
<stu name="zhangsan">1 &lt; 9</stu>
```

实体定义 | 字符 | 说明
-----|----|---
`&lt;` | < | 小于
`&gt;` | > | 大于
`&amp;` | & | 与
`&apos;` | ' | 单引号
`&quot;` | " | 引号

_ps:在 XML 中，只有字符 "<" 和 "&" 确实是非法的。大于号是合法的，但是用实体引用来代替它是一个好习惯。_

<a id="markdown-格式" name="格式"></a>
### 格式
<a id="markdown-树结构" name="树结构"></a>
#### 树结构
![](..\assets\xml\ct_nodetree1.gif)

bookstore.xml基本格式如下：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<!-- bookstore 根节点 -->
<bookstore>
    <book category="COOKING">
        <title lang="en">Everyday Italian</title>
        <author>Giada De Laurentiis</author>
        <year>2005</year>
        <price>30.00</price>
    </book>
    <book category="CHILDREN">
        <title lang="en">Harry Potter</title>
        <author>J K. Rowling</author>
        <year>2005</year>
        <price>29.99</price>
    </book>
    <book category="WEB">
        <title lang="en">Learning XML</title>
        <author>Erik T. Ray</author>
        <year>2003</year>
        <price>39.95</price>
    </book>
</bookstore>
```

<a id="markdown-属性和元素表示" name="属性和元素表示"></a>
#### 属性和元素表示

```cs
//定义一个英雄类
public class Hero {
    public string Name { get; set; }
    public string Gender { get; set; }
}

//实例化了三个对象
Hero superMan = new Hero() { Name = "超人", Gender = "Male" }
Hero superWomen = new Hero() { Name = "女超人", Gender = "Female" }
Hero spiderMan = new Hero() { Name = "蜘蛛侠", Gender = "Male" }
```

针对上面CS代码中三个对象，使用属性（名称-值）方式表示如下：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<heros>
  <hero name="超人" gender="Male"/>
  <hero name="女超人" gender="Female"/>
  <hero name="蜘蛛侠" gender="Male"/>
</heros>
```

针对上面CS代码中三个对象，使用元素方式表示如下：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<heros>
  <hero>
    <name>超人</name>
    <gender>Male</gender>
  </hero>
  <hero>
    <name>女超人</name>
    <gender>Female</gender>
  </hero>
  <hero>
    <name>蜘蛛侠</name>
    <gender>Male</gender>
  </hero>
</heros>
```

<a id="markdown-读写xml" name="读写xml"></a>
## 读写XML

<a id="markdown-基于流的分析xmlreaderxmlreadersettings" name="基于流的分析xmlreaderxmlreadersettings"></a>
### 基于流的分析XmlReader/XmlReaderSettings

XmlReader 类是一个提供对 XML 数据的非缓存、只进只读访问的抽象基类，该类不可以实例化，使用XmlReader.Create()创建实例。

XmlReader 类支持从流或文件读取 XML 数据。

读取上面示例中bookstore.xml，读取内容（包含属性和文本）：
```xml
string xmlPath = @"E:\Attachment\xml\bookstore.xml";
using (XmlReader reader = XmlReader.Create(xmlPath))
{
    while (reader.Read())
    {
        switch (reader.NodeType)
        {
            //节点元素的处理
            case XmlNodeType.Element:
                Console.Write("标签开始： <{0}", reader.Name);
                //判断该元素是否有属性
                if (reader.HasAttributes)
                {
                    //移动到下一个属性,遍历属性
                    while (reader.MoveToNextAttribute())
                    {
                        Console.Write(" {0}=\"{1}\"", reader.Name, reader.Value);
                    }
                }
                Console.WriteLine(">");
                break;
            //节点的文本内容
            case XmlNodeType.Text:
                Console.WriteLine("元素内容：{0}", reader.Value);
                break;
            //关闭标签名称
            case XmlNodeType.EndElement:
                Console.WriteLine("元素关闭：</{0}>", reader.Name);
                break;
            //其他类型请自行研究
            default:
                Console.WriteLine("其他节点类型：{0}，该节点值为：{1}", reader.NodeType, reader.Value);
                break;
        }
    }
}
```

> [MSDN-XmlReader 类]https://msdn.microsoft.com/zh-cn/library/system.xml.xmlreader(v=vs.110).aspx


<a id="markdown-基于流的创建xmlwriterxmlwritersettings" name="基于流的创建xmlwriterxmlwritersettings"></a>
### 基于流的创建XmlWriter/XmlWriterSettings
XmlWriter 类是一个抽象基类，提供只进、只写、非缓存的方式来生成 XML 流。
使用静态方法XmlWrite.Create()创建实例。

在当前目录创建一个books.xml文件，创建和关闭标记一定要按照正确顺序，如下：
```cs
XmlWriterSettings settings = new XmlWriterSettings();
settings.Indent = true;
settings.IndentChars = "\t";
using (XmlWriter writer = XmlWriter.Create("books.xml", settings))
{
    //创建开始标记，根节点
    writer.WriteStartElement("bookstore");
    
    //创建book标记
    writer.WriteStartElement("book");
    writer.WriteAttributeString("category", "COOKING");

    //创建title标记
    writer.WriteStartElement("title");
    writer.WriteString("category");
    writer.WriteEndElement();
    
    //关闭标记
    writer.WriteEndElement();
    //关闭标记
    writer.WriteEndElement();

    //将XML文档写入磁盘（冲刷缓冲区）
    writer.Flush();
}
```
得到books.xml内容如下，在上述代码中有很多重复的创建标记，代码很冗余，我们可以重构提取一个方法出来
```xml
<?xml version="1.0" encoding="utf-8"?>
<bookstore>
	<book category="COOKING">
		<title>category</title>
	</book>
</bookstore>
```

重构提取方法，简单对页面节点做了个提取，方便了叶子标记的创建。
```cs
static void Main()
{
    XmlWriterSettings settings = new XmlWriterSettings();
    //指定是否缩进元素，默认为false，默认缩进字符是两个空格
    settings.Indent = true;
    //指定缩进为制表符
    settings.IndentChars = "\t";
    using (XmlWriter writer = XmlWriter.Create("books.xml", settings))
    {
        //创建开始标记，根节点
        writer.WriteStartElement("bookstore");

        //创建book标记
        writer.WriteStartElement("book");
        writer.WriteAttributeString("category", "COOKING");
        WriteLeafNode(writer, "title", "Everyday Italian", new List<TagPropInfo>() { new TagPropInfo("lang", "en") });
        WriteLeafNode(writer, "author", "Giada De Laurentiis", null);
        WriteLeafNode(writer, "year", "2005", null);
        WriteLeafNode(writer, "price", "30.00", null);
        writer.WriteEndElement();

        //关闭标记
        writer.WriteEndElement();

        //将XML文档写入磁盘（冲刷缓冲区）
        writer.Flush();
    }
}

/// <summary>
/// 构造一个标记属性类，方便进行传递
/// </summary>
public class TagPropInfo
{
    public TagPropInfo(string name, string propValue)
    {
        Name = name;
        PropValue = propValue;
    }
    public string Name { get; set; }
    public string PropValue { get; set; }
}

/// <summary>
/// 创建叶子节点，简单封装
/// </summary>
/// <param name="xw">xmlwrite对象</param>
/// <param name="tagName">标记的名称</param>
/// <param name="text">元素的内容文本</param>
/// <param name="lstProps">元素属性集合</param>
static void WriteLeafNode(XmlWriter xw, string tagName, string text, List<TagPropInfo> lstProps)
{
    xw.WriteStartElement(tagName);
    if (lstProps != null && lstProps.Count > 0)
    {
        foreach (TagPropInfo item in lstProps)
        {
            xw.WriteAttributeString(item.Name, item.PropValue);
        }
    }
    if (!string.IsNullOrEmpty(text))
    {
        xw.WriteString(text);
    }
    xw.WriteEndElement();
}
```

<a id="markdown-内存中的处理xmldocument" name="内存中的处理xmldocument"></a>
### 内存中的处理XmlDocument
XML 文档对象模型 (DOM) 将 XML 数据作为一组标准的对象对待，用于处理内存中的 XML 数据。

常用的XML操作类如下：
* XmlNode：表示文档中的一个节点
* XmlDocument：扩张了XmlNode类，用于加载和保存磁盘或其他地方的数据
* XmlElement：表示Xml文档中的一个元素
* XmlAttribute：表示一个属性
* XmlText：表示开标记和闭标记之间的文本
* XmlComment：表示一种特殊类型的节点，为读者提供各部分的信息
* XmlNodeList：表示节点的集合

将xml文档加载到内存中：
```cs
XmlDocument doc = new XmlDocument();
doc.Load(xmlPath);
```

常用的XmlElement类包含的属性和方法：
* FirstChild：返回根节点后第一个节点
* LastChild：返回当前节点的最后一个子节点
* ParentNode：返回当前节点的父节点
* NextSibling：返回紧跟着的兄弟节点
* HasChildNodes：判断当前节点是否有子节点

<a id="markdown-查询" name="查询"></a>
#### 查询
使用DOM方式进行遍历节点：
```cs
XmlDocument doc = new XmlDocument();
doc.Load(@"E:\Attachment\xml\bookstore.xml");

//以文本方式打印所有内容
Console.WriteLine(doc.OuterXml);

//可以按照路径寻找节点，从根节点开始寻找，遍历所有的book节点
XmlNodeList nodeList = doc.SelectNodes("/bookstore/book");
foreach (XmlNode node in nodeList)
{
    /*
    node.InnerText 读取节点内容文本
    node.Attributes["category"].Value 节点对应的category属性的值
    省略了很多判断，需要注意捕获异常
    */
    string cate = node.Attributes["category"].Value;
    string title = node.SelectSingleNode("title").InnerText;
    string year = node.SelectSingleNode("year").InnerText;
    string price = node.SelectSingleNode("price").InnerText;

    Console.WriteLine("category:{0},title:{1},year:{2},price:{3}", cate, title, year, price);
}
```

<a id="markdown-修改" name="修改"></a>
#### 修改
使用DOM方式进行节点的修改：
```cs
string xmlPath = @"E:\Attachment\xml\bookstore.xml";
XmlDocument doc = new XmlDocument();
doc.Load(xmlPath);

//取第一个匹配的book标记
XmlNode node = doc.SelectSingleNode("/bookstore/book");
//修改book标记的属性
node.Attributes["category"].Value = "ART";

//修改后一定要保存
doc.Save(xmlPath);
```

<a id="markdown-创建" name="创建"></a>
#### 创建
同样，我们以前面所说的bookstore为例，使用XmlDocument添加文档：
```cs
XmlDocument doc = new XmlDocument();

//创建Xml声明部分，并添加到文档，即<?xml version="1.0" encoding="utf-8" ?>
XmlDeclaration xmlDecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
doc.AppendChild(xmlDecl);

//创建根元素，并添加到文档
XmlNode rootNode = doc.CreateElement("bookstore");
doc.AppendChild(rootNode);

//创建 book 元素，添加到 根元素 
XmlNode book = doc.CreateElement("book");
XmlAttribute attr = doc.CreateAttribute("category");
attr.Value = "COOKING";
book.Attributes.Append(attr);
rootNode.AppendChild(book);

//创建 title 元素，添加到 book 元素
XmlNode title = doc.CreateElement("title");
attr = doc.CreateAttribute("lang");
attr.Value = "en";
title.Attributes.Append(attr);
title.InnerText = "Everyday Italian";
book.AppendChild(title);

//创建 author 元素，添加到book元素
XmlNode author = doc.CreateElement("author");
author.InnerText = "Giada De Laurentiis";
book.AppendChild(author);

//创建 year 元素，添加到book元素
XmlNode year = doc.CreateElement("year");
year.InnerText = "2005";
book.AppendChild(year);

//创建 price 元素，添加到book元素
XmlNode price = doc.CreateElement("price");
price.InnerText = "30.00";
book.AppendChild(price);

//最后进行保存
doc.Save(@".\booksNew.xml");
```

<a id="markdown-删除" name="删除"></a>
#### 删除
使用XmlDocument进行修改、删除xml节点，以上面bookstore为例，删除其中category为WEB的元素
```cs
string xmlPath = @"..\..\bookstore.xml";
XmlDocument doc = new XmlDocument();
doc.Load(xmlPath);

//所有的book元素
XmlNodeList nodeList = doc.SelectNodes("/bookstore/book");

//book的父元素，删除category="WEB"的book元素需要使用父级元素
XmlNode bookstore = doc.SelectSingleNode("/bookstore");
foreach (XmlNode node in nodeList)
{
    if (node.Attributes["category"].Value == "WEB")
    {
        //从父级元素的角度进行删除该book元素及其子元素
        bookstore.RemoveChild(node);
    }
}
doc.Save(xmlPath);
```

<a id="markdown-xmldocument和xmlreader" name="xmldocument和xmlreader"></a>
### XmlDocument和XmlReader
- Dom模型

使用DOM的好处在于它允许编辑和更新XML文档，可以随机访问文档中的数据，可以使用XPath查询。

但是，DOM的缺点在于它需要一次性的加载整个文档到内存中，对于大型的文档，这会造成资源问题。 

- 流模型

流模型很好的解决了这个问题，因为它对XML文件的访问采用的是流的概念，

也就是说，任何时候在内存中只有当前节点，但它也有它的不足，它是只读的，仅向前的，不能在文档中执行向后导航操作。

<a id="markdown-xdocument方便快捷" name="xdocument方便快捷"></a>
### XDocument(方便、快捷)
LINQ to XML API

和XmlDocument相同的是会把所有的XML所有的节点加载到内存中。

使用XDocument进行创建xml文件，和上面XmlDocument创建xml文档一致，但是代码要精简很多，如下所示：
```cs
string xmlPath = @"..\..\bookstore.xml";

//XDocument 需要 using System.Xml.Linq;
XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", null));

XElement root = new XElement("bookstore");
xdoc.Add(root);

/*
new XElement("标记名称","元素内容",new XAttribute(),子元素)
*/
XElement book1 = new XElement("book", new XAttribute("category", "COOKING")
    , new XElement("title", "Everyday Italian", new XAttribute("lang", "en"))
    , new XElement("author", "Giada De Laurentiis")
    , new XElement("year", "2005")
    , new XElement("price", "30.00"));
//记得要进行保存
root.Add(book1);

XElement book2 = new XElement("book", new XAttribute("category", "CHILDREN")
    , new XElement("title", "Harry Potter", new XAttribute("lang", "en"))
    , new XElement("author", "J K. Rowling")
    , new XElement("year", "2005")
    , new XElement("price", "29.99"));
//记得要进行保存
root.Add(book2);

xdoc.Save(xmlPath);
```

<a id="markdown-增加对配置文件操作示例" name="增加对配置文件操作示例"></a>
## 增加对配置文件操作示例
//todo...

拓展参考：

[XML技术总结之XDocument 和XmlDocument](http://www.cnblogs.com/HQFZ/p/4788428.html)

