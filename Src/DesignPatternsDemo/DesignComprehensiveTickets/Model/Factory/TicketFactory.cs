using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Factory
{
    /// <summary>
    /// 票-抽象工厂
    /// </summary>
    public abstract class TicketFactory
    {
        public abstract Tickets CreateTicket(string beginning, string destination);
    }
}
