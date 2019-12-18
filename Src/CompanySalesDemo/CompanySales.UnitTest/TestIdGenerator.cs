using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestIdGenerator
    {
        [TestMethod]
        public void TestId()
        {
            List<long> idList = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                idList.Add(Common.IdGenerator.GeneratorInt64());
            }

            Assert.IsTrue(idList.Count > 0);
        }
    }
}
