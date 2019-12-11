using CompanySales.DAL;
using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.BLL
{
    public class OrderMgr
    {
        public static Pager<SellOrderDomain> GetSellOrderByPage(SellOrderParam parameter)
        {
            return OrderDAO.GetSellOrderByPage(parameter);
        }
    }
}
