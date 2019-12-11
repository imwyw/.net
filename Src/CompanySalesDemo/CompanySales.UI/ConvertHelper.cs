using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UI
{
    public class ConvertHelper
    {
        /// <summary>
        /// 进行输入的安全转换
        /// 反射匹配方法
        /// 调用反射方法
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tipMessage"></param>
        /// <returns></returns>
        public static T CheckConsoleInput<T>(string tipMessage) where T : struct
        {
            while (true)
            {
                Console.WriteLine(tipMessage);
                string input = Console.ReadLine();

                /* 反射的方式，找到泛型参数T的TryParse方法
                第一个参数（string）：方法的名字
                第二个参数（Type[]）：如有方法重载，需要指定对应的形参类型
                */
                MethodInfo parseMethod = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });

                if (null != parseMethod)
                {
                    // 反射得到方法后，进行invoke调用
                    object[] args = new object[] { input, null };
                    bool success = (bool)parseMethod.Invoke(null, args);
                    if (success)
                    {
                        return (T)args[1];
                    }
                    else
                    {
                        Console.WriteLine("输入的数据格式不正确，请重新输!");
                    }
                }
                else
                {
                    Console.WriteLine("不支持该数据类型的转换!!!");
                }
            }
        }
    }
}
