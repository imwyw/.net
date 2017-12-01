using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Factory
{
    /// <summary>
    /// 火车票工厂
    /// </summary>
    public class RailwayFactory : TicketFactory
    {
        public override Tickets CreateTicket(string beginning, string destination)
        {
            return new RailwayTicket(beginning, destination);
        }
    }
}
