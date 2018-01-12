using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class CalculateDemo
    {
        static void Print(string[] args)
        {
            /*
            委托
            加法运算，最简单的委托方式，先定义方法Add，再进行传入
            */
            DoCalc(Add, 1, 2);

            /*
            匿名方法
            减法运算，使用匿名方法形式传入
            */
            DoCalc(delegate (int x, int y)
            {
                return x - y;
            }, 1, 2);

            /*
            Lambda表达式
            乘法运算，【=>】左侧(x,y)为参数，【=>】右侧为代码块
            若要创建 Lambda 表达式，需要在 Lambda 运算符 => 左侧指定输入参数(如果有)，然后在另一侧输入表达式或语句块。
            */
            DoCalc((x, y) =>
            {
                return x * y;
            }, 2, 3);

            //上述乘法运算也可以简写为：
            DoCalc((x, y) => x * y, 2, 3);
        }

        /// <summary>
        /// 计算委托的类型
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        delegate int Calculate(int x, int y);

        /// <summary>
        /// 执行计算方法
        /// </summary>
        /// <param name="fun">委托传入的方法</param>
        /// <param name="x">操作数1</param>
        /// <param name="y">操作数2</param>
        static void DoCalc(Calculate fun, int x, int y)
        {
            fun.Invoke(x, y);
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
