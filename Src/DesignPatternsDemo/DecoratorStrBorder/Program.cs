using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorStrBorder
{
    class Program
    {
        static void Main(string[] args)
        {
            Display dis = new StringDisplay("hello world");
            Border b1 = new SideBorder(new SideBorder(dis, '*'), '@');
            b1.Show();
            Console.WriteLine("=================================================");

            Border b2 = new FullBorder(new SideBorder(new StringDisplay("AB"), '~'), '#');
            b2.Show();

            /*
            FileStream fs = new FileStream("", FileMode.OpenOrCreate);
            BufferedStream bs = new BufferedStream(fs);
            CryptoStream cs = new CryptoStream(fs);
            GZipStream gs = new GZipStream(fs, CompressionLevel.NoCompression);
            */
        }
    }

    /// <summary>
    /// 抽象组件类
    /// </summary>
    public abstract class Display
    {
        /// <summary>
        /// 获取横向字符数，有多少个字符
        /// </summary>
        /// <returns></returns>
        public abstract int GetColumns();

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        public abstract int GetRows();

        /// <summary>
        /// 获取第row行的字符串，row是索引，从0开始
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public abstract string GetRowText(int row);

        /// <summary>
        /// 显示所有字符串
        /// </summary>
        public void Show()
        {
            for (int i = 0; i < GetRows(); i++)
            {
                Console.WriteLine(GetRowText(i));
            }
        }
    }

    /// <summary>
    /// 单行字符串
    /// </summary>
    public class StringDisplay : Display
    {
        private string str;
        public StringDisplay(string str)
        {
            this.str = str;
        }

        public override int GetColumns()
        {
            return str.Length;
        }

        public override int GetRows()
        {
            return 1;
        }

        public override string GetRowText(int row)
        {
            if (row == 0)
            {
                return str;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 装饰抽象类
    /// </summary>
    public abstract class Border : Display
    {
        protected Display display;
        public Border(Display display)
        {
            this.display = display;
        }
    }

    /// <summary>
    /// 左右加边框
    /// </summary>
    public class SideBorder : Border
    {
        private char ch;
        public SideBorder(Display display, char ch) : base(display)
        {
            this.ch = ch;
        }
        public override int GetColumns()
        {
            return 1 + display.GetColumns() + 1;
        }

        public override int GetRows()
        {
            return display.GetRows();
        }

        public override string GetRowText(int row)
        {
            return ch + display.GetRowText(row) + ch;
        }
    }
    /// <summary>
    /// 全边框装饰
    /// </summary>
    public class FullBorder : Border
    {
        private char ch;
        public FullBorder(Display display, char ch) : base(display)
        {
            this.ch = ch;
        }
        public override int GetColumns()
        {
            return 1 + display.GetColumns() + 1;
        }

        public override int GetRows()
        {
            return 1 + display.GetRows() + 1;
        }

        public override string GetRowText(int row)
        {
            if (row == 0)//第一行
            {
                return ch + MakeLine() + ch;
            }
            else if (row == display.GetRows() + 1)//最后一行
            {
                return ch + MakeLine() + ch;
            }
            else//除第一行、最后一行其他行
            {
                return ch + display.GetRowText(row - 1) + ch;
            }
        }

        private string MakeLine()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < display.GetColumns(); i++)
            {
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
