using ArticleDemo.BLL;
using ArticleDemo.Model;
using ArticleDemo.UnitTest.UnitHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace ArticleDemo.UnitTest
{
    [TestFixture]
    public class MenuTest
    {
        /// <summary>
        /// 获取当前版本的全量菜单
        /// </summary>
        [Test]
        public void TestMenuList()
        {
            var res = MenuActionMgr.GetMenuList();
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// 获取所有权限菜单，包含权限ID和名称
        /// </summary>
        [Test]
        public void TestMenuListRoles()
        {
            var res = MenuActionMgr.GetMenuListRoles();
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// 当前用户可访问的权限
        /// </summary>
        [Test]
        public void GetMenuListSelf()
        {
            var mgr = new UserMgr();

            MockSession.NewHttpContextBase();

            ContextObjects.CurrentUser = mgr.Login("admin", "1");

            var res = MenuActionMgr.GetMenuListSelf();
            Assert.IsNotNull(res);
        }
    }
}
