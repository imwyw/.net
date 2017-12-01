using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Reflection;
using Model.Factory;
using System.Collections;
using BLL.Observer;

namespace BLL
{
    public class TicketsMgr : ITicketsMgr
    {
        List<IBuyObserver> lstObs = new List<IBuyObserver>();
        public void AddObserver(IBuyObserver obs)
        {
            lstObs.Add(obs);
        }

        public void RemoveObserver(IBuyObserver obs)
        {
            lstObs.Remove(obs);
        }

        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="count"></param>
        private void NotifyObserver(Tickets ticket, int count)
        {
            foreach (var item in lstObs)
            {
                item.Update(ticket, count);
            }
        }

        public bool Add(Tickets ticket)
        {
            if (CheckExist(ticket.Beginning, ticket.Destination))
            {
                return Update(ticket);
            }
            else
            {
                return TicketsDao.Add(ticket) > 0;
            }
        }

        public bool CheckExist(string begin, string destination)
        {
            return TicketsDao.CheckExist(begin, destination) > 0;
        }

        public bool Update(Tickets ticket)
        {
            //Tickets ticDB = TicketsDao.GetSingleTicket(ticket.TicketType, ticket.Beginning, ticket.Destination);
            Type t = SimpleTicketFactory.CreateTicketType(ticket.TicketType);
            Tickets ticDB = (Tickets)typeof(TicketsDao).GetMethod("GetSingleTicketGeneric").MakeGenericMethod(t)
                .Invoke(null, new object[] { ticket.TicketType, ticket.Beginning, ticket.Destination });

            ticket.Remainder = ticDB.Remainder + ticket.Remainder;

            return TicketsDao.Update(ticket) > 0;
        }

        /// <summary>
        /// 反射调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        public List<T> GetTicketsGeneric<T>(string ticketType) where T : Tickets, new()
        {
            return TicketsDao.GetTickets<T>(ticketType);
        }

        public List<Tickets> GetTickets(string ticketType)
        {
            Type t = SimpleTicketFactory.CreateTicketType(ticketType);

            //反射调用泛型方法，由于具体泛型只能在运行时确定
            IList templist = (IList)typeof(TicketsMgr).GetMethod("GetTicketsGeneric").MakeGenericMethod(t).Invoke(this, new object[] { ticketType });

            List<Tickets> res = new List<Tickets>();
            //返回的是具体类，需要转换为抽象类
            foreach (var item in templist)
            {
                res.Add((Tickets)item);
            }

            return res;
        }

        public List<string> BuyTicket(Tickets ticket, int count)
        {
            Type t = SimpleTicketFactory.CreateTicketType(ticket.TicketType);

            //反射调用泛型方法，由于具体泛型只能在运行时确定
            object res = typeof(TicketsDao).GetMethod("GetSingleTicketByIDGeneric").MakeGenericMethod(t).Invoke(null, new object[] { ticket.ID });
            Tickets ticDb = (Tickets)res;

            if (ticDb.Remainder < count)
            {
                return null;
            }
            ticket.Remainder = ticDb.Remainder - count;
            TicketsDao.Update(ticket);
            NotifyObserver(ticket, count);
            return ticDb.GetSeats(count);
        }
    }
}
