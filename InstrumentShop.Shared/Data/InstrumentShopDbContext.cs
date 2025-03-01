using InstrumentShop.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentShop.Shared.Data
{
    public class InstrumentShopDbContext : DbContext
    {
        public InstrumentShopDbContext(DbContextOptions<InstrumentShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
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
                .HasPrecision(10, 2); // Specify precision (10) and scale (2)
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(12, 2);

            // Cấu hình các mối quan hệ bằng Fluent API
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Instrument)
                .WithMany(i => i.CartItems)
                .HasForeignKey(ci => ci.CartItemInstrumentId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CustomerId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Instrument)
                .WithMany(i => i.Feedbacks)
                .HasForeignKey(f => f.FeedbackInstrumentId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Instrument)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.OrderDetailInstrumentId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<Instrument>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Instruments)
                .HasForeignKey(i => i.CategoryId);
        }
    }
}