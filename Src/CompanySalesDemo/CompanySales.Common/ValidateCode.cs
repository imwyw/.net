using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Common
{
    /// <summary>
    /// 验证码生成类
    /// 综合整理了以下博客
    /// https://my.oschina.net/xiangyisheng/blog/848573
    /// http://www.cnblogs.com/rushme/p/6597629.html
    /// http://blog.csdn.net/mr_wanter/article/details/50475684
    /// </summary>
    public class ValidateCode
    {
        static Random rd = new Random();

        /// <summary>
        /// 验证码字符的字典值
        /// </summary>
        static readonly char[] DICTIONARY =  { 'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y','Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g',
            'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',
            '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        // <summary>
        /// 创建验证码字符
        /// 利用伪随机数生成器生成验证码字符串。
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        public static string CreateVerificationText(int length)
        {
            char[] codes = new char[length];
            for (int i = 0; i < length; i++)
            {
                codes[i] = DICTIONARY[rd.Next(DICTIONARY.Length - 1)];
            }
            return new string(codes);
        }

        /// <summary>
        /// 创建验证码图片
        /// 思路是使用GDI+创建画布，使用伪随机数生成器生成渐变画刷，然后创建渐变文字。
        /// </summary>
        /// <param name="verificationText">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns>图片</returns>
        public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
        {
            Pen pen = new Pen(Color.Black);
            Font font = new Font("Arial", 14, FontStyle.Bold);
            Brush brush = null;
            Bitmap bmp = new Bitmap(width, height);
            Graphics graph = Graphics.FromImage(bmp);
            SizeF totalSizeF = graph.MeasureString(verificationText, font);
            SizeF curCharSizeF;

            PointF startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
            graph.Clear(Color.White);

            //画图片的干扰线  
            for (int i = 0; i < 25; i++)
            {
                int x1 = rd.Next(width);
                int x2 = rd.Next(width);
                int y1 = rd.Next(height);
                int y2 = rd.Next(height);
                graph.DrawLine(new Pen(Color.FromArgb(rd.Next())), x1, y1, x2, y2);
            }

            //画图片的前景干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = rd.Next(width);
                int y = rd.Next(height);
                bmp.SetPixel(x, y, Color.FromArgb(rd.Next()));
            }

            //画字符
            for (int i = 0; i < verificationText.Length; i++)
            {
                brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(rd.Next(255), rd.Next(255), rd.Next(255)), Color.FromArgb(rd.Next(255), rd.Next(255), rd.Next(255)));
                graph.DrawString(verificationText[i].ToString(), font, brush, startPointF);
                curCharSizeF = graph.MeasureString(verificationText[i].ToString(), font);
                startPointF.X += curCharSizeF.Width;
            }
            graph.Dispose();
            return bmp;
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        public static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = Math.PI * 2;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }

        public static byte[] ConvertImg2Byte(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);

                //输出图片流
                return ms.ToArray();
            }
        }

        public static byte[] CreateImage(string verificationText, int width, int height)
        {
            Bitmap bmp = CreateVerificationImage(verificationText, width, height);
            Bitmap twistBmp = TwistImage(bmp, true, rd.Next(1, 4), rd.Next(1, 5));

            return ConvertImg2Byte(twistBmp);
        }

        /// <summary>
        /// 验证码转换图片对应的base64编码
        /// 可用于前端直接显示，预留方法，暂未使用
        /// </summary>
        /// <param name="verificationText"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string CreateImageVerifyBase64(string verificationText, int width, int height)
        {
            Bitmap bmp = CreateVerificationImage(verificationText, width, height);
            Bitmap twistBmp = TwistImage(bmp, true, rd.Next(1, 4), rd.Next(1, 5));

            return Img2Base64View(twistBmp);
        }

        /// <summary>
        /// Bitmap转为base64编码的文本
        /// 用于前端直接显示
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static string Img2Base64View(Bitmap bmp)
        {
            string res = ImgToBase64String(bmp);
            return $"data:image/jpg;base64,{res}";
        }

        /// <summary>
        /// Bitmap转为base64编码的文本
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private static string ImgToBase64String(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                //ImgToBase64String 转换失败\nException:" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// base64编码的文本转为Bitmap
        /// </summary>
        /// <param name="txtBase64"></param>
        /// <returns></returns>
        private static Bitmap Base64StringToImage(string txtBase64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(txtBase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch
            {
                //Base64StringToImage 转换失败\nException：" + ex.Message);
                return null;
            }
        }

    }
}
