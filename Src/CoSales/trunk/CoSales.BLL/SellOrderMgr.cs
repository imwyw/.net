using CoSales.DAL;
using CoSales.Model;
using CoSales.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.BLL
{
    public class SellOrderMgr
    {
        public static readonly SellOrderMgr Mgr = new SellOrderMgr();
        public ResultPager<SellOrderInfo> GetSellOrderInfo(SellOrderInfo param)
        {
            var res = SellOrderDAO.DAO.GetSellOrderInfo(param);
            return res;
        }
    }
}
