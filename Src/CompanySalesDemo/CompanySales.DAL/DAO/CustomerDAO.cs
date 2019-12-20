using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Common;

namespace CompanySales.DAL
{
    public class CustomerDAO
    {
        public static bool Add(Customer entity)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Customer.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public static DataTable GetCustomerData()
        {
            string sql = @"
SELECT CustomerID ,
       CompanyName ,
       ContactName ,
       Phone ,
       Address ,
       EmailAddress FROM dbo.Customer";
            DataTable dt = EFUtility.GetDataTableBySql(sql, null);
            return dt;
        }

        public static bool Delete(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Customer.Where(t => t.CustomerId.Equals(id)).First();
                db.Customer.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        public static bool Update(Customer entity)
        {
            using (SaleContext db = new SaleContext())
            {
                var dbEntity = db.Customer.Where(t => t.CustomerId == entity.CustomerId).First();
                if (null != dbEntity)
                {
                    dbEntity.CompanyName = entity.CompanyName;
                    dbEntity.ContactName = entity.ContactName;
                    dbEntity.Address = entity.Address;
                    dbEntity.EmailAddress = entity.EmailAddress;
                    dbEntity.Phone = entity.Phone;

                    db.SaveChanges();
                    return true;
                }
                else
                {
                    Log4Helper.InfoLog.Warn("未找到匹配的customer id:" + entity.CustomerId);
                    return false;
                }
            }
        }

        public static Customer GetCustomerById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Customer.Find(id);
                return entity;
            }
        }
    }
}
