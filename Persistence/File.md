# 文件、流
## 路径
用户在磁盘上寻找文件时，所历经的文件夹线路叫路径。Windows 约定使用反斜线 (\\) 作为路径中的分隔符。UNIX 系统使用正斜线 (/)。

- 绝对路径：从根文件夹开始的路径，对于windows文件系统来说，从盘符开始。
```cs
//以下两种方式均可以，第一种更方便
string filePath = @"E:\Attachment\iflytek.txt";
string filePath = "E:\\Attachment\\iflytek.txt";
```

- 相对路径：相对路径是指相对于当前目录的位置。相对路径使用两种特殊符号，单点 (.) 和双点 (..)，通过它们可以转换到当前目录或父目录。双点用于在目录等级中上移。单点表示当前目录本身。

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
    File.Create(filePath);
}

//使用FileInfo实例化进行判断
FileInfo fl = new FileInfo(filePath);
if (fl.Exists)
{
    Console.WriteLine("该文件已存在");
}
else
{
    fl.Create();
}
```

## 目录
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

## 文件读写
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

## 流
**流** 是一个用于传输数据的对象，按照传输方向分为读取流/写入流：
- 读取流，从外部源到程序中，即从硬盘到内存。
- 写入流，从程序传输到外部源中，即从内存到硬盘。

在这里外部源通常指硬盘中的文件，但也不完全是文件，还可以是网络上的传输数据等等。

对于文件的读写，最常用的类如下：
- FileStream(文件流)，这个类主要用于在二进制文件中读写二进制，也可以使用读写任何文件。
- StreamReader(流读取器)和StreamWriter(流写入器)，这两个专门用于读写文本文件。

### FileStream
读写通过字节序列的方式

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

### StreamReader/StreamWriter
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
        sw.Write("hello streamwriter\n");
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

    //需要引用using System.Runtime.Serialization.Formatters.Binary;
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