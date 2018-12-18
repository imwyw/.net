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
SELECT  Row_number() over(order by SellOrderDate) as RN,
        a.SellOrderID ,
        a.ProductID ,
        a.SellOrderNumber ,
        a.EmployeeID ,
        a.CustomerID ,
        a.SellOrderDate ,
        pt.ProductName ,
        pt.Price ,
        pt.ProductStockNumber ,
        pt.ProductSellNumber ,
        em.EmployeeName ,
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
        LEFT JOIN dbo.T_PRODUCT pt ON a.ProductID = pt.ProductID
        LEFT JOIN dbo.T_EMPLOYEE em ON a.EmployeeID = em.EmployeeID
        LEFT JOIN dbo.T_CUSTOMER cm ON a.CustomerID = cm.CustomerID
        /**where**/ 
";
            string countSql = string.Format($"SELECT COUNT(1) FROM ({sql}) AS TR");

            string dataSql = param.IsPager ? string.Format($@"SELECT * FROM ({sql}) AS TR WHERE TR.RN>@PageStart AND TR.RN<@PageEnd ") : sql;

            // Dapper 扩展 SqlBuilder
            SqlBuilder builder = new SqlBuilder();
            var templateCount = builder.AddTemplate(countSql);
            var templateData = builder.AddTemplate(dataSql);

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
            if (param.IsPager)
            {
                builder.AddParameters(new { PageStart = param.PageStart, PageEnd = param.PageEnd });
            }

            ResultPager<SellOrderInfo> resPager = new ResultPager<SellOrderInfo>();
            resPager.Total = (int)DapperHelper.ExcuteScalar(templateCount.RawSql, templateCount.Parameters);
            resPager.Rows = DapperHelper.Query<SellOrderInfo>(templateData.RawSql, templateData.Parameters).ToList();

            return resPager;
        }
    }
}
