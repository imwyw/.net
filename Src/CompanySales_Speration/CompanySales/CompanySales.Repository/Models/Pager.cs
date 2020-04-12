using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySales.Repository.Models
{
    public class Pager<T> where T : new()
    {
        public List<T> Rows { get; set; }
        public int Total { get; set; }
    }
}
