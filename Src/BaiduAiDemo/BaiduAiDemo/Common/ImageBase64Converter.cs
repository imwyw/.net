using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduAiDemo.Common
{
    public class ImageBase64Converter
    {
        /// <summary>
        /// 将图片转换为BASE64编码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ImageToBase64(string path)
        {
            try
            {
                Bitmap bmp = new Bitmap(path);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                //ImgToBase64String 转换失败\nException:" + ex.Message);
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

    }
}
