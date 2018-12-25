using CoSales.DAL;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.BLL
{
    public class CustomerMgr
    {
        public static readonly CustomerMgr Mgr = new CustomerMgr();

        public List<Customer> GetList()
        {
            var res = CustomerDAO.DAO.GetList();
            return res;
        }
    }
}
