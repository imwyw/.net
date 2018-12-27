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
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool RemoveProduct(IEnumerable<int> ids)
        {
            var res = ProductDAO.DAO.RemoveProduct(ids).GetValueOrDefault(-1);
            return res > 0;
        }

        public List<Product> GetList()
        {
            var res = ProductDAO.DAO.GetList();
            return res;
        }

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateProduct(Product entity)
        {
            var res = ProductDAO.DAO.UpdateProduct(entity).GetValueOrDefault(-1);
            return res > -1;
        }

        /// <summary>
        /// 新增产品，普通用户新增时需要经过审核判断
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertProduct(Product entity)
        {
            // todo 先写死0，普通用户需要进行审核，管理员新增无需审核
            if (ContextObjects.CurrentUser.RoleID == (int)EnumRole.普通用户)
            {
                entity.State = (int)EnumProductState.待审核;
            }
            else
            {
                entity.State = (int)EnumProductState.正常;
            }
            var res = ProductDAO.DAO.Insert(entity);
            // 主键ID
            entity.ID = res;
            return res;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool PassFlow(IEnumerable<int> ids)
        {
            var res = ProductDAO.DAO.FlowMoveTo(ids, (int)EnumProductState.正常).GetValueOrDefault(-1);
            return res > 0;
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool RejectFlow(IEnumerable<int> ids)
        {
            var res = ProductDAO.DAO.FlowMoveTo(ids, (int)EnumProductState.被驳回).GetValueOrDefault(-1);
            return res > 0;
        }
    }
}
