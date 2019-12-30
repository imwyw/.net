using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaiduAiDemo.FaceSDK;

namespace BaiduAiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] imgs = { "mayun.jpg", "zhangxueyou.jpg", "nezha.jpg" };
            string path = $@".\images\{imgs[2]}";

            string result = FaceDetect.Detect(path);

            JObject ro = JsonConvert.DeserializeObject<JObject>(result);
            // 解析的第一个人脸信息
            JToken faceObj = ro["result"]["face_list"][0];

            Console.WriteLine("=================================");
            Console.WriteLine($"年龄：{faceObj["age"]}");
            Console.WriteLine($"美丑打分,范围0-100：{faceObj["beauty"]}");
            Console.WriteLine($"情绪：{faceObj["emotion"]}");
            Console.WriteLine($"表情：{faceObj["expression"]}");
            Console.WriteLine($"真实人脸/卡通人脸：{faceObj["face_type"]}");
            Console.WriteLine($"性别：{faceObj["gender"]}");
            Console.WriteLine($"是否戴眼镜：{faceObj["glasses"]}");

            Console.ReadKey(false);
        }
    }
}
