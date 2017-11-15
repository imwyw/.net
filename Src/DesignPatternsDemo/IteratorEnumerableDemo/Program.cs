using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorEnumerableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StrAggregate agg = new StrAggregate();
            foreach (string item in agg)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            EasyStrAggregate easyAgg = new EasyStrAggregate();
            foreach (string item in easyAgg)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
    }

    public class StrAggregate : IEnumerable
    {
        string[] arr;
        public StrAggregate()
        {
            arr = new string[] { "h", "e", "l", "l", "o" };
        }
        public IEnumerator GetEnumerator()
        {
            return new StrIterator(this);
        }

        public string ElementAt(int index)
        {
            return arr[index];
        }

        internal int GetLength()
        {
            return arr.Length;
        }
    }

    public class StrIterator : IEnumerator
    {
        private StrAggregate strAggregate;

        private int index;

        public StrIterator(StrAggregate strAggregate)
        {
            this.strAggregate = strAggregate;
        }

        /// <summary>
        /// 返回当前指向的元素
        /// </summary>
        public object Current
        {
            get; private set;
        }

        /// <summary>
        /// 指向下一个元素
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (index + 1 <= strAggregate.GetLength())
            {
                Current = strAggregate.ElementAt(index++);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重置，当前指向回到0
        /// </summary>
        public void Reset()
        {
            index = 0;
        }
    }

    /// <summary>
    /// 简化迭代器的实现
    /// </summary>
    public class EasyStrAggregate : IEnumerable
    {
        string[] arr;
        public EasyStrAggregate()
        {
            arr = new string[] { "h", "e", "l", "l", "o" };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 1)
                {
                    yield return arr[i];
                }
            }
        }
    }
}
