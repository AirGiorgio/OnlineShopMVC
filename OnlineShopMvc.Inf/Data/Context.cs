using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Domain.Model;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Data;

public class Context : IdentityDbContext<User, Role, string>
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }

    public Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
             .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

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

        builder.Entity<OrderProduct>()
                .HasKey(x => new { x.OrderId, x.ProductId });

        builder.Entity<OrderProduct>()
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderProducts)
            .HasForeignKey(x => x.ProductId);

        builder.Entity<OrderProduct>()
          .HasOne(x => x.Order)
          .WithMany(x => x.OrderProducts)
          .HasForeignKey(x => x.OrderId);
    }
}