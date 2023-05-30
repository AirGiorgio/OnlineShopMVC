using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopMvc.Domain.Model;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
            public DbSet<ProductTag> ProductTag { get; set; }
            public DbSet<OrderProduct> OrderProduct { get; set; }
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

                builder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId }); //?

                builder.Entity<OrderProduct>()
                    .HasOne<Order>(op => op.Order)
                    .WithMany(o => o.OrderProducts)
                    .HasForeignKey(op => op.OrderId);
           
            builder.Entity<OrderProduct>()
                   .HasOne<Product>(op => op.Product)
                   .WithMany(o => o.OrderProducts)
                   .HasForeignKey(op => op.ProductId);

            builder.Entity<ProductTag>()
                    .HasKey(pt => new { pt.ProductId, pt.TagId });

                builder.Entity<ProductTag>()
                .HasOne<Product>(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);
           
            builder.Entity<ProductTag>()
              .HasOne<Tag>(pt => pt.Tag)
              .WithMany(p => p.ProductTags)
              .HasForeignKey(pt => pt.TagId);
        }    
    }
}
