using IflySdk.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IflySdk.Model.Common
{
    public class AppSettings
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// ApiSecret
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// ASR接口地址
        /// 语音听写（流式版）
        /// 语音听写流式接口，用于1分钟内的即时语音转文字技术，支持实时返回识别结果，达到一边上传音频一边获得识别文本的效果。
        /// </summary>
        public string ASRUrl { get; set; } = "wss://iat-api.xfyun.cn/v2/iat";

        /// <summary>
        /// TTS接口地址
        /// 在线语音合成（流式版）
        /// </summary>
        public string TTSUrl { get; set; } = "wss://tts-api.xfyun.cn/v2/tts";

        public ApiType ApiType { get; set; }
    }
}
