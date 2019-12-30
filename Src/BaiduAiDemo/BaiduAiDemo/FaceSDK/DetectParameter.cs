using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduAiDemo.FaceSDK
{
    /// <summary>
    /// 人脸检测参数
    /// </summary>
    public class DetectParameter
    {
        /// <summary>
        /// 图片信息(总数据大小应小于10M)，图片上传方式根据image_type来判断
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 图片类型
        /// BASE64:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M；
        /// URL:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)；
        /// FACE_TOKEN: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。
        /// </summary>
        public string image_type { get; set; }
        /// <summary>
        /// 包括
        /// age,beauty,expression,face_shape,gender,glasses,landmark,landmark150,race,quality,eye_status,emotion,face_type
        /// 逗号分隔. 默认只返回face_token、人脸框、概率和旋转角度
        /// </summary>
        public string face_field { get; set; }
    }
}
