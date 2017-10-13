using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework0912
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWork1();

            TestWork2();
        }

        /// <summary>
        /// 写一个程序，要求输入一个两位数，对这十位和个位上的数字进行加减乘除运算，输出结果
        /// </summary>
        static void TestWork1()
        {
            int input = int.Parse(Console.ReadLine());
            int value10 = input / 10;
            int value1 = input % 10;
            Console.WriteLine(value10 + "加" + value1 + "等于：" + (value10 + value1));
            Console.WriteLine(value10 + "减" + value1 + "等于：" + (value10 - value1));
            Console.WriteLine(value10 + "乘" + value1 + "等于：" + (value10 * value1));
            Console.WriteLine(value10 + "除" + value1 + "等于：" + (value10 / value1));
        }

        /// <summary>
        /// 给出一段加密的小写字符串“tbizljbqlfcivqbh”，请问它是什么意思（加密规则如下：小写26字母向后循环递进N位（N不确定），如果N=2则a->c,b->d,z->b等）
        /// </summary>
        static void TestWork2()
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
    }
}
