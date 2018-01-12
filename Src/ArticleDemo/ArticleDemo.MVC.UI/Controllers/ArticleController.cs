using ArticleDemo.BLL;
using ArticleDemo.Model;
using ArticleDemo.MVC.UI.Core;
using ArticleDemo.MVC.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleDemo.MVC.UI.Controllers
{
    //[CustomActionFilter(Roles = "admin")]
    public class ArticleController : BaseController
    {
        #region Views
        /// <summary>
        /// 查看文章页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticlesView()
        {
            List<Article> list = ArticleMgr.GetArticles();
            ViewBag.ArticleList = list;
            return View();
        }

        /// <summary>
        /// 编辑/添加文章 页面复用
        /// 形参 (int? id) 表明id可以为null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditArticleView(Nullable<int> id)
        {
            //public ActionResult EditArticleView(int? id) 和上面的方法签名等效
            //int?和Nullable<int> 表示值类型参数可以为 null 值

            //不使用强类型传值
            //Article res = ArticleMgr.GetArticleByID(id);
            //return View(res);

            // 修改为 将id传到前端，使用ajax拉取数据 modified at 2017-12-13
            ViewBag.ArticleID = id;
            return View();
        }

        /// <summary>
        /// 文章分页查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticlesViewPager()
        {
            return View();
        }
        #endregion

        #region Datas
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetArticles()
        {
            List<Article> list = ArticleMgr.GetArticles();

            //默认的序列化，时间格式会有问题
            //return Json(list);

            //string jsonStr = JsonConvert.SerializeObject(list, Config.FULL_DATE_FORMAT);
            //return Json(jsonStr);

            //使用layui table的方式
            LayUITableModel res = new LayUITableModel(list.Count, list);
            return Json(res);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="title"></param>
        /// <param name="cateid"></param>
        /// <returns></returns>
        public JsonResult GetArticleByPager(PageParam pp, string title, int cateid = -1)
        {
            var res = ArticleMgr.GetArticlesByPager(cateid, title, pp);
            LayUITableModel model = new LayUITableModel(res.Total, res.Rows);
            return Json(model);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveArticle(int id)
        {
            var res = ArticleMgr.Remove(id);
            return Json(res);
        }

        /// <summary>
        /// 根据ID获取文章实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetArticleByID(int id)
        {
            Article res = ArticleMgr.GetArticleByID(id);
            //return Json(JsonConvert.SerializeObject(res, Config.FULL_DATE_FORMAT));
            return Json(res);
        }

        /// <summary>
        /// 保存文章 新增/修改
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public JsonResult SaveArticle(Article article)
        {
            bool res = false;
            article.Update_Time = DateTime.Now;
            article.Create_User = ContextObjects.CurrentUser.ID;

            //id为-1时，即新增
            if (article.ID == -1)
            {
                res = ArticleMgr.Add(article);
            }
            else
            {
                res = ArticleMgr.Update(article);
            }
            return Json(res);
        }
        #endregion
    }
}