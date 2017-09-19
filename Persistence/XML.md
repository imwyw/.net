# XML
## XML标准
可扩展标记语言（英语：Extensible Markup Language，简称：XML），是一种标记语言。标记指计算机所能理解的信息符号，通过此种标记，计算机之间可以处理包含各种信息的文章等。

XML在.NET Framework中有重要作用。不仅允许在应用程序中使用XML，.NET Framework本身也在配置文件和源代码文档中使用XML。

对XML处理的支持由.NET中System.Xml名称空间的类提供。

它和超文本标记语言(HTML)语法区别：超文本标记语言的标记不是所有的都需要成对出现，它则要求所有的标记必须成对出现；HTML标记不区分大小写，它则大小敏感，即区分大小写。

### 特点
- 必须有一个根元素(有且只有一个)
- 元素必须有结束，可以简写，例如：<student/>
- 元素之间必须合理嵌套
- 标记对于大小写是敏感的

### 格式
基本格式如下：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<students>
  <student age="12" course="BigData">王富贵</student>
  <student age="18" course="AI">赵有才</student>
  <student age="22" course="EatSleep">李霸道</student>
  <student age="99" course="MakeMoney">钱不够</student>
</students>
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<heros>
  <hero name="超人" gender="Male"/>
  <hero name="女超人" gender="Female"/>
  <hero name="蜘蛛侠" gender="Male"/>
</heros>
```
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

## 读写XML
### XmlWriter
XmlWriter的用法，使用XmlWriter生成以下格式的student.xml文件：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<students>
  <student age="12" course="BigData">王富贵</student>
  <student age="18" course="AI">赵有才</student>
</students>
```

```cs
string fileName = @"E:\Attachment\students.xml";

XmlWriterSettings xmlSet = new XmlWriterSettings();

//指定是否缩进元素，默认为false，默认缩进字符是两个空格
xmlSet.Indent = true;

//设置分行符为换行
xmlSet.NewLineChars = System.Environment.NewLine;

using (XmlWriter xr = XmlWriter.Create(fileName, xmlSet))
{
    //写入根节点
    xr.WriteStartElement("students");
    //写入子节点
    xr.WriteStartElement("student");
    //给节点添加属性
    xr.WriteAttributeString("age", "12");
    xr.WriteAttributeString("course", "BigData");
    //给节点内部添加文本
    xr.WriteString("王富贵");
    //关闭当前节点
    xr.WriteEndElement();

    //写入子节点
    xr.WriteStartElement("student");
    //给节点添加属性
    xr.WriteAttributeString("age", "18");
    xr.WriteAttributeString("course", "AI");
    //给节点内部添加文本
    xr.WriteString("赵有才");
    //关闭当前节点
    xr.WriteEndElement();

    //将XML文档写入磁盘（冲刷缓冲区）
    xr.Flush();
}
```

### XmlReader
读取一点，显示一点，并不是全部加载内存中才能进行显示

针对XmlWriter部分中的student.xml进行读取，示例如下：
```cs
string fileName = @"E:\Attachment\students.xml";

using (XmlReader reader = XmlReader.Create(fileName))
{
    //循环read方法直到文档结束
    while (reader.Read())
    {
        switch (reader.NodeType)
        {
            //节点元素的处理
            case XmlNodeType.Element:
                Console.Write("<{0}", reader.Name);
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        Console.Write(" {0}=\"{1}\"", reader.Name, reader.Value);
                    }
                }
                Console.WriteLine(">");
                break;
            //节点的文本内容
            case XmlNodeType.Text:
                Console.WriteLine(reader.Value);
                break;
            //其他类型请自行研究
            default:
                break;
        }
    }
}
```


### XmlDocument
文档对象模型XmlDocument，适用于并不是很大的文档。因为需要全部加载到内存中。

使用XmlDocument上述示例中的student.xml文件中节点进行遍历节点，也是最常用的一种用法：
```cs
XmlDocument xdoc = new XmlDocument();
xdoc.Load(fileName);

//按照路径获取所有students下student节点
XmlNodeList nodeList = xdoc.SelectNodes("/students/student");

foreach (XmlNode nd in nodeList)
{
    /*
    nd.InnerText 读取节点内容文本
    nd.Attributes["age"].Value 节点对应的age属性的值
    nd.Attributes["course"].Value 节点对应的course属性的值
    省略了很多判断，需要注意捕获异常
    */
    Console.WriteLine("姓名：{0}，年龄：{1}，专业：{2}"
        , nd.InnerText.Trim(), nd.Attributes["age"].Value, nd.Attributes["course"].Value);
}
```

