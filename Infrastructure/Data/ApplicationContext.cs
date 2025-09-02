using ECommerceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Infrastructure.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = new Guid("10000000-0000-0000-0000-000000000001"), Name = "Shoe", Description = "A stylish shoe", Price = 10.0m, StockQuantity = 5 },
                new Product { Id = new Guid("10000000-0000-0000-0000-000000000002"), Name = "Wristwatch", Description = "A classic wristwatch", Price = 20.0m, StockQuantity = 5 },
                new Product { Id = new Guid("10000000-0000-0000-0000-000000000003"), Name = "Backpack", Description = "A durable backpack", Price = 30.0m, StockQuantity = 5 }
            );
        }
    }
}