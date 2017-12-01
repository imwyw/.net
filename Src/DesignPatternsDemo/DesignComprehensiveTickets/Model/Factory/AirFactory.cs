using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Factory
{
    /// <summary>
    /// 机票工厂
    /// </summary>
    public class AirFactory : TicketFactory
    {
        public override Tickets CreateTicket(string beginning, string destination)
        {
            return new AirTicket(beginning, destination);
        }
    }
}
