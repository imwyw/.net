using ArticleDemo.BLL;
using ArticleDemo.Model;
using ArticleDemo.MVC.UI.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleDemo.MVC.UI.Controllers
{
    /// <summary>
    /// 类别控制器
    /// </summary>
    public class CategoryController : BaseController
    {
        #region Views

        /// <summary>
        /// 类别列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryListView()
        {
            return View();
        }

        /// <summary>
        /// 新增/编辑 类别视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCategoryView(int? id)
        {
            ViewBag.CateID = id;
            return View();
        }

        #endregion

        #region Datas

        /// <summary>
        /// 获取类别数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCategoryList()
        {
            DataTable dt = CategoryMgr.GetCategory();
            //return Json(JsonConvert.SerializeObject(dt, Config.FULL_DATE_FORMAT));
            return Json(dt);
        }

        /// <summary>
        /// 类别保存
        /// </summary>
        /// <param name="cate"></param>
        /// <returns></returns>
        public JsonResult SaveCategory(Category cate)
        {
            bool res = false;
            if (cate.ID == -1)
            {
                res = CategoryMgr.Add(cate.Name);
            }
            else
            {
                res = CategoryMgr.Update(cate);
            }
            return Json(res);
        }

        /// <summary>
        /// 根据ID获取类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetCategoryByID(int id)
        {
            var cate = CategoryMgr.GetCategoryByID(id);
            return Json(cate, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据id删除类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveCategoryByID(int id)
        {
            bool res = CategoryMgr.DeleteCategory(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}