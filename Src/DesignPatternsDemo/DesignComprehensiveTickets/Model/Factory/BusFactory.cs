using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Factory
{
    /// <summary>
    /// 汽车票工厂
    /// </summary>
    public class BusFactory : TicketFactory
    {
        public override Tickets CreateTicket(string beginning, string destination)
        {
            return new BusTicket(beginning, destination);
        }
    }
}
