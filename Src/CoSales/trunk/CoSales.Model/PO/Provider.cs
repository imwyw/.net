using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_PROVIDER")]
    public class Provider
    {
        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string ContactName { get; set; }
        public string ProviderAddress { get; set; }
        public string ProviderPhone { get; set; }
        public string ProviderEmail { get; set; }

    }
}
