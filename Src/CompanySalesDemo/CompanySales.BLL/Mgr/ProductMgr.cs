using CompanySales.DAL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Model.Parameter;

namespace CompanySales.BLL
{
    public class ProductMgr
    {
        public static List<Product> GetList()
        {
            return ProductDAO.GetList();
        }

        public static bool AddProduct(Product entity)
        {
            // 新产品入库的时候，产品已销售数量均为0
            entity.ProductSellNumber = 0;
            return ProductDAO.AddProduct(entity);
        }

        /// <summary>
        /// 根据产品编号检查是否存在该产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns>存在则返回true，不存在则返回false</returns>
        public static bool CheckProductById(int id)
        {
            Product result = ProductDAO.GetProductById(id);
            return result != null;
        }

        public static Product GetProductById(int id)
        {
            return ProductDAO.GetProductById(id);
        }

        public static bool DeleteById(int id)
        {
            return ProductDAO.DeleteById(id);
        }

        public static bool Update(Product entity)
        {
            return ProductDAO.Update(entity);
        }

        public static Pager<Product> GetListByPage(ProductParameter parameter)
        {
            return ProductDAO.GetListByPage(parameter);
        }

        public static bool BatchDelete(string ids)
        {
            string[] array = ids.Split(',');
            return ProductDAO.BatchDelete(array);
        }
    }
}
