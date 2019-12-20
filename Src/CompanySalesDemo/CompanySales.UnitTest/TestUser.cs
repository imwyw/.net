using CompanySales.Model.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void TestLogin()
        {
            string uid = "admin";
            string pwd = "123";

            var res = BLL.UserMgr.Login(uid, pwd);

            // 单元测试中 断言判断
            //Assert.IsTrue(res != null);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TestAddUser()
        {
            User entity = new User();
            entity.UserId = "a";
            entity.Name = "aaa";
            entity.Password = "123";
            entity.Address = "文津西路8号";
            entity.Roles = "admin";

            bool res = BLL.UserMgr.AddUser(entity);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestAddBatchUser()
        {
            List<User> list = new List<User>();
            for (int i = 0; i < 1000; i++)
            {
                var entity = new Bogus.Faker<User>("zh_CN")
                    .RuleFor(t => t.ID, f => f.Random.Int())
                    .RuleFor(t => t.UserId, f => f.Person.UserName)
                    .RuleFor(t => t.Name, f => f.Person.FullName)
                    .RuleFor(t => t.Password, f => f.Person.Email)
                    .RuleFor(t => t.Address, f => f.Person.Address.ToString());
                list.Add(entity);
            }

            var res = BLL.UserMgr.AddBatchUser(list);
            Assert.IsTrue(res);
        }
    }
}
