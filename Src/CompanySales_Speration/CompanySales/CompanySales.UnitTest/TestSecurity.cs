using CompanySales.Repository.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestSecurity
    {
        [TestMethod]
        public void TestLogin()
        {
            string uid = "admin";
            string pwd = "1234";

            var res = UserBiz.Login(uid, pwd);

        }
    }
}
