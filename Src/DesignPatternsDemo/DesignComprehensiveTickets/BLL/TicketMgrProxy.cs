using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;
using BLL.Observer;

namespace BLL
{
    /// <summary>
    /// 代理模式记录业务操作日志
    /// </summary>
    public class TicketMgrProxy : ITicketsMgr
    {
        TicketsMgr mgr;

        public TicketMgrProxy()
        {
            mgr = new TicketsMgr();
            mgr.AddObserver(new SendMsgObserver());
        }

        public bool Add(Tickets ticket)
        {
            LogHelper.GetInstance().Log("操作", "新增票");
            return mgr.Add(ticket);
        }

        public List<string> BuyTicket(Tickets ticket, int count)
        {
            LogHelper.GetInstance().Log("操作", "购票");
            return mgr.BuyTicket(ticket, count);
        }

        public List<Tickets> GetTickets(string ticketType)
        {
            LogHelper.GetInstance().Log("操作", "获取列表");
            return mgr.GetTickets(ticketType);
        }
    }
}
