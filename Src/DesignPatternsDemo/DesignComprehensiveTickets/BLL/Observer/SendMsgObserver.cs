using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace BLL.Observer
{
    /// <summary>
    /// 观察者 短信发送
    /// </summary>
    public class SendMsgObserver : IBuyObserver
    {
        public void Update(Tickets ticket, int count)
        {
            //模拟发送短信，在这里写日志代替处理
            string msg = string.Format("尊敬的先生/女士，您已购{0}-{1} {2} {3}张", ticket.Beginning, ticket.Destination, ticket.TicketType, count);
            LogHelper.GetInstance().Log("发送短信", msg);
        }
    }
}
