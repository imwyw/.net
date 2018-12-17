using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model
{
    public class ResutlStateDTO
    {
        public ResutlStateDTO() { }
        public ResutlStateDTO(bool status, string msg = "")
        {
            Status = status;
            Message = msg;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
