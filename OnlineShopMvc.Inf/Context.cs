using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMVC.Infrastructure
{
        public class Context : IdentityDbContext
        {
            public DbSet<Address> Addresses { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Client> Clients { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Tag> Tags { get; set; }
            
            public Context()
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                 optionsBuilder.UseSqlServer("Server=.;Database=OnlineShopMvc;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            public Context(DbContextOptions options) : base(options)
            {


            }
            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                 builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

                builder.Entity<Order>()
                  .Property(p => p.TotalCost)
                  .HasPrecision(18, 2);

                 builder.Entity<Product>()
                 .HasMany(e => e.Tags)
                 .WithMany(e => e.Products);

                builder.Entity<Tag>()
                 .HasMany(e => e.Products)
                 .WithMany(e => e.Tags);

                builder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithMany(e => e.Products);

                builder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders);
        }    
    }
}
