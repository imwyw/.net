using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaiduAiDemo.FaceSDK
{
    /// <summary>
    /// 人脸检测
    /// </summary>
    public class FaceDetect
    {
        /// <summary>
        /// 人脸检测与属性分析
        /// 更多参数文档请查看： https://ai.baidu.com/ai-doc/FACE/yk37c1u4t
        /// </summary>
        /// <returns></returns>
        public static string Detect(string path)
        {
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/detect?access_token=" + AccessToken.TOKEN;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;

            DetectParameter parameter = new DetectParameter();
            parameter.image = Common.ImageBase64Converter.ImageToBase64(path);
            parameter.image_type = "BASE64";
            parameter.face_field = "age,beauty,expression,face_shape,gender,glasses,eye_status,emotion,face_type";
            string str = JsonConvert.SerializeObject(parameter);

            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("人脸检测与属性分析:");
            Console.WriteLine(result);
            return result;
        }
    }
}
