using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.UnitTest
{
    [TestClass]
    public class TestDepartment
    {
        [TestMethod]
        public void TestGetDeptEmpInfo()
        {
            var pr = new Model.Parameter.DeptEmpParameter();
            //pr.IsPage = false;

            var res = BLL.DepartmentMgr.GetDeptEmpInfo(pr);
            Assert.IsTrue(res.Total > 0);
        }
    }
}
