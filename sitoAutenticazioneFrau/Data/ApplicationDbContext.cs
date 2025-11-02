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
            builder.Entity<Product>().HasData(
                   new Product
                   {
                       Id = 1,
                       Name = "Camicia Classica",
                       Description = "Camicia bianca elegante in cotone",
                       Price = 49.99M,
                       ImageUrl = "/images/products/camicia-classica.jpg"
                   },
                   new Product
                   {
                       Id = 2,
                       Name = "Pantaloni Eleganti",
                       Description = "Pantaloni classici neri in tessuto premium",
                       Price = 79.90M,
                       ImageUrl = "/images/products/pantaloni-eleganti.jpg"
                   },
                   new Product
                   {
                       Id = 3,
                       Name = "Giacca Sartoriale",
                       Description = "Giacca slim fit in lana, ideale per occasioni formali",
                       Price = 149.00M,
                       ImageUrl = "/images/products/giacca-sartoriale.jpg"
                   },
                   new Product
                   {
                       Id = 4,
                       Name = "Abito Completo",
                       Description = "Abito blu navy in tessuto raffinato con chiusura a due bottoni",
                       Price = 199.90M,
                       ImageUrl = "/images/products/abito-completo.jpg"
                   },
                   new Product
                   {
                       Id = 5,
                       Name = "Cintura in Pelle",
                       Description = "Cintura elegante in vera pelle italiana",
                       Price = 39.50M,
                       ImageUrl = "/images/products/cintura-pelle.jpg"
                   },
                   new Product
                   {
                       Id = 6,
                       Name = "Scarpe Oxford",
                       Description = "Scarpe stringate eleganti in pelle lucida",
                       Price = 129.90M,
                       ImageUrl = "/images/products/scarpe-oxford.jpg"
                   }
               );



        }


        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

    }
}
