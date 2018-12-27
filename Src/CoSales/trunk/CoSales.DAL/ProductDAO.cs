using CoSales.Model;
using CoSales.Model.NodeType;
using CoSales.Model.PO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.DAL
{
    public class ProductDAO
    {
        public static readonly ProductDAO DAO = new ProductDAO();

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResultPager<Product> GetProductInfo(Product param)
        {
            string sql = @"
SELECT  ROW_NUMBER() OVER ( ORDER BY A.ID ) AS RN ,
        a.ID ,
        ProductName ,
        Price ,
        ProductStockNumber ,
        ProductSellNumber ,
        State ,
        nd.NodeName StateText
FROM    T_PRODUCT a
        LEFT JOIN dbo.T_DIC_NODE nd ON a.state = nd.NodeID
        /**where**/ 
";

            // Dapper 扩展 SqlBuilder
            SqlBuilder builder = new SqlBuilder();

            // 产品名称模糊匹配
            if (!string.IsNullOrEmpty(param.ProductName))
            {
                builder.Where("ProductName LIKE @ProductName", new { ProductName = string.Format($"%{param.ProductName}%") });
            }
            // 产品状态筛选
            if (param.StateList != null && param.StateList.Count > 0)
            {
                builder.Where("State in @StateList", new { StateList = param.StateList });
            }

            ResultPager<Product> resPager = DapperHelper.GetResultPager<Product>(builder, sql, param);

            return resPager;
        }

        /// <summary>
        /// 根据id获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            string sql = @"
SELECT  a.ID ,
        ProductName ,
        Price ,
        ProductStockNumber ,
        ProductSellNumber ,
        State ,
        nd.NodeName StateText
FROM    T_PRODUCT a
        LEFT JOIN dbo.T_DIC_NODE nd ON a.state = nd.NodeID
WHERE a.ID=@ID
";
            object pams = new { ID = id };
            return DapperHelper.QueryFirstOrDefault<Product>(sql, pams);
        }

        /// <summary>
        /// 添加产品，返回主键ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(Product entity)
        {
            return DapperHelper.Insert(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int? RemoveProduct(IEnumerable<int> ids)
        {
            string sql = @"UPDATE T_PRODUCT SET State=@State WHERE ID IN @ID";
            object pams = new { State = (int)EnumProductState.已删除, ID = ids };
            return DapperHelper.Excute(sql, pams);
        }

        public List<Product> GetList()
        {
            return DapperHelper.GetList<Product>().ToList();
        }

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int? UpdateProduct(Product entity)
        {
            string sql = @"
UPDATE  dbo.T_PRODUCT
SET     ProductName = @ProductName ,
        Price = @Price ,
        ProductStockNumber = @ProductStockNumber ,
        ProductSellNumber = @ProductSellNumber
WHERE   ID = @ID
";
            return DapperHelper.Excute(sql, entity);
        }

        /// <summary>
        /// 状态修改，即流转至某节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int? FlowMoveTo(IEnumerable<int> ids, int state)
        {
            string sql = @"UPDATE dbo.T_PRODUCT SET State = @State WHERE ID IN @ID";
            object pams = new { State = state, ID = ids };
            return DapperHelper.Excute(sql, pams);
        }



    }
}
