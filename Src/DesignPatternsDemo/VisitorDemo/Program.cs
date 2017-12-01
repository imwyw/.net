using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStructure objs = new ObjectStructure();
            objs.Show();
        }
    }

    /// <summary>
    /// 数据结构的抽象类
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// 接收访问者访问的方法
        /// </summary>
        /// <param name="v"></param>
        public abstract void Accept(Visitor v);
        /// <summary>
        /// 数据具有的方法
        /// </summary>
        public abstract void Print();
    }
    /// <summary>
    /// 具体元素A
    /// </summary>
    public class ConcreteElementA : Element
    {
        /// <summary>
        /// 接收访问者访问实现
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 元素A的方法
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("ElementA");
        }
    }

    public class ConcreteElementB : Element
    {
        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("ElementB");
        }
    }

    /// <summary>
    /// 访问者抽象类
    /// </summary>
    public abstract class Visitor
    {
        public abstract void Visit(ConcreteElementA em);
        public abstract void Visit(ConcreteElementB em);
    }

    /// <summary>
    /// 具体访问者
    /// </summary>
    public class ConcreteVisitor : Visitor
    {
        /// <summary>
        /// 访问实现，操作实例
        /// </summary>
        /// <param name="em"></param>
        public override void Visit(ConcreteElementA em)
        {
            //todo 针对 ElementA实例 xxxx
            em.Print();
        }

        /// <summary>
        /// 访问实现，操作实例
        /// </summary>
        /// <param name="em"></param>
        public override void Visit(ConcreteElementB em)
        {
            //todo 针对 ElementB实例 xxxx
            em.Print();
        }
    }

    /// <summary>
    /// 对象结构，模拟具体业务
    /// </summary>
    public class ObjectStructure
    {
        List<Element> lst = new List<Element>();
        Random rd = new Random();
        public ObjectStructure()
        {
            for (int i = 0; i < 10; i++)
            {
                if (rd.Next(0, 2) % 2 == 0)
                {
                    lst.Add(new ConcreteElementA());
                }
                else
                {
                    lst.Add(new ConcreteElementB());
                }
            }
        }

        public void Show()
        {
            Visitor v1 = new ConcreteVisitor();
            foreach (Element item in lst)
            {
                item.Accept(v1);
            }
        }
    }
}
