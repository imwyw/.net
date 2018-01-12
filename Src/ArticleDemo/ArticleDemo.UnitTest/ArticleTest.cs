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
    /// /// TestFixture 单元测试标签，表明该类包含测试方法，类必须声明为public
    /// </summary>
    [TestFixture]
    public class ArticleTest
    {
        /// <summary>
        /// 针对EF方法的单元测试
        /// app.config中需要添加connectionStrings节点
        /// 单元测试项目需要引用 EntityFramework
        /// </summary>
        [Test]
        public void TestGetArticlesEF()
        {
            var res = ArticleMgr.GetArticlesEF(-1, "");
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// 添加测试数据
        /// </summary>
        [Test]
        public void TestAddArticle()
        {
            string[] arrContent = { "学数字设计的软件工程师该了解的时钟知识",
            "软件开发工作的第一现场",
            "十个不错的 Linux 网络监视工具",
            "用系统日志了解你的 Linux 系统",
            "PostgreSQL 查询成本模型",
            "浅谈重构中踩过的坑",
            "客户想要的 vs 客户实际预算：漫画解读软件开发模式 ​​​​"};
            int[] arrCate = { 0, 2, 6, 7 };
            Random rd = new Random();

            for (int i = 0; i < 10000; i++)
            {
                Article obj = new Article();
                obj.Cate_id = arrCate[rd.Next() % arrCate.Length];
                int index = rd.Next() % arrContent.Length;
                obj.Title = arrContent[index] + index;
                obj.Content = string.Format("时间：{0}\t内容：{1}", DateTime.Now.ToString(), arrContent[index]);
                obj.Update_Time = DateTime.Now;
                obj.Create_User = 6;

                //var res = ArticleMgr.Add(obj);
            }

        }

        [Test]
        public void TestGetArticleByPager()
        {
            PageParam pp = new PageParam() { PageIndex = 2, PageSize = 10 };
            var res = ArticleMgr.GetArticlesByPager(-1, "linux", pp);
            Assert.IsTrue(res.Total > 0);
        }
    }
}
