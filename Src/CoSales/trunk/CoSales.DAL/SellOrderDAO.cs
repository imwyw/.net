using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoSales.Model.PO;
using CoSales.Model.DomainModel;
using Dapper;
using CoSales.Model;

namespace CoSales.DAL
{
    public class SellOrderDAO
    {
        public static readonly SellOrderDAO DAO = new SellOrderDAO();

        public ResultPager<SellOrderInfo> GetSellOrderInfo(SellOrderInfo param)
        {
            string sql = @"
SELECT  Row_number() over(order by a.ID) as RN,
        a.ID ,
        a.ProductID ,
        a.SellOrderNumber ,
        a.EmployeeID ,
        a.CustomerID ,
        a.SellOrderDate ,
        pt.ProductName ,
        pt.Price ,
        pt.ProductStockNumber ,
        pt.ProductSellNumber ,
        em.ID EmployeeName ,
        em.Sex ,
        em.BirthDate ,
        em.HireDate ,
        em.Salary ,
        em.DepartmentID ,
        cm.CompanyName ,
        cm.ContactName ,
        cm.Phone ,
        cm.Address ,
        cm.EmailAddress
FROM    dbo.T_SELL_ORDER a
        LEFT JOIN dbo.T_PRODUCT pt ON a.ProductID = pt.ID
        LEFT JOIN dbo.T_EMPLOYEE em ON a.EmployeeID = em.ID
        LEFT JOIN dbo.T_CUSTOMER cm ON a.CustomerID = cm.ID
        /**where**/ 
";

            // Dapper 扩展 SqlBuilder
            SqlBuilder builder = new SqlBuilder();

            if (!string.IsNullOrEmpty(param.EmployeeName))
            {
                builder.Where("EmployeeName LIKE @EmployeeName", new { EmployeeName = string.Format($"%{param.EmployeeName}%") });
            }
            if (!string.IsNullOrEmpty(param.CompanyName))
            {
                builder.Where("CompanyName LIKE @CompanyName", new { CompanyName = string.Format($"%{param.CompanyName}%") });
            }
            if (param.SellOrderDate.HasValue)
            {
                builder.Where("SellOrderDate=@SellOrderDate", new { SellOrderDate = param.SellOrderDate });
            }

            ResultPager<SellOrderInfo> resPager = DapperHelper.GetResultPager<SellOrderInfo>(builder, sql, param);

            return resPager;
        }

        /// <summary>
        /// 添加销售记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(SellOrder entity)
        {
            return DapperHelper.Insert(entity);
        }

        /// <summary>
        /// 统计一年中每个月各销售人员销售产品数量
        /// 目前写死展示 年销量前5的员工
        /// </summary>
        /// <param name="year">需要统计的年份</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetEmployeeSellStat(int year)
        {
            string sql = @"
WITH    temp
          AS ( SELECT   FORMAT(a.SellOrderDate, 'yyyy-MM') SellMonth ,
                        MONTH(a.SellOrderDate) IntMonth,
                        a.EmployeeID ,
                        em.EmployeeName ,
                        a.SellOrderNumber
               FROM     dbo.T_SELL_ORDER a
                        LEFT	JOIN dbo.T_EMPLOYEE em ON a.EmployeeID = em.ID
                        LEFT JOIN dbo.T_PRODUCT pd ON a.ProductID = pd.ID
                        LEFT JOIN dbo.T_CUSTOMER cu ON a.CustomerID = cu.ID
               WHERE    YEAR(a.SellOrderDate) = @Year
             )
    SELECT  temp.SellMonth ,
            temp.IntMonth ,/*IntMonth字段排序用*/
            temp.EmployeeID ,
            temp.EmployeeName ,
            SUM(temp.SellOrderNumber) SellSum
    FROM    temp
            JOIN ( SELECT TOP 5
                            EmployeeID
                   FROM     temp
                   GROUP BY EmployeeID
                   ORDER BY SUM(SellOrderNumber) DESC
                 ) tp ON temp.EmployeeID = tp.EmployeeID
    GROUP BY temp.SellMonth ,
            temp.IntMonth,
            temp.EmployeeID ,
            temp.EmployeeName
    ORDER BY temp.EmployeeID
";
            object pams = new { Year = year };
            return DapperHelper.Query<dynamic>(sql, pams);
        }

    }
}
