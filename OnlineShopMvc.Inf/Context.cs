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
            //public DbSet<ProductTag> ProductTag { get; set; }
            //public DbSet<OrderProduct> OrderProduct { get; set; }
            public DbSet<Tag> Tags { get; set; }

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
            }    
    }
}
