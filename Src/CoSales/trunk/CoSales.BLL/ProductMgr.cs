using CoSales.DAL;
using CoSales.Model;
using CoSales.Model.NodeType;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.BLL
{
    public class ProductMgr
    {
        public static readonly ProductMgr Mgr = new ProductMgr();

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResultPager<Product> GetProductInfo(Product param)
        {
            // 普通用户，无法查看已删除的记录
            if (ContextObjects.CurrentUser.RoleID == 0)
            {
                param.StateList = new List<int>() {
                    (int)EnumProductState.待审核, (int)EnumProductState.正常, (int)EnumProductState.被驳回
                };
            }
            var res = ProductDAO.DAO.GetProductInfo(param);
            return res;
        }

        /// <summary>
        /// 根据id获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            Product res = ProductDAO.DAO.GetProduct(id);
            return res;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveProduct(int id)
        {
            var res = ProductDAO.DAO.RemoveProduct(id).GetValueOrDefault(-1);
            return res > 0;
        }

        public List<Product> GetList()
        {
            var res = ProductDAO.DAO.GetList();
            return res;
        }

    }
}
