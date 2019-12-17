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
using CompanySales.Model.Domain;

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
        /// <summary>
        /// 产品列表全量数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetList()
        {
            List<Product> list = ProductMgr.GetList();

            return Json(list);
        }

        /// <summary>
        /// 分页获取产品列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public JsonResult GetListByPage(ProductParameter parameter)
        {
            Pager<Product> result = ProductMgr.GetListByPage(parameter);
            return Json(result);
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 用于校验产品id是否重复
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult CheckProductById(int id)
        {
            bool isExist = ProductMgr.CheckProductById(id);
            StateModel state = new StateModel(isExist);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据id删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomActionFilter("admin")]
        public JsonResult DeleteById(int id)
        {
            bool success = ProductMgr.DeleteById(id);
            StateModel state = new StateModel(success);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据主键id获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProductById(int id)
        {
            Product entity = ProductMgr.GetProductById(id);
            return Json(entity);
        }

        /// <summary>
        /// 更新产品实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 批量删除产品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult BatchDelete(string ids)
        {
            StateModel state = new StateModel();
            state.Status = ProductMgr.BatchDelete(ids);
            return Json(state);
        }

        #endregion

        #region 产品图片
        /// <summary>
        /// 上传产品图片
        /// </summary>
        /// <param name="productID">产品主键id</param>
        /// <returns></returns>
        public JsonResult UploadProductImg(int productID)
        {
            StateModel state = AttachmentMgr.AddAttachment(
                HttpContext.Request.Files,
                productID,
                AttachmentType.ProductImage);

            return Json(state);
        }

        #endregion
    }
}