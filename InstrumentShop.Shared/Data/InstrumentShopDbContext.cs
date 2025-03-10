using InstrumentShop.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentShop.Shared.Data
{
    public class InstrumentShopDbContext : DbContext
    {
        public InstrumentShopDbContext(DbContextOptions<InstrumentShopDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InstrumentCategory> InstrumentCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình kiểu dữ liệu cho decimal
            modelBuilder.Entity<Instrument>()
                .Property(i => i.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasPrecision(10, 2);

            // Cấu hình khóa chính tổ hợp cho OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.InstrumentId });

            modelBuilder.Entity<Instrument>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Instruments)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
        }
    }
}