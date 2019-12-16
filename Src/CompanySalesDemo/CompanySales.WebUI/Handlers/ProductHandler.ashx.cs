using CompanySales.BLL;
using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using CompanySales.Common;
using CompanySales.Model.Parameter;

namespace CompanySales.WebUI.Handlers
{
    /// <summary>
    /// ProductHandler 的摘要说明
    /// </summary>
    public class ProductHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.PathInfo.Substring(1);
            // 反射获取方法对象，方法名大小写敏感
            MethodInfo method = GetType().GetMethod(name);
            if (null == method)
            {
                context.Response.Write(null);
                return;
            }
            // 调用反射得到的方法
            method.Invoke(this, new object[] { context });
        }

        public void GetList(HttpContext context)
        {
            List<Product> list = ProductMgr.GetList();

            string json = JsonConvert.SerializeObject(list);
            context.Response.Write(json);
        }

        public void GetListByPage(HttpContext context)
        {
            int pageIndex = int.Parse(context.Request["pageIndex"]);
            int pageSize = int.Parse(context.Request["pageSize"]);
            ProductParameter parameter = new ProductParameter();
            parameter.PageIndex = pageIndex;
            parameter.PageSize = pageSize;

            Pager<Product> result = ProductMgr.GetListByPage(parameter);

            string json = JsonConvert.SerializeObject(result);
            context.Response.Write(json);
        }

        public void SaveProduct(HttpContext context)
        {
            Product entity = GetProductFromRequest(context);

            bool success = ProductMgr.AddProduct(entity);
            StateModel state = new StateModel(success);
            if (!state.Status)
            {
                state.Message = "新增发生异常，请重新添加！";
            }
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public void CheckProductById(HttpContext context)
        {
            int id = int.Parse(context.Request["ProductID"]);
            bool isExist = ProductMgr.CheckProductById(id);
            StateModel state = new StateModel(isExist);
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public void DeleteById(HttpContext context)
        {
            int id = int.Parse(context.Request["ProductID"]);
            bool success = ProductMgr.DeleteById(id);
            StateModel state = new StateModel(success);
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public void GetProductById(HttpContext context)
        {
            int id = int.Parse(context.Request["ProductID"]);
            Product entity = ProductMgr.GetProductById(id);
            context.Response.Write(JsonConvert.SerializeObject(entity));
        }

        public void UpdateProduct(HttpContext context)
        {
            Product entity = GetProductFromRequest(context);

            bool success = ProductMgr.Update(entity);
            StateModel state = new StateModel(success);
            if (!state.Status)
            {
                state.Message = "更新失败，请重新更新！";
            }
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        /// <summary>
        /// 从前端request对象中解析，得到产品对象
        /// 用于新增产品和更新产品业务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Product GetProductFromRequest(HttpContext context)
        {
            Product entity = new Product();
            entity.ProductID = int.Parse(context.Request["ProductID"]);
            entity.ProductName = context.Request["ProductName"];
            entity.Price = decimal.Parse(context.Request["Price"]);
            entity.ProductStockNumber = int.Parse(context.Request["ProductStockNumber"]);

            return entity;
        }

        public void BatchDelete(HttpContext context)
        {
            string ids = context.Request["ids"];
            StateModel state = new StateModel();
            state.Status = ProductMgr.BatchDelete(ids);
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}