namespace CompanySales.Model.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading;
    using Common;

    public partial class SaleContext : DbContext
    {
        /// <summary>
        /// name=SaleContext
        /// SaleContext 对应 web.config 中【connectionStrings】配置节点
        /// </summary>
        public SaleContext()
            : base("name=SaleContext")
        {
            // 记录SQL的委托，LogFormat处理每个SQL
            Database.Log = LogFormat;
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<SellOrder> SellOrder { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Manager)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.ProviderName)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.ContactName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.ProviderAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.ProviderPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.ProviderEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Roles)
                .IsUnicode(false);
        }

        private void LogFormat(string message)
        {
            // 将EF执行的SQL语句记录至log文件
            Log4Helper.InfoLog.DebugFormat("[{0}]{1}-- {2}", Thread.CurrentThread.ManagedThreadId,
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), message.Trim());
        }
    }
}
