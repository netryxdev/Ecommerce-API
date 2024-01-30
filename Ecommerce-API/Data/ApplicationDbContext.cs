using Microsoft.EntityFrameworkCore;
using Ecommerce_API.Models;

namespace Ecommerce_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().ToTable("t_cart_items");
            modelBuilder.Entity<Category>().ToTable("t_categories");
            modelBuilder.Entity<Order>().ToTable("t_orders");
            modelBuilder.Entity<Product>().ToTable("t_products");
            modelBuilder.Entity<Review>().ToTable("t_reviews");
            modelBuilder.Entity<User>().ToTable("t_users");

            // Configure outras relações, índices, etc.
        }
    }
}
