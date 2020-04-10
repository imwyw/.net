using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySales.Model
{
    /// <summary>
    /// 和前端数据交互的封装
    /// </summary>
    public class ResultDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
