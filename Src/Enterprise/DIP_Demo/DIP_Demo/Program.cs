using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 不好的示例，因为的Order直接依赖于OracleDal，当我们持久化存储方案有需求变更的时候，需要改动业务逻辑，这非常得不好
/// </summary>
namespace DIP_Demo
{
    /// <summary>
    /// 模拟UI层的调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Order od = new Order();
            od.Add();
        }
    }

    /// <summary>
    /// 模拟订单业务逻辑，相当于业务逻辑层BLL
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 私有变量，保存对数据操作的对象
        /// 即 Order 依赖于 IOC.SqlServerDal，上层的功能依赖于底层的功能
        /// 不好的设计，当IOC.SqlServerDal发生变化或者不再需要IOC.SqlServerDal的时候需要改动业务层Order类!
        /// </summary>
        readonly SqlServerDal dal = new SqlServerDal();

        /// <summary>
        /// 添加订单
        /// </summary>
        public void Add()
        {
            // 业务验证。。。。。
            dal.Add();
        }
    }

    /// <summary>
    /// 模拟SqlServer数据库的操作，相当于数据访问层DAL
    /// 用于数据读写
    /// </summary>
    public class SqlServerDal
    {
        public void Add()
        {
            Console.WriteLine("在数据库中添加一条订单!");
        }
    }
}
