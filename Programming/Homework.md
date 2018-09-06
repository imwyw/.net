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

## 循环
### 解密字符串
题：给出一段加密的小写字符串“tbizljbqlfcivqbh”，请问它是什么意思（加密规则如下：小写26字母向后循环递进N位（N不确定），如果N=2则a->c,b->d,z->b等）
```cs
static void Decrypt()
{
    //26个字母字符串
    string abc = "abcdefghijklmnopqrstuvwxyz";

    //密文
    string cipher = "tbizljbqlfcivqbh";

    //解密后的明文
    string result = "";

    //递进的位数
    int step = 1;

    //总共有26个字符，则递进结果有25种，需要进行25次循环
    for (int i = 0; i < 24; i++, step++)
    {
        //每次循环需要将上次循环得到的结果清空
        result = "";

        //依次找寻密文中的每一个字符
        for (int j = 0; j < cipher.Length; j++)
        {
            //利用Substring方法得到当前索引的字符
            string curChar = cipher.Substring(j, 1);

            //找到密文中每个字符在abc字符串中出现的位置
            int index = abc.IndexOf(curChar);

            //考虑循环递进的关系，需要对总数26进行取余，例如递进2，z字符得到的其实是b
            int resIndex = (index + step) % 26;

            //将本次解密得到的字符添加到结果字符串result中
            result += abc.Substring(resIndex, 1);
        }
        Console.WriteLine("递进" + step + "位的结果是：" + result);
    }
}
```

## 方法
### 计算第几日
题：编写函数，给出年月日，计算该日是本年的第几天

公历闰年判定遵循的规律为:四年一闰,百年不闰,四百年再闰.

公历闰年的简单计算方法如下：（符合以下条件之一的年份即为闰年）
a)能被4整除而不能被100整除。
b)能被100整除也能被400整除。
```cs
/// <summary>
/// 返回指定日期是该年第多少天
/// </summary>
/// <param name="date"></param>
/// <returns></returns>
static int DayInTheYear(DateTime date)
{
    //每个月的天数，以非闰年为例，2月份当28天处理
    int[] daysInMon = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    int year = date.Year;
    bool isLoop = IsLeapYear(year);
    int res = 0;
    for (int i = 0; i < date.Month - 1; i++)
    {
        res += daysInMon[i];
    }
    res += date.Day;
    if (isLoop && date.Month > 2)
    {
        res++;
    }
    return res;
}

/// <summary>
/// 判断您是否为闰年
/// </summary>
/// <param name="year"></param>
/// <returns></returns>
static bool IsLeapYear(int year)
{
    if (year % 4 == 0 && year % 100 != 0)
    {
        return true;
    }
    if (year % 400 == 0)
    {
        return true;
    }
    return false;
}
```