使用XmlDocument添加xml文档：
```cs
XmlDocument xdocNew = new XmlDocument();

////创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
XmlDeclaration decNode = xdocNew.CreateXmlDeclaration("1.0", "utf-8", null);
xdocNew.AppendChild(decNode);

//创建根节点
XmlNode rootNode = xdocNew.CreateElement("heros");
xdocNew.AppendChild(rootNode);

//创建子节点
XmlNode hrNode = xdocNew.CreateElement("hero");
//创建一个属性
XmlAttribute skillAttr = xdocNew.CreateAttribute("skill");
skillAttr.Value = "fly";
hrNode.Attributes.Append(skillAttr);
hrNode.InnerText = "超人";
//将子节点添加到根节点上
rootNode.AppendChild(hrNode);

hrNode = xdocNew.CreateElement("hero");
skillAttr = xdocNew.CreateAttribute("skill");
skillAttr.Value = "run fast";
hrNode.Attributes.Append(skillAttr);
hrNode.InnerText = "快银";
//将子节点添加到根节点上
rootNode.AppendChild(hrNode);

//使用XmlElement实例操作也可以，类XmlElement派生自XmlNode
XmlElement xmlEm = xdocNew.CreateElement("hero");
skillAttr = xdocNew.CreateAttribute("skill");
skillAttr.Value = "talk much";
xmlEm.InnerText = "Deadpool";
xmlEm.Attributes.Append(skillAttr);
rootNode.AppendChild(xmlEm);

xdocNew.Save(@"E:\Attachment\maven.xml");
```

生成的maven.xml文档内容如下所示：
```xml
<?xml version="1.0" encoding="utf-8"?>
<heros>
  <hero skill="fly">超人</hero>
  <hero skill="run fast">快银</hero>
  <hero skill="talk much">Deadpool</hero>
</heros>
```

使用XmlDocument进行修改、删除xml节点，以上面的maven.xml为例：
```cs
string mavenXmlPath = @"E:\Attachment\maven.xml";
XmlDocument docMaven = new XmlDocument();
docMaven.Load(mavenXmlPath);

XmlNodeList herosList = docMaven.SelectNodes("/heros/hero");
foreach (XmlNode item in herosList)
{
    item.Attributes["skill"].Value = "new " + item.Attributes["skill"].Value;

    if (item.InnerText.Trim() == "快银")
    {
        //移除快银节点
        docMaven.SelectSingleNode("/heros").RemoveChild(item);
    }
}
docMaven.Save(mavenXmlPath);

```

### XmlDocument和XmlReader
- Dom模型

使用DOM的好处在于它允许编辑和更新XML文档，可以随机访问文档中的数据，可以使用XPath查询。

但是，DOM的缺点在于它需要一次性的加载整个文档到内存中，对于大型的文档，这会造成资源问题。 

- 流模型

流模型很好的解决了这个问题，因为它对XML文件的访问采用的是流的概念，也就是说，任何时候在内存中只有当前节点，但它也有它的不足，它是只读的，仅向前的，不能在文档中执行向后导航操作。

### XDocument(方便、快捷)
LINQ to XML API

和XmlDocument相同的是会把所有的XML所有的节点加载到内存中。

使用XDocument进行创建xml文件，和上面XmlDocument创建xml文档一致，但是代码要精简很多，如下所示：
```cs
//using System.Xml.Linq;
XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", null));

XElement root = new XElement("heros"
    , new XElement("hero", "超人", new XAttribute("skill", "fly"))
    , new XElement("hero", "快银", new XAttribute("skill", "run fast"))
    , new XElement("hero", "Deadpool", new XAttribute("skill", "talk much")));
document.Add(root);

document.Save(@"E:\Attachment\xMaven.xml");
```

拓展参考：

[XML技术总结之XDocument 和XmlDocument](http://www.cnblogs.com/HQFZ/p/4788428.html)

