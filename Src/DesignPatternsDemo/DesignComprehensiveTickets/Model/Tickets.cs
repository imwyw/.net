using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 票类型抽象
    /// </summary>
    public abstract class Tickets
    {
        public Tickets() { }
        public Tickets(string begin, string destination)
        {
            Beginning = begin;
            Destination = destination;
            Remainder = 100;//默认100张票
        }

        public int ID { get; set; }

        public string TicketType { get; set; }

        public string Beginning { get; set; }

        public string Destination { get; set; }

        public int Remainder { get; set; }
        

        protected virtual bool TakeOffTicket(int cnt)
        {
            if (Remainder >= cnt)
            {
                Remainder -= cnt;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual List<string> GetSeats(int cnt)
        {
            return null;
        }

    }
}
