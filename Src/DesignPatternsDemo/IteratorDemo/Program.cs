using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteAggregate agg = new ConcreteAggregate();
            IIterator ite = agg.CreateIterator();

            while (ite.HasNext())
            {
                Console.Write(ite.Next());
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 迭代器接口
    /// </summary>
    public interface IIterator
    {
        /// <summary>
        /// 是否有下一个元素
        /// </summary>
        /// <returns></returns>
        bool HasNext();
        /// <summary>
        /// 指向到下一个元素，并返回它
        /// </summary>
        /// <returns></returns>
        object Next();
    }

    /// <summary>
    /// 聚合接口
    /// </summary>
    public interface IAggregate
    {
        IIterator CreateIterator();
    }

    /// <summary>
    /// 自定义的迭代器
    /// </summary>
    public class ConcreteIterator : IIterator
    {
        private ConcreteAggregate concreteAggregate;
        private int index;

        public ConcreteIterator(ConcreteAggregate concreteAggregate)
        {
            this.concreteAggregate = concreteAggregate;
        }

        public bool HasNext()
        {
            if (index < concreteAggregate.GetLength())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object Next()
        {
            return concreteAggregate.ElementAt(index++);
        }
    }

    /// <summary>
    /// 具体的聚合类
    /// </summary>
    public class ConcreteAggregate : IAggregate
    {
        string[] arr;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConcreteAggregate()
        {
            arr = new string[] { "h", "e", "l", "l", "o" };
        }

        /// <summary>
        /// 获取自定义迭代器
        /// </summary>
        /// <returns></returns>
        public IIterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        /// <summary>
        /// 获取数组长度
        /// </summary>
        /// <returns></returns>
        internal int GetLength()
        {
            return arr.Length;
        }

        /// <summary>
        /// 返回索引位置的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ElementAt(int index)
        {
            return arr[index];
        }
    }
}
