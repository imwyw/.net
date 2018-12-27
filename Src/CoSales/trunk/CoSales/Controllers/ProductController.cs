using CoSales.BLL;
using CoSales.Core;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoSales.Controllers
{
    [CustomAuth]
    public class ProductController : BaseController
    {
        /// <summary>
        /// 产品列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductListView()
        {
            return View();
        }

        /// <summary>
        /// 产品详情展示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditProductView(int? id)
        {
            ViewBag.ProductID = id;
            return View();
        }

        /// <summary>
        /// 分页获取产品信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult GetProductInfo(Product param)
        {
            var result = ProductMgr.Mgr.GetProductInfo(param);
            return Json(result);
        }

        /// <summary>
        /// 根据id获取单个产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProduct(int id)
        {
            var res = ProductMgr.Mgr.GetProduct(id);
            return Json(res);
        }

        /// <summary>
        /// 根据id进行逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveProduct(int id)
        {
            ResultStateDTO res = new ResultStateDTO();
            res.Status = ProductMgr.Mgr.RemoveProduct(id);
            return Json(res);
        }

        /// <summary>
        /// 上传产品图片
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public JsonResult UploadProductImg(int productID)
        {
            LayStateModel state = AttachmentMgr.Mgr.AddAttachment(HttpContext.Request.Files, productID
                , "product", ContextObjects.ProductImagePath);

            return Json(state);
        }

        /// <summary>
        /// 获取产品对应的图片集合
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public JsonResult GetImagesByProduct(int productID)
        {
            List<Attachment> res = AttachmentMgr.Mgr.GetList(productID);
            return Json(res);
        }

        /// <summary>
        /// 根据路径获取产品图片
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public FileResult GetImageByPath(string fullPath)
        {
            string mime = MimeMapping.GetMimeMapping(fullPath);
            return File(fullPath, mime);
        }

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult UpdateProduct(Product entity)
        {
            ResultStateDTO result = new ResultStateDTO();

            // ID为默认值0时，即为新增，返回新增数据主键ID
            if (entity.ID == 0)
            {
                int res = ProductMgr.Mgr.InsertProduct(entity);
                result.Status = res > 0;
                result.Message = res.ToString();
            }
            result.Status = ProductMgr.Mgr.UpdateProduct(entity);
            return Json(result);
        }
    }
}