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
                   Price = 49.99M
               },
               new Product
               {
                   Id = 2,
                   Name = "Pantaloni Eleganti",
                   Description = "Pantaloni classici neri in tessuto premium",
                   Price = 79.90M
               },
               new Product
               {
                   Id = 3,
                   Name = "Giacca Sartoriale",
                   Description = "Giacca slim fit in lana, ideale per occasioni formali",
                   Price = 149.00M
               },
               new Product
               {
                   Id = 4,
                   Name = "Abito Completo",
                   Description = "Abito blu navy in tessuto raffinato con chiusura a due bottoni",
                   Price = 199.90M
               },
               new Product
               {
                   Id = 5,
                   Name = "Cintura in Pelle",
                   Description = "Cintura elegante in vera pelle italiana",
                   Price = 39.50M
               },
               new Product
               {
                   Id = 6,
                   Name = "Scarpe Oxford",
                   Description = "Scarpe stringate eleganti in pelle lucida",
                   Price = 129.90M
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
