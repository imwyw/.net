# 高级编程
## 委托
委托是安全封装方法的类型，类似于 C 和 C++ 中的函数指针。 与 C 函数指针不同的是，委托是面向对象的、类型安全的和可靠的。

说白了，委托是一个类，字面理解就是托人干事。将方法作为实参传递，实际传递也就是方法的地址/引用

```cs
//委托调用的方法和委托的定义必须保持一致，如下面的几个示例
void Say(){}
delegate void DelegateTalk();

string Say(){}
delegate string DelegateTalk();

bool Say(int value){}
delegate bool DelegateTalk(int vlaue);
```

```cs
static void Main(string[] args)
{
    BackHome(BuyTicket);
    BackHome(Subway);

    Console.ReadKey();
}

/// <summary>
/// 无参，返回值为void的委托 和下面的Action等同
/// </summary>
//public delegate void DelegateBack();

static void BackHome(Action action)
{
    action();
}

static void BuyTicket()
{
    Console.WriteLine("买火车票");
}

static void Subway()
{
    Console.WriteLine("再换乘地铁");
}
```

针对上例中的Main方法也可以修改为以下方式，以合并委托（多路广播委托）的方式实现
```cs
static void Main(string[] args)
{
    Action action = new Action(BuyTicket);
    action += Subway;

    BackHome(action);

    Console.ReadKey();
}
```

## 反射
在运行期间处理和检测代码，反射指程序可以访问、检测和修改它本身状态或行为的一种能力。

程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。

您可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性。

### 反射（Reflection）
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
    public void Say(string name)
    {
        Console.WriteLine("hi {0},how do u do,i'm {1}", name, Name);
    }
}

static void Main(string[] args)
{
    //以下的type1、type2、type3的是相同的
    Student stu1 = new Student("");
    Type type1 = stu1.GetType();

    Type type2 = typeof(Student);

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
```
看了上面的代码，也许会有疑问，既然在开发时就能够写好代码，干嘛还放到运行期去做，不光繁琐，而且效率也受影响。

很多设计模式是基于反射实现的，设计模式的好处是复用解决方案，可靠性高等。如何取舍是一个见仁见智的问题。。。

### 自定义特性（Attribute）
#### 特性是什么
Attribute 是一种可由用户自由定义的修饰符（Modifier），可以用来修饰各种需要被修饰的目标。

简单的说，Attribute就是一种“附着物” —— 就像牡蛎吸附在船底或礁石上一样。

这些附着物的作用是为它们的附着体追加上一些额外的信息（这些信息就保存在附着物的体内）—— 比如这个属性对应数据库中哪个字段，这个类对应数据库中哪张表等等。

#### 作用
特性Attribute 的作用是添加元数据。

元数据可以被工具支持，比如：编译器用元数据来辅助编译，调试器用元数据来调试程序。

#### Attribute 与注释的区别
- 注释是对程序源代码的一种说明，主要目的是给人看的，在程序被编译的时候会被编译器所丢弃，因此，它丝毫不会影响到程序的执行。
- Attribute是程序代码的一部分，不但不会被编译器丢弃，而且还会被编译器编译进程序集（Assembly）的元数据（Metadata）里，在程序运行的时候，你随时可以从元数据里提取出这些附加信息来决策程序的运行。

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

    //获取Student类上自定义特性
    Attribute stuAttr = type.GetCustomAttribute(typeof(FieldChNameAttribute), false);

    //该类有FieldChaNameAttribute自定义特性，则获取设置的属性值ChName
    if (null != stuAttr)
    {
        FieldChNameAttribute stuChAttr = stuAttr as FieldChNameAttribute;
        Console.WriteLine(stuChAttr.ChName);//学生实体
    }

    //获取类型的所有公开属性
    PropertyInfo[] props = type.GetProperties();
    //遍历公开属性
    foreach (PropertyInfo pp in props)
    {
        //获取属性上的自定义特性
        Attribute ppAttr = pp.GetCustomAttribute(typeof(FieldChNameAttribute), false);
        //如属性有自定义特性，则获取设置的属性值ChName
        if (null != ppAttr)
        {
            FieldChNameAttribute ppChAttr = ppAttr as FieldChNameAttribute;
            Console.WriteLine(ppChAttr.ChName);
        }
    }
}
```

参考以下文章：

[C# 中自定义Attribute值的获取与优化](http://kb.cnblogs.com/page/87531/)

[关于C# 中的Attribute 特性](http://blog.csdn.net/hegx2001/article/details/50352225)



