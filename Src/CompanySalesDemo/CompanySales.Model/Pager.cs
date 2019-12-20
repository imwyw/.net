using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model
{
    public class Pager<T> where T : new()
    {
        public List<T> Rows { get; set; }
        public int Total { get; set; }
    }
}
