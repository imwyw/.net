using System;
using System.Collections.Generic;
using System.Text;

namespace IflySdk.Model.TTS
{
    class TTSResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TTSResultData Data { get; set; }
    }
}
