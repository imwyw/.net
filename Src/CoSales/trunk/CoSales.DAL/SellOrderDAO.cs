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
    }
}
