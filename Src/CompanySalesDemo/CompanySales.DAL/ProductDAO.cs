using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Common;

namespace CompanySales.DAL
{
    public class ProductDAO
    {
        public static List<Product> GetList()
        {
            using (SaleContext db = new SaleContext())
            {
                return db.Product.ToList();
            }
        }

        public static bool AddProduct(Product entity)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Product.Add(entity);
                db.SaveChanges();
                return true;
            }

        }

        /// <summary>
        /// 根据产品编号返回产品对象
        /// 如果不存在该产品则返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Product GetProductById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var res = db.Product.Find(id);
                return res;
            }
        }

        public static bool Update(Product entity)
        {
            using (SaleContext db = new SaleContext())
            {
                var dbEntity = db.Product.Find(entity.ProductID);
                if (null == dbEntity)
                {
                    Log4Helper.InfoLog.Warn("未找到该product id：" + entity.ProductID);
                    return false;
                }
                else
                {
                    dbEntity.Price = entity.Price;
                    dbEntity.ProductName = entity.ProductName;
                    dbEntity.ProductSellNumber = entity.ProductSellNumber;
                    //dbEntity.ProductStockNumber = entity.ProductStockNumber;
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public static Pager<Product> GetListByPage(int pageindex, int pagesize)
        {
            Pager<Product> result = new Pager<Product>();
            using (SaleContext db = new SaleContext())
            {
                result.Total = db.Product.Count();
                result.Rows = db.Product
                    .OrderBy(t => t.ProductID)
                    .Skip(pageindex * pagesize)
                    .Take(pagesize)
                    .ToList();
                return result;
            }
        }

        public static bool DeleteById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Product.Find(id);
                if (null != entity)
                {
                    db.Product.Remove(entity);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    Log4Helper.InfoLog.Warn("未找到该product id：" + id);
                    return false;
                }
            }
        }

        public static bool BatchDelete(string[] ids)
        {
            using (SaleContext db = new SaleContext())
            {
                var list = db.Product.Where(t => ids.Contains(t.ProductID.ToString()));
                db.Product.RemoveRange(list);
                db.SaveChanges();
                return true;
            }
        }
    }
}
