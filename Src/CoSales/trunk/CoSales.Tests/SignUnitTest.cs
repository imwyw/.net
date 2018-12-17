using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoSales.Model.PO;
using CoSales.BLL;

namespace CoSales.Tests
{
    /// <summary>
    /// SignUnitTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SignUnitTest
    {
        public SignUnitTest()
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
        public void TestMethod1()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        [TestMethod]
        public void TestAddUser()
        {
            User entity = new User();
            entity.UserID = "admin";
            entity.Password = "123";
            entity.UserName = "jack";
            entity.BirthDate = new DateTime(2000, 1, 1);
            entity.Gender = "M";
            entity.Remark = "单元测试添加";

            var res = UserMgr.Mgr.Add(entity);
            Assert.IsTrue(res > 0);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            User entity = new User();
            entity.ID = 1;
            entity.UserID = "admin";
            entity.Password = "1";
            entity.UserName = "administrator";
            entity.BirthDate = new DateTime(2000, 1, 1);
            entity.Gender = "M";
            entity.Remark = "单元测试修改";
            entity.RoleID = 0;

            var res = UserMgr.Mgr.Update(entity);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestGetUserList()
        {
            var res = UserMgr.Mgr.GetList();
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestGetUserLogin()
        {
            string userid = "admin";
            string pwd = "1";
            var res = UserMgr.Mgr.GetUser(userid, pwd);
            Assert.IsNotNull(res);
        }
    }
}
