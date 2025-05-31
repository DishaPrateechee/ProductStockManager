using Microsoft.EntityFrameworkCore;
using ProductStockManager.Core;

namespace ProductStockManager.Infrastructure;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This part is crucial for the unique index
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.ProductId)
            .IsUnique();
    }
}