using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Demo
{
    public class ProgramUi
    {
        public static void Show()
        {
            Order o2 = new Order(new SqLiteDal());

            // xxxType  从配置中获取
            //Order o1 = new Order(Activator.CreateInstance<xxx>());
            //o1.Add();
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
        readonly IDataAccess dal;

        public Order(IDataAccess dal)
        {
            this.dal = dal;
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        public void Add()
        {
            // 业务验证。。。。。
            dal.Add();
        }
    }

    public interface IDataAccess
    {
        void Add();
    }

    /// <summary>
    /// 模拟SqlServer数据库的操作，相当于数据访问层DAL
    /// 用于数据读写
    /// </summary>
    public class SqlServerDal : IDataAccess
    {
        public void Add()
        {
            Console.WriteLine("在数据库中添加一条订单!");
        }
    }

    public class OracleDal : IDataAccess
    {
        public void Add()
        {
            Console.WriteLine("Oracle中保存订单");
        }
    }

    public class SqLiteDal : IDataAccess
    {
        public void Add()
        {
            Console.WriteLine("SQLite中添加订单");
        }
    }
}
