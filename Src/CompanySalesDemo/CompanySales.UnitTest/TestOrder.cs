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
    public class TestOrder
    {
        [TestMethod]
        public void TestSellOrderList()
        {
            SellOrderParam parameter = new SellOrderParam();
            parameter.PageSize = 10;
            parameter.ProductName = "笔";

            var res = BLL.OrderMgr.GetSellOrderByPage(parameter);

            Assert.IsNotNull(res);
        }
    }
}
