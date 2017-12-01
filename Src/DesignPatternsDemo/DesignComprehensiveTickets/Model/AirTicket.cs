using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AirTicket : Tickets
    {
        string[] seats = { "A", "B", "C", "J", "K", "L" };
        Random rd = new Random();

        public AirTicket()
        {
        }

        public AirTicket(string begin, string destination) : base(begin, destination)
        {
            TicketType = "飞机票";
        }

        public override List<string> GetSeats(int cnt)
        {
            if (TakeOffTicket(cnt))
            {
                List<string> list = new List<string>();
                for (int i = 0; i < cnt; i++)
                {
                    list.Add(rd.Next(1, 21).ToString() + seats[rd.Next(0, 6)]);
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
