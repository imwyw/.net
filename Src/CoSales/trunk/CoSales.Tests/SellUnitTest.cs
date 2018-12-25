using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoSales.BLL;
using CoSales.Model.DomainModel;
using CoSales.Model.PO;

namespace CoSales.Tests
{
    /// <summary>
    /// SellUnitTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SellUnitTest
    {
        public SellUnitTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetSellOrderInfo()
        {
            SellOrderInfo param = new SellOrderInfo();
            //param.EmployeeName = "王";
            param.page = 1;
            param.limit = 10;
            var res = SellOrderMgr.Mgr.GetSellOrderInfo(param);
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// 添加销售记录
        /// </summary>
        [TestMethod]
        public void TestInsertSellOrder()
        {
            SellOrder entity = new SellOrder();
            entity.ProductID = 1;
            entity.SellOrderNumber = 1000;
            entity.EmployeeID = 1;
            entity.CustomerID = 1;
            entity.SellOrderDate = new DateTime(2018, 12, 1);

            var res = SellOrderMgr.Mgr.Insert(entity);
            Assert.IsTrue(res);
        }

        /// <summary>
        /// 批量添加销售订单测试数据
        /// </summary>
        [TestMethod]
        public void TestAddSellOrder()
        {
            Random rd = new Random();
            var listPds = ProductMgr.Mgr.GetList();
            var listEmp = EmployeeMgr.Mgr.GetList();
            var listCus = CustomerMgr.Mgr.GetList();

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int curMonth = month;

            // 循环每年
            for (int i = 0; i < 15; i++)
            {
                // 循环每个月
                while (curMonth > 0)
                {
                    int daysInMonth = DateTime.DaysInMonth(year - i, curMonth);

                    // 每个月插入 100条 测试数据
                    for (int j = 0; j < 100; j++)
                    {
                        int rVal = rd.Next(1, 100);
                        SellOrder entity = new SellOrder();
                        entity.ProductID = listPds.ToArray()[rVal % listPds.Count].ID;
                        entity.SellOrderNumber = rd.Next(1, 100) * 10;
                        entity.EmployeeID = listEmp.ToArray()[rVal % listEmp.Count].ID;
                        entity.CustomerID = listCus.ToArray()[rVal % listCus.Count].ID;
                        entity.SellOrderDate = new DateTime(year - i, curMonth, rd.Next(1, daysInMonth + 1));

                        var res = SellOrderMgr.Mgr.Insert(entity);
                    }
                    curMonth--;
                }
                curMonth = 12;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestGetEmployeeSellStat()
        {
            var res = SellOrderMgr.Mgr.GetEmployeeSellStat(2004);
            Assert.IsNotNull(res);
        }
    }
}
