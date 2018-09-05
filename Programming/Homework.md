# Homework
## 变量和类型转换
### 数字比较
题：从键盘上输入2个数，找出最大的数；
```cs
/// <summary>
/// 从键盘上输入2个数，找出最大的数；
/// </summary>
static void Solution1_1()
{
    Console.WriteLine("请输入数字A:");
    int a = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("请输入数字B:");
    int b = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("数值A:" + a);
    Console.WriteLine("数值B:" + b);

    //方案1
    /*
    int max = A;
    if (A < B)
    {
        max = B;
    }
    */

    //方案2 三元运算符
    int max = a > b ? a : b;
    //Console.WriteLine("最大值：{0}", max);
    Console.WriteLine("最大值：" + max);
}
```

题：从键盘上输入3个数，找出最大的数；
```cs
/// <summary>
/// 从键盘上输入3个数，找出最大的数；
/// </summary>
static void Solution1_2()
{
    int a, b, c, max;
    Console.WriteLine("请输入整数A：");
    a = int.Parse(Console.ReadLine());
    Console.WriteLine("请输入整数B：");
    b = int.Parse(Console.ReadLine());
    Console.WriteLine("请输入整数C：");
    c = int.Parse(Console.ReadLine());

    max = a > b ? a : b;
    max = max > c ? max : c;
    Console.WriteLine("三个数：{0}，{1}，{2}中最大的是：{3}", a, b, c, max);
}
```

题：从键盘上输入3个数，把他们从大到小排列起来；
```cs
static void Solution1_3()
{
    int a, b, c;
    Console.WriteLine("请输入整数A：");
    a = int.Parse(Console.ReadLine());
    Console.WriteLine("请输入整数B：");
    b = int.Parse(Console.ReadLine());
    Console.WriteLine("请输入整数C：");
    c = int.Parse(Console.ReadLine());

    //使用冒泡方式进行排序
    //1.首先定义数组保存三个数值
    int[] arr = new int[3] { a, b, c };
    //2.开始冒泡排序
    for (int i = 0; i < arr.Length - 1; i++)
    {
        for (int j = i + 1; j < arr.Length; j++)
        {
            if (arr[i] < arr[j])
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }

    Console.Write("冒泡从大到小得到：");
    //3.遍历打印
    foreach (int item in arr)
    {
        Console.Write(item + "\t");
    }
}
```

### 两位数的十位和个位
题：写一个程序，要求输入一个两位数，对这十位和个位上的数字进行加减乘除运算，输出结果
```cs
static void Solution2()
{
    Console.WriteLine("请输入一个两位数：");
    int input = int.Parse(Console.ReadLine());

    //十位
    int decadeNum = input / 10;
    //个位
    int digitNum = input % 10;

    Console.WriteLine("加法得到：" + decadeNum + digitNum);
    Console.WriteLine("减法得到：" + (decadeNum - digitNum));
    Console.WriteLine("乘法得到：" + decadeNum * digitNum);
    if (digitNum != 0)
    {
        Console.WriteLine("除法得到：" + decadeNum / digitNum);
    }
    else
    {
        Console.WriteLine("个位数为0，无法进行除法运算。");
    }
}
```

### 整数和小数部分对调
题：写一个程序，要求输入一个小数，输出的数将它的整数部分和小数部分对调。比如14.3换成3.14
```cs
static void Solution3()
{
    Console.WriteLine("请输入小数：");
    string input = Console.ReadLine();

    string[] arr = input.Split('.');
    Console.WriteLine("{0}对调后为：{1}", input, (arr[1] + arr[0]));
}
```