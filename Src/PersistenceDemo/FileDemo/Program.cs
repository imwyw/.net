using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //为解决转义字符带来的显示问题，需要用\\代表一个\
            string path = "e:\\attachment\\iflytek.txt";

            //@符号的使用
            string path1 = @"e:\attachment\iflytek.txt";

            //没有扩展名的文件也可以
            string unExtPath = @"E:\Attachment\unExxxxxxxxxx";

            //使用@符号字符串内可以进行换行
            string str = @"hello 
                world";

            Console.WriteLine(path);
            Console.WriteLine(path1);

            Console.WriteLine(str);

            Console.WriteLine("=========================================");

            //判断是否存在 E:\attachment\iflytek.txt 文件，不存在则创建
            if (File.Exists(path))
            {
                Console.WriteLine("该文件已存在");
            }
            else
            {
                //静态方法创建文件
                File.Create(path);
            }

            FileInfo finfo = new FileInfo(unExtPath);
            if (finfo.Exists)
            {
                //....
            }
            else
            {
                //实例方法创建文件
                finfo.Create();
            }

            Console.WriteLine("=========================================");

            string directory = @"E:\attachment\2017年9月19日";

            //判断目录是否存在
            if (Directory.Exists(directory))
            {
                Console.WriteLine("已经存在该目录");
            }
            else
            {
                //创建目录
                Directory.CreateDirectory(directory);
            }

            Console.WriteLine("=========================================");

            string filePath = @"E:\attachment\2017年9月19日\Main.txt";

            //使用using自动释放关联资源，否则无法继续进行操作
            using (File.Create(filePath))
            {

            }

            File.WriteAllText(filePath, "秋高气爽，丹桂飘香", Encoding.UTF8);
            string content = File.ReadAllText(filePath, Encoding.UTF8);

            Console.WriteLine("=========================================");


            /*FileStream的使用
            使用using不需要对FileStream进行资源释放
            FileMode.Append 拼接模式，往原先的内容尾部进行添加
            FileMode.OpenOrCreate 存在(覆盖写入)则打开，不存在创建再打开
            */
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                //字符串转换为对应的字节序列
                byte[] array = Encoding.UTF8.GetBytes("十月国庆，黄金周");
                fs.Write(array, 0, array.Length);
            }

            //通过 FileStream 进行读取
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string fsReadStr = Encoding.UTF8.GetString(array);
                Console.WriteLine("#################filestream读取出来的内容:");
                Console.WriteLine(fsReadStr);
            }


            Console.WriteLine("=========================================");
            /*  StreamWriter /StreamReader  */
            //写入
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sw.WriteLine(i + "StreamWriter写入内容");
                    }
                }
            }

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string lineStr;
                    /*注意运算优先级，优先执行的是(lineStr = sr.ReadLine())，
                    然后再执行lineStr ！= null这一部分，返回布尔值
                    */
                    Console.WriteLine("###########通过StreamReader读取内容#########");
                    while ((lineStr = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(lineStr);
                    }
                }
            }
        }
    }
}
