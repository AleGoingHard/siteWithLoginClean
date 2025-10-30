using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sitoAutenticazioneFrau.Models;

namespace sitoAutenticazioneFrau.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Camicia Classica",
                Description = "Camicia bianca elegante in cotone",
                Price = 49.99M
            });
        }


        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

    }
}
