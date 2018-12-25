using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoSales.Model.PO;
using CoSales.Model.DomainModel;
using Dapper;
using CoSales.Model;

namespace CoSales.DAL
{
    public class CustomerDAO
    {
        public static readonly CustomerDAO DAO = new CustomerDAO();

        public List<Customer> GetList()
        {
            return DapperHelper.GetList<Customer>().ToList();
        }
    }
}
