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
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
