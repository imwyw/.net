using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDemo
{
    /// <summary>
    /// 定义星期枚举
    /// </summary>
    enum WeekEnum
    {
        周一,
        周二,
        周三,
        周四,
        周五,
        周六,
        周天,
    }

    class Program
    {
        static void Main(string[] args)
        {
            WeekEnum wek = WeekEnum.周二;

            //枚举->数值
            Console.WriteLine((int)wek);//结果：1

            //数值 -> 枚举类型
            int holiday = 6;
            //将数值转换为枚举类型的变量
            wek = (WeekEnum)Enum.Parse(typeof(WeekEnum), holiday.ToString());
            Console.WriteLine(wek);//结果：周天

            //数值 -> 枚举字符串
            string wekStr = Enum.GetName(typeof(WeekEnum), 5);
            Console.WriteLine(wekStr);//结果：周六

            /*==================================================================*/

            //三元运算符 等价于 if。。。else。。。分支
            string name = "admin";
            bool result;
            //下面代码中的if else结构和三元表达式的含义是一致的
            if (name == "admin")
            {
                result = true;
            }
            else
            {
                result = false;
            }

            result = (name == "admin") ? true : false;

            Console.ReadKey();
        }
    }
}
