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
        public ActionResult ProductListView()
        {
            return View();
        }

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

        public JsonResult GetProduct(int id)
        {
            var res = ProductMgr.Mgr.GetProduct(id);
            return Json(res);
        }

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

        public JsonResult GetImagesByProduct(int productID)
        {
            List<Attachment> res = AttachmentMgr.Mgr.GetList(productID);
            return Json(res);
        }

        public FileResult GetImageByPath(string fullPath)
        {
            string mime = MimeMapping.GetMimeMapping(fullPath);
            return File(fullPath, mime);
        }
    }
}