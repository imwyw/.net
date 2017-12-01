using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RailwayTicket : Tickets
    {
        Random rd = new Random();
        public RailwayTicket()
        {
        }

        public RailwayTicket(string begin, string destination) : base(begin, destination)
        {
            TicketType = "火车票";
        }

        public override List<string> GetSeats(int cnt)
        {
            if (TakeOffTicket(cnt))
            {
                List<string> list = new List<string>();
                for (int i = 0; i < cnt; i++)
                {
                    list.Add(string.Format("{0}车厢{1}座", rd.Next(1, 15), rd.Next(1, 119)));
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
