using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Factory
{
    /// <summary>
    /// 简单工厂的应用
    /// </summary>
    public static class SimpleTicketFactory
    {
        /// <summary>
        /// 将外部参数封装到简单工厂中
        /// </summary>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        public static TicketFactory CreateTicketFactory(string ticketType)
        {
            TicketFactory factory;
            switch (ticketType)
            {
                case "飞机票":
                    factory = new AirFactory();
                    break;
                case "火车票":
                    factory = new RailwayFactory();
                    break;
                case "汽车票":
                    factory = new BusFactory();
                    break;
                default:
                    return null;
            }
            return factory;
        }

        public static Type CreateTicketType(string ticketType)
        {
            switch (ticketType)
            {
                case "飞机票":
                    return typeof(AirTicket);
                case "火车票":
                    return typeof(RailwayTicket);
                case "汽车票":
                    return typeof(BusTicket);
                default:
                    return null;
            }
        }

        public static Tickets CreateTicket(string ticketType, string beginning, string destination)
        {
            TicketFactory factory = CreateTicketFactory(ticketType);
            return factory.CreateTicket(beginning, destination);
        }
        
    }
}
