using CoSales.DAL;
using CoSales.Model;
using CoSales.Model.DomainModel;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.BLL
{
    public class SellOrderMgr
    {
        public static readonly SellOrderMgr Mgr = new SellOrderMgr();
        public ResultPager<SellOrderInfo> GetSellOrderInfo(SellOrderInfo param)
        {
            var res = SellOrderDAO.DAO.GetSellOrderInfo(param);
            return res;
        }

        /// <summary>
        /// 添加销售记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(SellOrder entity)
        {
            var res = SellOrderDAO.DAO.Insert(entity);
            return res > 0;
        }

        /// <summary>
        /// 统计一年中每个月各销售人员销售产品数量
        /// 目前写死展示 年销量前5的员工
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetEmployeeSellStat(int year)
        {
            var res = SellOrderDAO.DAO.GetEmployeeSellStat(year);

            // 也可以通过在业务逻辑层进行处理，在这里将统计数据的处理放在前端进行处理

            // 每个员工的姓名
            //List<dynamic> listLegend = res.GroupBy(t => new { t.EmployeeID, t.EmployeeName }).Select(t => { return t.Key.EmployeeName; }).ToList();

            // 横轴坐标
            //List<dynamic> listX = res.GroupBy(t => t.SellMonth).Select(t => { return t.Key; }).ToList();

            return res;
        }
    }
}
