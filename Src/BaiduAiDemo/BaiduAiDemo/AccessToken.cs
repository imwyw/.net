using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaiduAiDemo
{
    /// <summary>
    /// 调用API时必须在URL中带上access_token参数
    /// </summary>
    public static class AccessToken
    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例，需要重新设置
        public static string TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static string clientId = "8il5QEPhILT5YAacBytaD4M4";
        // 百度云中开通对应服务应用的 Secret Key
        private static string clientSecret = "oRpnOtGAtNfjuxqhRHOmCZgUPdIRS5Dh";

        /// <summary>
        /// 静态构造，调用时，直接使用TOKEN即可
        /// </summary>
        static AccessToken()
        {
            RefreshToken();
        }

        /// <summary>
        /// 根据apikey和secret获取access token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            string authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<string, string>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// 刷新TOKEN值
        /// </summary>
        public static void RefreshToken()
        {
            var jsonStr = GetAccessToken();
            JObject res = JsonConvert.DeserializeObject<JObject>(jsonStr);
            TOKEN = res["access_token"].ToString();
        }
    }
}
