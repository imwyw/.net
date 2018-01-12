using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    /// <summary>
    /// 扩展方法的定义
    /// </summary>
    static class ClassExtendMethod
    {
        public static void Print()
        {
            string name = "不识美妻刘强东";
            //ToConsole 把变量值打印到控制台
            name.ToConsole();
        }

        public static void ToConsole(this string source)
        {
            Console.WriteLine(source);
        }
    }
}
