using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestProduct
    {
        [TestMethod]
        public void TestGetList()
        {
            List<Product> list = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var entity = new Bogus.Faker<Product>("zh_CN")
                    .RuleFor(t => t.ProductID, f => f.Random.Int())
                    .RuleFor(t => t.ProductName, f => f.Commerce.ProductName())
                    .RuleFor(t => t.ProductStockNumber, f => f.Random.Int(0, 10000))
                    .RuleFor(t => t.Price, f => f.Finance.Random.Decimal(0, 1000));

                list.Add(entity);
            }


            var res = BLL.ProductMgr.GetList();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestAddProduct()
        {
            Product entity = new Product();
            entity.ProductID = 1001;
            entity.ProductName = "西瓜";
            entity.Price = 20;
            entity.ProductStockNumber = 100;

            var res = BLL.ProductMgr.AddProduct(entity);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestGetProductById()
        {
            var res = BLL.ProductMgr.GetProductById(1000);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Product entity = new Product();
            entity.ProductID = 1000;
            entity.ProductName = "哈密瓜";
            entity.Price = 30;
            entity.ProductStockNumber = 200;

            var res = BLL.ProductMgr.Update(entity);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestGetListByPage()
        {
            // 查询第2页数据
            ProductParameter parameter = new ProductParameter();
            parameter.PageIndex = 2;
            parameter.PageSize = 10;

            var res = BLL.ProductMgr.GetListByPage(parameter);
            Assert.IsTrue(res.Total > 0 && res.Rows != null);
        }

        [TestMethod]
        public void TestDeleteById()
        {
            var res = BLL.ProductMgr.DeleteById(1000);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestBatchDelete()
        {
            var res = BLL.ProductMgr.BatchDelete("1000,1001,1002");
            Assert.IsTrue(res);
        }
    }
}
