<!-- TOC -->

- [文件、流](#文件流)
    - [文件相关类](#文件相关类)
    - [路径Path](#路径path)
        - [当前路径获取](#当前路径获取)
    - [文件File](#文件file)
    - [目录Directory](#目录directory)
    - [文件读写](#文件读写)
        - [文本文件](#文本文件)
    - [文件流](#文件流)
        - [文件流和字符串的转换](#文件流和字符串的转换)
        - [FileStream](#filestream)
        - [StreamReader/StreamWriter](#streamreaderstreamwriter)
        - [BinaryWriter/BinaryReader](#binarywriterbinaryreader)
    - [对象持久化](#对象持久化)

<!-- /TOC -->
<a id="markdown-文件流" name="文件流"></a>
# 文件、流

<a id="markdown-文件相关类" name="文件相关类"></a>
## 文件相关类

文件操作常用相关类:

类名 | 说明
---|---
File | 操作文件，静态类，对文件整体操作。拷贝、删除、剪切等。
Directory | 操作目录（文件夹），静态类。
DirectoryInfo | 文件夹的一个“类”，用来描述一个文件夹对象（获取指定目录下的所有目录时返回一个DirectoryInfo数组。）
FileInfo | 文件类，用来描述一个文件对象。获取指定目录下的所有文件时，返回一个FileInfo数组。
Path | 对文件或目录的路径进行操作（很方便）【字符串】
Stream | 文件流，抽象类。
FileStream | 文件流，MemoryStream(内存流)，NetworkStream(网络流)
StreamReader | 快速读取文本文件
StreamWriter | 快速写入文本文件

<a id="markdown-路径path" name="路径path"></a>
## 路径Path
用户在磁盘上寻找文件时，所历经的文件夹线路叫路径。

Windows 约定使用反斜线 (\\) 作为路径中的分隔符。UNIX 系统使用正斜线 (/)。

- 绝对路径：从根文件夹开始的路径，对于windows文件系统来说，从盘符开始。
```cs
//以下两种方式均可以，第一种更方便
string filePath = @"E:\Attachment\iflytek.txt";
string filePath = "E:\\Attachment\\iflytek.txt";
```

- 相对路径：相对路径是指相对于当前目录的位置。

相对路径使用两种特殊符号，单点 (.) 和双点 (..)，通过它们可以转换到当前目录或父目录。

双点用于在目录等级中上移。单点表示当前目录本身。

`.\iflytek.txt`

C#中用于操作路径的类：System.IO.Path
```cs
//将两个字符串组合成一个路径
Path.Combine(@"E:\Attachment", "iflytek.txt");//E:\Attachment\iflytek.txt

//返回指定的路径字符串的扩展名
Path.GetExtension(@"E:\Attachment\iflytek.txt");//.txt

//返回指定路径字符串的文件名和扩展名
Path.GetFileName(@"E:\Attachment\iflytek.txt");//iflytek.txt

//返回不具有扩展名的指定路径字符串的文件名
Path.GetFileNameWithoutExtension(@"E:\Attachment\iflytek.txt");//iflytek

//其他方法自行查看研究
```

<a id="markdown-当前路径获取" name="当前路径获取"></a>
### 当前路径获取

1、取得控制台应用程序的根目录方法

     方法1、Environment.CurrentDirectory 取得或设置当前工作目录的完整限定路径
     
     方法2、AppDomain.CurrentDomain.BaseDirectory 获取基目录，它由程序集冲突解决程序用来探测程序集
     
 2、取得Web应用程序的根目录方法
 
     方法1、HttpRuntime.AppDomainAppPath.ToString();//获取承载在当前应用程序域中的应用程序的应用程序目录的物理驱动器路径。用于App_Data中获取

     方法2、Server.MapPath("") 或者 Server.MapPath("~/");//返回与Web服务器上的指定的虚拟路径相对的物理文件路径
     
     方法3、Request.ApplicationPath;//获取服务器上ASP.NET应用程序的虚拟应用程序根目录
     
 3、取得WinForm应用程序的根目录方法

     1、Environment.CurrentDirectory.ToString();//获取或设置当前工作目录的完全限定路径

     2、Application.StartupPath.ToString();//获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称
     
     3、Directory.GetCurrentDirectory();//获取应用程序的当前工作目录

     4、AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集
     
     5、AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称
     
其中：以下两个方法可以获取执行文件名称

     1、Process.GetCurrentProcess().MainModule.FileName;//可获得当前执行的exe的文件名。

     2、Application.ExecutablePath;//获取启动了应用程序的可执行文件的路径，包括可执行文件的名称
     
     3、System.IO.Path类中有一些获取路径的方法，可以在控制台程序或者WinForm中根据相对路径来获取绝对路径


<a id="markdown-文件file" name="文件file"></a>
## 文件File
- File:静态类，提供文件操作的静态方法，无法被实例化。
- FileInfo:可以被实例化，一个对象对应一个文件。

判断一个文件路径是否存在，不存在则创建之，对应于以下的两种方案，均可。
```cs
string filePath = @"E:\attachment\iflytek.txt";

//使用静态类File判断文件是否存在
if (File.Exists(filePath))
{
    Console.WriteLine("该文件已存在");
}
else
{
    //File.Create不会自动释放，需要人工释放一下资源
    File.Create(filePath).Dispose();
}

//使用FileInfo实例化进行判断
FileInfo fl = new FileInfo(filePath);
if (fl.Exists)
{
    Console.WriteLine("该文件已存在");
}
else
{
    //File.Create不会自动释放，需要人工释放一下资源
    fl.Create().Dispose();
}
```


<a id="markdown-目录directory" name="目录directory"></a>
## 目录Directory
目录仅指定到文件夹，而非文件。

- Directory:静态类，提供了操作目录的静态方法。
- DirectoryInfo:可以被实例化，一个DirectoryInfo对象对应一个目录。

判断一个目录是否存在，存在则遍历该目录下所有目录和文件，不存在则创建之
```cs
string directory = @"E:\attachment\iflytek";

if (Directory.Exists(directory))
{
    Console.WriteLine("该目录已存在，有以下目录：");
    
    //显示该目录下所有目录
    string[] dirts = Directory.GetDirectories(directory);
    foreach (string item in dirts)
    {
        Console.WriteLine(item);
    }

    //显示该目录下所有文件
    string[] filePaths = Directory.GetFiles(directory);
    foreach (string item in filePaths)
    {
        Console.WriteLine(item);
    }
}
else
{
    Directory.CreateDirectory(directory);
}

//使用DirectoryInfo方案省略。。。
```

<a id="markdown-文件读写" name="文件读写"></a>
## 文件读写
<a id="markdown-文本文件" name="文本文件"></a>
### 文本文件
```cs
string path = @"D:\attachment\iflytek.txt";

//以utf-8格式读取文件，读取文件全部内容
string str = File.ReadAllText(path, Encoding.UTF8);

//创建一个新文件，在其中写入指定的字符串，然后关闭文件。如果目标文件已存在，则覆盖该文件。
File.WriteAllText(path, "hello world", Encoding.UTF8);

//将指定的字符串追加到文件中，如果文件还不存在则创建该文件。
File.AppendAllText(path, "how do u do", Encoding.UTF8);
```

<a id="markdown-文件流" name="文件流"></a>
## 文件流

文件和流是有区别的，文件是存储在磁盘上的数据集，它具有名称和相应的路径。

当打开一个文件并对其进行读/写时，该文件就称为流（stream）。但是，流不仅仅是指打开的磁盘文件，还可以是网络数据。

.Net Framework允许在内存中创建流。此外，在控制台应用程序中，键盘输入和文本显示都是流。

通俗的来讲，读一点处理一点(以及相反的生成一点, 写入一点)的数据类型(或操作)抽象出来, 这就是流。

流包括以下基本操作：

* 读取（read）：把数据从流传输到某种数据结构中，如输出到字符数组中。
* 写入（write）：把数据从某种数据结构传输到流中，如把字节数组中的数据传输到流中。
* 定位（seek）：在流中查找或重新定位当前位置。

**流** 是一个用于传输数据的对象，按照传输方向分为读取流/写入流：
- 读取流，从外部源到程序中，即从硬盘到内存。
- 写入流，从程序传输到外部源中，即从内存到硬盘。

在这里外部源通常指硬盘中的文件，但也不完全是文件，还可以是网络上的传输数据等等。

对于文件的读写，最常用的类如下：
- Stream类， Stream类是所有流的抽象基类。
- FileStream(文件流)，这个类主要用于在二进制文件中读写二进制，也可以使用读写任何文件。
- MemoryStream（内存流），在内存中创建流，以暂时保持数据，因此有了它就无须在硬盘上创建临时文件。它将数据封装为无符号的字节序列，可以直接进行读、写、查找操作。
- BufferedStream（缓冲流），把流先添加到缓冲区，再进行数据的读/写操作。缓冲区是存储区中用来缓存数据的字节块。使用缓冲区可以减少访问数据时对操作系统的调用次数，增强系统的读/写功能。
- StreamReader(流读取器)和StreamWriter(流写入器)，以一种特定的编码（如：UTF-8）从字节流中读取字符，流写入器StreamWriter类用来以一种特定的编码（如：UTF-8）向流中写入字符。StreamReader和StreamWriter类一般用来操作文本文件。
- BinaryReader和BinaryWriter，用特定的编码将基元数据类型读作二进制或以二进制形式将基元类型写入流，并支持用特定的编码写入字符串。

<a id="markdown-文件流和字符串的转换" name="文件流和字符串的转换"></a>
### 文件流和字符串的转换

在文件流的操作过程中，我们经常需要使用到字符串与字节数组之间的互相转换：
```cs
//string->byte[]
string msg = "helloworld";
//使用UTF8国际编码
byte[] bytes = System.Text.Encoding.UTF8.GetByte(msg);

//byte[]->string
string resMsg = System.Text.Encoding.UTF8.GetString(bytes);
```

<a id="markdown-filestream" name="filestream"></a>
### FileStream
这个类提供了在文件中读写字节的方法，但经常使用StreamReader或 StreamWriter执行这些功能。

这是因为FileStream类操作的是字节和字节数组，而Stream类操作的是字符数据。

```cs
string path = @"E:\Attachment\iflytek\iflytek.txt";

//使用指定的路径和创建模式初始化 System.IO.FileStream 类的新实例。指定操作系统应打开文件（如果文件存在）；否则，应创建新文件。
FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

//转换为字节序列
byte[] writeIn = Encoding.UTF8.GetBytes("hello world");
//将字节序列写入文件流中
fs.Write(writeIn, 0, writeIn.Length);
//关闭当前流并释放与之关联的所有资源
fs.Close();

fs = new FileStream(path, FileMode.OpenOrCreate);
byte[] array = new byte[fs.Length];
fs.Read(array, 0, array.Length);
string content = Encoding.UTF8.GetString(array);
//关闭当前流并释放与之关联的所有资源
fs.Close();
```

<a id="markdown-streamreaderstreamwriter" name="streamreaderstreamwriter"></a>
### StreamReader/StreamWriter

用来处理流数据，提供了高效的流读写功能。可以直接用字符串进行读写，而不用转换成字节数组。

```cs
string path = @"E:\Attachment\iflytek\iflytek.txt";

//写入
StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
sw.Write("hello streamwriter");
sw.Close();

//读取
StreamReader sr = new StreamReader(path, Encoding.UTF8);
string content = sr.ReadToEnd();
sr.Close();

/*=================或使用以下方案也可以=================*/
string path = @"E:\Attachment\iflytek\iflytek.txt";

/*
写入，使用文件流方式作为参数传入StreamWriter构造函数
使用using会自动进行释放，不需要调用close进行释放资源
*/
using (FileStream fs = new FileStream(path, FileMode.Append))
{
    using (StreamWriter sw = new StreamWriter(fs))
    {
        sw.Write("hello streamwriter" + Environment.NewLine);
    }
}

/*
读取，使用文件流方式作为参数传入StreamWriter构造函数
使用using会自动进行释放，不需要调用close进行释放资源
*/
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
    {
        string content = sr.ReadToEnd();
    }
}
```

拓展之-每次写一行，每次读取一行
```cs
string path = @"E:\Attachment\iflytek\iflytek.txt";

/*
写入，使用文件流方式作为参数传入StreamWriter构造函数
使用using会自动进行释放，不需要调用close进行释放资源
*/
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (StreamWriter sw = new StreamWriter(fs))
    {
        for (int i = 0; i < 10; i++)
        {
            sw.WriteLine(i + "hello streamwriter");
        }
    }
}

/*
读取，使用文件流方式作为参数传入StreamWriter构造函数
使用using会自动进行释放，不需要调用close进行释放资源
*/
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
    {
        string content;
        while ((content = sr.ReadLine()) != null)
        {
            Console.WriteLine(content);
        }
    }
}
```

<a id="markdown-binarywriterbinaryreader" name="binarywriterbinaryreader"></a>
### BinaryWriter/BinaryReader
```cs
string path = @"E:\Attachment\iflytek\stu.bin";

//写入
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (BinaryWriter bw = new BinaryWriter(fs))
    {
        bw.Write("文本信息转换为二进制");
    }
}
//读取
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (BinaryReader br = new BinaryReader(fs))
    {
        string content = br.ReadString();
    }
}
```

<a id="markdown-对象持久化" name="对象持久化"></a>
## 对象持久化
```cs
[Serializable]
public class Student
{
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public string Name { get; set; }
    public int Age { get; set; }
}

string path = @"E:\Attachment\iflytek\stu.bin";

//写入
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    Student stu = new Student("王富贵", 123);

    //创建二进制序列化器，需要引用using System.Runtime.Serialization.Formatters.Binary;
    BinaryFormatter bf = new BinaryFormatter();

    //进行序列化保存，需要进行序列化的类必须有Serializable特性
    bf.Serialize(fs, stu);
}

//读取
using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
{
    using (BinaryReader br = new BinaryReader(fs))
    {
        //需要引用using System.Runtime.Serialization.Formatters.Binary;
        BinaryFormatter bf = new BinaryFormatter();

        Student stu = bf.Deserialize(fs) as Student;
    }
}
```

使用二进制格式序列化时，它不仅是将对象的字段数据进行持久化，

也持久化每个类型的完全限定名称和定义程序集的完整名称（包括包称、版本、公钥标记、区域性），

这些数据使得在进行二进制格式反序列化时亦会进行类型检查。

所以反序列化时的运行环境要与序列化时的运行环境要相同，否者可能会无法反序列化成功。

---

参考引用：

[C# 获取当前程序运行路径](https://blog.csdn.net/june905206961/article/details/78428551)

[C# 文件操作（摘抄）](https://www.cnblogs.com/hellowzl/p/6797556.html)