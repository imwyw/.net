using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 命名空间，如304班王富贵，303班王富贵的区分
/// </summary>
namespace FirstDemo
{
    /// <summary>
    /// 类名，如上注释中说明的王富贵
    /// </summary>
    class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //写一个程序，要求输入一个小数，输出的数将它的整数部分和小数部分对调（比如输入3.14，输出14.3）。
            //double input = double.Parse(Console.ReadLine());

            while (true)
            {
                //接收控制台的输入信息
                string input = Console.ReadLine();

                //根据符号'.'进行拆分数组，如 1.23得到结果为数组["1","23"]
                string[] arrInput = input.Split('.');

                //进行判断数组长度是否符合规范，即为整数时arrInput长度为1，不可按照索引1进行取元素，否则会发生异常
                if (arrInput.Length < 2)
                {
                    //控制台打印提示信息
                    Console.WriteLine("请输入合法的小数：");

                    /*
                    注意此处为什么用continue！而非break。
                    continue不再执行循环体后面的语句，但会继续循环
                    break会直接跳出循环，不再继续进行循环
                    */
                    continue;
                }
                Console.WriteLine(arrInput[1] + "." + arrInput[0]);
            }

        }

    }
}
