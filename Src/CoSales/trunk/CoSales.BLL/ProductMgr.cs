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
    }
}
