using IflySdk;
using IflySdk.Enum;
using IflySdk.Model.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IflytekPlatformDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ASRAudio();
            Console.ReadKey(false);
        }

        /// <summary>
        /// 一次识别一个完整的音频文件
        /// </summary>
        static async void ASRAudio()
        {
            string path = @"test.wav";  //测试文件路径,自己修改
            byte[] data = File.ReadAllBytes(path);

            try
            {
                ASRApi iat = new ApiBuilder()
                    .WithAppSettings(new AppSettings()
                    {
                        ApiKey = "d30c6f020d796790e48585f2f9acb0c8",
                        ApiSecret = "812685bb2fef1303cd4b5e91f5b55373",
                        AppID = "5dee66f1"
                    })
                    .UseError((sender, e) =>
                    {
                        Console.WriteLine("错误：" + e.Message);
                    })
                    .UseMessage((sender, e) =>
                    {
                        Console.WriteLine("实时结果：" + e);
                    })
                    .BuildASR();

                //计算识别时间
                Stopwatch sw = new Stopwatch();
                sw.Start();

                ResultModel<string> result = await iat.ConvertAudio(data);
                if (result.Code == ResultCode.Success)
                {
                    Console.WriteLine("\n识别结果：" + result.Data);
                }
                else
                {
                    Console.WriteLine("\n识别错误：" + result.Message);
                }

                sw.Stop();
                Console.WriteLine($"总共花费{Math.Round(sw.Elapsed.TotalSeconds, 2)}秒。");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
