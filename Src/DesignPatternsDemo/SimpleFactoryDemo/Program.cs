using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            面向过程方式实现计算器
            1、面向过程的编程
            2、添加运算类型时，改动较大
            OCP 开放-封闭
            */
            string opt = Console.ReadLine();
            double a = double.Parse(Console.ReadLine());
            double b = double.Parse(Console.ReadLine());

            double res;

            switch (opt)
            {
                case "+":
                    res = a + b;
                    break;
                case "-":
                    res = a - b;
                    break;
            }


            /*
            面向对象的思想，通过简单工厂实现的计算器
            调用者角色，
            */
            Operation addOp = SimpleFactory.CreateOperation("+");
            if (null != addOp)
            {
                addOp.NumberA = double.Parse(Console.ReadLine());
                addOp.NumberB = double.Parse(Console.ReadLine());

                Console.WriteLine(addOp.GetResult());
            }

        }

        /// <summary>
        /// 运算类
        /// </summary>
        public class Operation
        {
            public double NumberA { get; set; }
            public double NumberB { get; set; }
            public virtual double GetResult()
            {
                return 0;
            }
        }

        /// <summary>
        /// 简单工厂
        /// 职责：根据外部传入参数(+-*/)输出具体运算类的实例
        /// </summary>
        public class SimpleFactory
        {
            /// <summary>
            /// 返回具体运算类实例
            /// SimpleFactory.CreateOperation()
            /// </summary>
            /// <param name="opt">+、-、*、/</param>
            /// <returns></returns>
            public static Operation CreateOperation(string opt)
            {
                Operation operate = null;
                switch (opt)
                {
                    case "+":
                    case "加":
                        operate = new OperationAdd();
                        break;
                    case "-":
                        operate = new OperationSub();
                        break;
                }
                return operate;
            }
        }

        /// <summary>
        /// 加法类
        /// </summary>
        public class OperationAdd : Operation
        {
            public override double GetResult()
            {
                return NumberA + NumberB;
            }
        }

        /// <summary>
        /// 减法类
        /// </summary>
        public class OperationSub : Operation
        {
            public override double GetResult()
            {
                return NumberA - NumberB;
            }
        }
    }
}
