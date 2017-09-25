<!-- TOC -->

- [编程基础](#编程基础)
    - [数据类型](#数据类型)
        - [变量](#变量)
            - [值类型](#值类型)
            - [引用类型](#引用类型)
        - [常量](#常量)
        - [类型转换](#类型转换)
        - [装箱(boxing)、拆箱(unboxing)](#装箱boxing拆箱unboxing)
    - [流程控制](#流程控制)
        - [顺序](#顺序)
        - [分支](#分支)
        - [循环](#循环)
    - [数组](#数组)
        - [初始化](#初始化)
        - [Array](#array)
        - [循环](#循环-1)
        - [二维数组](#二维数组)
    - [方法](#方法)
        - [定义、调用、形参、实参](#定义调用形参实参)
        - [重载](#重载)
        - [ref、out、params](#refoutparams)
    - [集合](#集合)
        - [ArrayList](#arraylist)
        - [Hashtable](#hashtable)
    - [泛型集合](#泛型集合)
        - [List<T> 很重要！！！](#listt-很重要)
        - [Dictionay<Tkey,Tvalue>](#dictionaytkeytvalue)

<!-- /TOC -->
# 编程基础

## 数据类型
### 变量
#### 值类型
原类型（Sbyte、Byte、Short、Ushort、Int、Uint、Long、Ulong、Char、Float、Double、Bool、Decimal）、枚举(enum)、结构(struct)

值型就是在栈中分配内存，在申明的同时就初始化，以确保数据不为NULL；

#### 引用类型
类、数组、接口、委托、字符串等。

引用型是在堆中分配内存，初始化为null，引用型是需要GARBAGE COLLECTION来回收内存的，值型不用，超出了作用范围，系统就会自动释放！




### 常量

### 类型转换

### 装箱(boxing)、拆箱(unboxing)
装箱就是隐式的将一个值型转换为引用型对象。
```cs
//将i进行装箱操作
int i=0;
object obj=i;
```

拆箱就是将一个引用型对象转换成任意值型
```cs
针对上例中的obj进行拆箱
int res = (int)obj;
```

```cs
int i=0;//L1
object obj=i;//L2
Console.WriteLine(i+","+(int)obj);//L3
```
上述代码实际进行了三次装箱、一次拆箱

L2行，i装箱为obj，1次装箱；

L3行，writeline参数应该为字符串，即i又进行一次装箱，变为string引用类型，2次装箱；

L3行，(int)obj对obj对象进行拆箱转换为值类型，1次拆箱；

L3行，(int)obj的结果为值类型，控制台打印输出需要又一次的装箱为string类型，3次装箱；

频繁的装拆箱会造成性能的损耗

## 流程控制
### 顺序
程序渐进的流程，一条语句一条语句地逐条推进，按预先设置好的步骤来完成功能

### 分支
if...else...

### 循环
- do{}while()
- while(true){}
- for(){}

跳出循环：

break
continue
return


## 数组
### 初始化
在内存中是顺序连续存储的，所以它的索引速度非常快，赋值与修改元素也很简单

```cs
//使用字面值
int[] array = { 1, 2, 3, 4, 5 };
//使用new操作符
int[] array = new int[5];
//两者结合
int[] array = new int[] { 1, 2, 3, 4, 5 };
```

### Array
Array类是一个抽象类，无法实例化该类。

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
```

### 二维数组
```cs
int[,] array1 = { { 1, 2, 3 }, { 4, 5, 6 } };
int[,] array2 = new int[2, 3];
int[,] array3 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

//锯齿数组，同样可以使用for和foreach进行遍历
int[][] array = new int[2][] { new int[] { 1, 2 }, new int[] { 1, 2, 3 } };
int[][] array = new int[][] { new int[] { 1, 2 }, new int[] { 1, 2, 3 }, new int[] { } };
```

## 方法
### 定义、调用、形参、实参
### 重载
```cs
public int Calculate(int x, int y) {......}
public double Calculate(double x, double y) {......}
```
特点（两必须一可以）
1. 方法名必须相同
2. 参数列表必须不相同
3. 返回值类型可以不相同

### ref、out、params


## 集合
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
### Hashtable
Hashtable也并非类型安全的，用于处理和表现类似keyvalue的键值对，其中key通常可用来快速查找，同时key是区分大小写；

value用于存储对应于key的值。Hashtable中keyvalue键值对均为object类型，所以Hashtable可以支持任何类型的keyvalue键值对.

```cs
Hashtable ht = new Hashtable();
ht.Add(0, 1);
ht.Add("key", "name");
ht.Add(1.23, true);

//添加一个keyvalue键值对：
HashtableObject.Add(key,value);

//移除某个keyvalue键值对：
HashtableObject.Remove(key);

//移除所有元素：           
HashtableObject.Clear(); 

// 判断是否包含特定键key：
HashtableObject.Contains(key);
```

什么情况下应该使用Hashtable：
1. 某些数据会被高频率查询
2. 数据量大
3. 查询字段包含字符串类型
4. 数据类型不唯一

## 泛型集合
### List<T> 很重要！！！
List<T>类是 ArrayList 类的泛型等效类。

不会强行对值类型进行装箱和拆箱，或对引用类型进行向下强制类型转换，所以性能得到提高。

```cs
//限制类型只能是string，同样的限定类型也可以是值类型
List<string> lstRes = new List<string>();
//添加对象到结尾处
lstRes.Add("a");
lstRes.Add("b");
lstRes.Add("c");

//长度
int len = lstRes.Count;

//删除
lstRes.Remove("a");

//是否包含某个对象
lstRes.Contains("a");
```
更多的方法等待你去探索。。。

### Dictionay<Tkey,Tvalue>
在初始化的时候也必须指定其类型，而且他还需要指定一个Key,并且这个Key是唯一的。正因为这样，Dictionary的索引速度非常快。但是也因为他增加了一个Key,Dictionary占用的内存空间比其他类型要大。他是通过Key来查找元素的，元素的顺序是不定的。

```cs
Dictionary<string, string> dicRes = new Dictionary<string, string>();
dicRes.Add("zhangsan","张三");
dicRes.Add("zhaofugui", "赵富贵");
```