using CompanySales.BLL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanySales.Model.Parameter;

namespace CompanySales.MVC.Controllers
{
    [CusAuth]
    public class ProductController : Controller
    {
        #region 产品页面视图
        //[AllowAnonymous]//如果需要特殊标记允许匿名进行访问
        public ActionResult ProductList()
        {
            return View();
        }

        [CustomActionFilter("admin")]
        public ActionResult ProductDetail()
        {
            return View();
        }
        #endregion

        #region 产品数据相关操作
        public JsonResult GetList()
        {
            List<Product> list = ProductMgr.GetList();

            return Json(list);
        }

        public JsonResult GetListByPage(ProductParameter parameter)
        {
            Pager<Product> result = ProductMgr.GetListByPage(parameter);
            return Json(result);
        }

        public JsonResult SaveProduct(Product entity)
        {
            bool success = ProductMgr.AddProduct(entity);
            StateModel state = new StateModel(success);
            if (!state.Status)
            {
                state.Message = "新增发生异常，请重新添加！";
            }
            return Json(state);
        }

        public JsonResult CheckProductById(int id)
        {
            bool isExist = ProductMgr.CheckProductById(id);
            StateModel state = new StateModel(isExist);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        [CustomActionFilter("admin")]
        public JsonResult DeleteById(int id)
        {
            bool success = ProductMgr.DeleteById(id);
            StateModel state = new StateModel(success);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductById(int id)
        {
            Product entity = ProductMgr.GetProductById(id);
            return Json(entity);
        }

        public JsonResult UpdateProduct(Product entity)
        {
            bool success = ProductMgr.Update(entity);
            StateModel state = new StateModel(success);
            if (!state.Status)
            {
                state.Message = "更新失败，请重新更新！";
            }
            return Json(state);
        }

        public JsonResult BatchDelete(string ids)
        {
            StateModel state = new StateModel();
            state.Status = ProductMgr.BatchDelete(ids);
            return Json(state);
        }
        #endregion
    }
}