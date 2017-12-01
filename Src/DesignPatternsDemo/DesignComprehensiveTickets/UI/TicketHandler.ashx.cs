using BLL;
using Model;
using Model.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace UI
{
    /// <summary>
    /// TicketHandler 的摘要说明
    /// </summary>
    public class TicketHandler : IHttpHandler
    {
        ITicketsMgr mgr = new TicketMgrProxy();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string methodName = context.Request.PathInfo.Substring(1);
            MethodInfo method = GetType().GetMethod(methodName);
            if (method != null)
            {
                method.Invoke(this, new object[] { context });
            }
            else
            {
                context.Response.Write("请检查请求URL是否正确:" + methodName);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void AddTickets(HttpContext context)
        {
            string ticketType = context.Request["ticketType"];
            string beginning = context.Request["beginning"];
            string destination = context.Request["destination"];

            Tickets ticket = SimpleTicketFactory.CreateTicket(ticketType, beginning, destination);
            ticket.Remainder = int.Parse(context.Request["count"]);

            if (mgr.Add(ticket))
            {
                context.Response.Write("{\"status\":true}");
            }
            else
            {
                context.Response.Write("{\"status\":false}");
            }
        }

        public void GetTickets(HttpContext context)
        {
            string ticketType = context.Request["ticketType"];
            List<Tickets> list = mgr.GetTickets(ticketType);

            //拼接json字符串
            StringBuilder builder = new StringBuilder("[");
            foreach (Tickets item in list)
            {
                builder.Append("{");
                builder.AppendFormat("\"ID\":{0},\"TicketType\":\"{1}\",\"Remainder\":{2},\"Beginning\":\"{3}\",\"Destination\":\"{4}\"",
                    item.ID, item.TicketType, item.Remainder, item.Beginning, item.Destination);
                builder.Append("},");
            }

            context.Response.Write(builder.ToString().Substring(0, builder.ToString().Length - 1) + "]");
        }

        public void BuyTickets(HttpContext context)
        {
            string ticketType = context.Request["ticketType"];
            string beginning = context.Request["beginning"];
            string destination = context.Request["destination"];

            Tickets ticket = SimpleTicketFactory.CreateTicket(ticketType, beginning, destination);
            ticket.ID = int.Parse(context.Request["id"]);

            List<string> lstSeats = mgr.BuyTicket(ticket, int.Parse(context.Request["count"]));

            if (lstSeats != null)
            {
                StringBuilder builder = new StringBuilder("[");
                foreach (string item in lstSeats)
                {
                    builder.Append("{\"seat\":\"" + item + "\"},");
                }
                context.Response.Write(builder.ToString().Substring(0, builder.ToString().Length - 1) + "]");
            }
            else
            {
                context.Response.Write("[]");
            }
        }
    }
}