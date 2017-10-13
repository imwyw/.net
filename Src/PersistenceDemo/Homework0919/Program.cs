using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework0919
{
    class Program
    {
        static void Main(string[] args)
        {
            Rename();

            ResetLrc();
        }

        /// <summary>
        /// 批量重命名
        /// </summary>
        static void Rename()
        {
            // 相对路径： @"..\..\任务一"
            string[] array = Directory.GetFiles(@"..\..\任务一");
            string oldName = "测试名称";
            string newName = "盖世英雄";
            foreach (string str in array)
            {
                //将测试名称替换为盖世英雄
                string newPath = str.Replace(oldName, newName);

                //移动文件，即进行重命名操作
                File.Move(str, newPath);
            }
        }

        /// <summary>
        /// 歌词时间调整
        /// </summary>
        static void ResetLrc()
        {
            //旧歌词泛型集合
            List<string> lstOldLrc = new List<string>();

            using (FileStream fs = new FileStream(@"..\..\任务二\陈一发-童话镇.lrc", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        lstOldLrc.Add(str);
                    }
                }
            }

            //创建新文件保存修改后的歌词
            string newLrcPath = @"..\..\任务二\童话镇.lrc";
            if (File.Exists(newLrcPath))
            {
                //File.Create不会自动释放，需要人工释放一下资源
                File.Create(newLrcPath).Dispose();
            }
            
            using (FileStream fs = new FileStream(newLrcPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    //遍历每一行歌词
                    foreach (string item in lstOldLrc)
                    {
                        //先截取"]"符号左边为时间部分
                        string prefix = item.Split(']')[0];
                        //再取出分、秒、毫秒字符串
                        string minSeconds = prefix.Substring(1, prefix.Length - 1);
                        string strTime = "1900-01-01 00:" + minSeconds;
                        DateTime time = Convert.ToDateTime(strTime);
                        if (time.Minute == 0 && time.Second == 0 && time.Millisecond == 0)
                        {
                            sw.WriteLine(item);
                            continue;
                        }
                        //DateTime.ToString()参数接收输出格式化参数
                        string newPart = "[" + time.AddSeconds(-1).ToString("mm:ss.ff") + "]" + item.Split(']')[1];
                        sw.WriteLine(newPart);
                    }
                }
            }
        }
    }
}
