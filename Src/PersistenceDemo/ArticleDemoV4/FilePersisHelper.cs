using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemoV4
{
    public static class FilePersisHelper
    {
        /// <summary>
        /// 保存集合中数据到文本文件
        /// </summary>
        /// <param name="v"></param>
        /// <param name="lstUser"></param>
        public static void SaveData(string v, List<User> lstUser)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, v);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (User item in lstUser)
                    {
                        sw.WriteLine(string.Format("{0},{1},{2}", item.ID, item.Pwd, item.Name));
                    }
                }
            }
        }

        /// <summary>
        /// 加载用户数据
        /// </summary>
        /// <param name="v"></param>
        /// <param name="lstUser"></param>
        public static void LoadData(string v, List<User> lstUser)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, v);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string strLine;
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        string[] arrInfo = strLine.Split(',');
                        lstUser.Add(new User() { ID = arrInfo[0], Pwd = arrInfo[1], Name = arrInfo[2] });
                    }
                }
            }
        }
    }
}
