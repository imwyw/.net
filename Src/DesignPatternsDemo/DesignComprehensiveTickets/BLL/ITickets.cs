using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ITicketsMgr
    {
        bool Add(Tickets ticket);
        List<Tickets> GetTickets(string ticketType);
        List<string> BuyTicket(Tickets ticket, int count);
    }
}
