using CompanySales.Repository.Business;
using CompanySales.Repository.Parameter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySales.UnitTest
{
    [TestClass]
   public class TestProduct
    {
        [TestMethod]
        public void TestGetByPage()
        {
            ProductParameter parameter = new ProductParameter();
            parameter.ProductName = "笔";
            var res = ProductBiz.GetListByPage(parameter);

        }
    }
}
