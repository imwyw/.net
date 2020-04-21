﻿using CompanySales.Repository.Common;
using CompanySales.Repository.Models;
using CompanySales.Repository.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanySales.Repository.DAO
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
                var dbEntity = db.Product.Find(entity.ProductId);
                if (null == dbEntity)
                {
                    //Log4Helper.InfoLog.Warn("未找到该product id：" + entity.ProductId);
                    return false;
                }
                else
                {
                    dbEntity.Price = entity.Price;
                    dbEntity.ProductName = entity.ProductName;

                    // 更新时不更新库存和已销售数量
                    //dbEntity.ProductSellNumber = entity.ProductSellNumber;
                    //dbEntity.ProductStockNumber = entity.ProductStockNumber;
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public static Pager<Product> GetListByPage(ProductParameter parameter)
        {
            Pager<Product> result = new Pager<Product>();
            using (SaleContext db = new SaleContext())
            {
                var query = db.Product.AsQueryable();
                if (!string.IsNullOrEmpty(parameter.ProductName))
                {
                    query = query.Where(t => t.ProductName.Contains(parameter.ProductName));
                }

                Log4Helper.Info("DAO层中写入日志信息：" + parameter.ProductName);

                result.Total = query.Count();
                result.Rows = query.OrderBy(t => t.ProductId)
                    .Skip(parameter.Skip)
                    .Take(parameter.PageSize)
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
                    //Log4Helper.InfoLog.Warn("未找到该product id：" + id);
                    return false;
                }
            }
        }

        public static bool BatchDelete(string[] ids)
        {
            using (SaleContext db = new SaleContext())
            {
                var list = db.Product.Where(t => ids.Contains(t.ProductId.ToString()));
                db.Product.RemoveRange(list);
                db.SaveChanges();
                return true;
            }
        }
    }
}