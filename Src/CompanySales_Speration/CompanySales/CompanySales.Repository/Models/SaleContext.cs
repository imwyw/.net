using System;
using CompanySales.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanySales.Repository.Models
{
    public partial class SaleContext : DbContext
    {
        public SaleContext()
        {
        }

        public SaleContext(DbContextOptions<SaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<SellOrder> SellOrder { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=.;Database=CompanySales;Trusted_Connection=True;uid=sa;pwd=123456;");
                // 从配置中读取连接字符串
                var connStr = ConfigHelper.GetConfig()["DbConfig:ConnectionString"];
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.AttachmentName).IsUnicode(false);

                entity.Property(e => e.AttachmentType).IsUnicode(false);

                entity.Property(e => e.PhysicalPath).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartDescription).IsUnicode(false);

                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.Property(e => e.Manager)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.EmployeeName).IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.ProductName).IsUnicode(false);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.ProviderId).ValueGeneratedNever();

                entity.Property(e => e.ContactName)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProviderAddress).IsUnicode(false);

                entity.Property(e => e.ProviderEmail).IsUnicode(false);

                entity.Property(e => e.ProviderName).IsUnicode(false);

                entity.Property(e => e.ProviderPhone).IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.PurchaseOrderId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SellOrder>(entity =>
            {
                entity.Property(e => e.SellOrderId).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HeadImg).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Roles).IsUnicode(false);

                entity.Property(e => e.UserId).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
