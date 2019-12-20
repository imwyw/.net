using CompanySales.Model.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestCustomer
    {
        [TestMethod]
        public void TestAdd()
        {
            Customer entity = new Customer();
            entity.CustomerId = 1000;
            entity.CompanyName = "中国移动安徽省分公司";
            entity.ContactName = "刘经理";
            entity.EmailAddress = "abc@123.com";
            entity.Phone = "12312341234";
            entity.Address = "合肥市高新区软件园";

            var res = BLL.CustomerMgr.Add(entity);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestGetCustomerData()
        {
            var res = BLL.CustomerMgr.GetCustomerData();
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Customer entity = new Customer();
            entity.CustomerId = 1000;
            entity.CompanyName = "中国联通安徽省分公司";
            entity.ContactName = "张经理";
            entity.EmailAddress = "abc@666.com";
            entity.Phone = "18812341234";
            entity.Address = "合肥市动漫基地";

            var res = BLL.CustomerMgr.Update(entity);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestGetCustomerById()
        {
            var res = BLL.CustomerMgr.GetCustomerById(1000);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestDelete()
        {
            var res = BLL.CustomerMgr.Delete(1000);
            Assert.IsTrue(res);
        }
    }
}
