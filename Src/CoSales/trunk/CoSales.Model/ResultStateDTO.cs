using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model
{
    public class ResultStateDTO
    {
        public ResultStateDTO() { }
        public ResultStateDTO(bool status, string msg = "")
        {
            Status = status;
            Message = msg;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
