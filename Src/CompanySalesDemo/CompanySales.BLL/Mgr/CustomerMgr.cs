using CompanySales.DAL;
using CompanySales.Model.Entity;using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.BLL
{
    public class CustomerMgr
    {
        public static bool Add(Customer entity)
        {
            return CustomerDAO.Add(entity);
        }

        public static DataTable GetCustomerData()
        {
            return CustomerDAO.GetCustomerData();
        }

        public static bool Delete(int id)
        {
            return CustomerDAO.Delete(id);
        }

        public static bool Update(Customer entity)
        {
            return CustomerDAO.Update(entity);
        }

        public static Customer GetCustomerById(int id)
        {
            return CustomerDAO.GetCustomerById(id);
        }
    }
}
