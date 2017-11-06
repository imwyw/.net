using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 模拟需求实现如下功能：
/// 用户输入姓名，控制台返回欢迎信息 
/// 默认版，直接返回姓名
/// 中文版，返回：  你好！姓名
/// 英文版，返回： hello！姓名
/// 。。。其他语言版本
/// </summary>
namespace DecoratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*面向过程的思路：*/
            /*
            string name = Console.ReadLine();
            Console.WriteLine(name);
            Console.WriteLine("你好！" + name);
            */

            /*面向对象的思路：*/
            new DefaultWelcome().Say("jack");
            new ZhWelcome().Say("jack");

            /*装饰模式*/
            /*
            1、默认输出
            2、中文
            3、英文
            */
            Component lucy = new ConcreteComponent();
            Decorator zhDecorator = new ZhDecorator();
            zhDecorator.SetComponent(lucy);
            Decorator enDecorator = new EnDecorator();
            enDecorator.SetComponent(zhDecorator);

            enDecorator.Say("lucy");

            /*
            1、默认
            2、英文
            3、中文
            */
            Console.WriteLine();
            Component smith = new ConcreteComponent();
            Decorator enD = new EnDecorator();
            enD.SetComponent(smith);
            Decorator zhD = new ZhDecorator();
            zhD.SetComponent(enD);

            zhD.Say("smith");
        }
    }



}
