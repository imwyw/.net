using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model
{
    /// <summary>
    /// 标识后端返回给前端的对象
    /// 
    /// </summary>
    public class StateModel
    {
        public StateModel() { }
        public StateModel(bool s, string msg = "")
        {
            Status = s;
            Message = msg;
        }
        /// <summary>
        /// 标记操作成功与否
        /// </summary>
        public bool Status { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 用于数据返回，定义为dynamic动态类型
        /// </summary>
        public dynamic Data { get; set; }
        /// <summary>
        /// 返回状态码，在layui组件的使用中有用
        /// </summary>
        public int Code { get; set; }
    }
}
