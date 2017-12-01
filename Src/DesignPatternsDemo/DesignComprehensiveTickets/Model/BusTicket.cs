using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BusTicket : Tickets
    {
        public BusTicket()
        {
        }

        public BusTicket(string begin, string destination) : base(begin, destination)
        {
            TicketType = "汽车票";
        }

        public override List<string> GetSeats(int cnt)
        {
            throw new NotImplementedException();
        }
    }
}
