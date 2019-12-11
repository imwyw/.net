using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.DAL
{
    public class OrderDAO
    {
        /// <summary>
        /// 分页查询销售记录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Pager<SellOrderDomain> GetSellOrderByPage(SellOrderParam parameter)
        {
            Pager<SellOrderDomain> result = new Pager<SellOrderDomain>();

            StringBuilder sqlLayout = new StringBuilder(@"
SELECT a.SellOrderID ,
        a.ProductID ,
        a.SellOrderNumber ,
        a.EmployeeID ,
        a.CustomerID ,
        a.SellOrderDate ,
        prod.ProductID ,
        prod.ProductName ,
        prod.Price ,
        prod.ProductStockNumber ,
        prod.ProductSellNumber ,
        cus.CustomerID ,
        cus.CompanyName ,
        cus.ContactName ,
        cus.Phone ,
        cus.Address ,
        cus.EmailAddress ,
        emp.EmployeeID ,
        emp.EmployeeName ,
        emp.Sex ,
        emp.BirthDate ,
        emp.HireDate ,
        emp.Salary ,
        emp.DepartmentID
FROM    dbo.Sell_Order a
        LEFT JOIN dbo.Product prod ON prod.ProductID = a.ProductID
        LEFT JOIN dbo.Customer cus ON cus.CustomerID = a.CustomerID
        LEFT JOIN dbo.Employee emp ON emp.EmployeeID = a.EmployeeID
WHERE 1=1
");

            // 动态查询条件的拼接
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(parameter.ProductName))
            {
                sqlLayout.Append($" AND prod.ProductName like @ProductName");
                paramList.Add(new SqlParameter("@ProductName", $"%{parameter.ProductName}%"));
            }

            // 统计数目
            result.Total = EFUtility.GetCount(sqlLayout.ToString(), paramList);
            result.Rows = EFUtility.GetList<SellOrderDomain>(sqlLayout.ToString(), paramList, parameter);

            return result;
        }
    }
}
