using ArticleDemo.BLL;
using ArticleDemo.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.UnitTest
{
    /// <summary>
    /// TestFixture 单元测试标签，表明该类包含测试方法，类必须声明为public
    /// </summary>
    [TestFixture]
    public class UserTest
    {
        /// <summary>
        /// Test 单元测试方法标签
        /// </summary>
        [Test]
        public void AddTest()
        {
            var mgr = new UserMgr();
            double res = mgr.Add(2.34, 4.32);
            Assert.IsTrue(res == 6.66);
        }

        /// <summary>
        /// 需要查询数据库的测试方法，项目内需要包含app.config文件配置数据库连接字符串
        /// </summary>
        [Test]
        public void LoginTest()
        {
            var mgr = new UserMgr();
            User res = mgr.Login("admin", "1");
            mgr.Login("admin", "C4CA4238A0B923820DCC509A6F75849B", true);
            Assert.IsNotNull(res);
        }

        [Test]
        public void TestCipherText()
        {
            var res = ArticleDemo.Common.EncryptHelper.MD5Encrypt("admin");

        }
    }
}
