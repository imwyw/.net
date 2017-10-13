/*
编写一个能判断是否能构成三角形的函数，如果能则使用out参数输出其周长和面积
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework0913
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("==================================================================");
                Console.WriteLine("请输入三角形的三个边长，请以逗号间隔：如1,2,3");
                string input = Console.ReadLine();

                //防止有中文逗号参与，将中文逗号替换成英文逗号，再进行拆分
                string[] arrInput = input.Replace("，", ",").Split(',');
                if (arrInput.Length != 3)
                {
                    Console.WriteLine("请输入合法的三个边长");
                    continue;
                }
                double a = double.Parse(arrInput[0]);
                double b = double.Parse(arrInput[1]);
                double c = double.Parse(arrInput[2]);

                //判定是否符合任意两边之和大于第三边
                if ((a + b) > c && (a + c) > b && (b + c) > a)
                {
                    Console.WriteLine("周长：" + (a + b + c));

                    //根据海伦公式计算面积，已知三边长求面积
                    double p = (a + b + c) / 2;
                    double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                    //Math.Round 仅保留两位有效小数
                    Console.WriteLine("面积：" + Math.Round(s, 2));
                }
                else
                {
                    Console.WriteLine("无法构建三角形，不满足任意两边之和大于第三边");
                }
            }

        }

    }
}
