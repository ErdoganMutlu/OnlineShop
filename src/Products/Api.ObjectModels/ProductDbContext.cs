using Api.ObjectModels.Base;
using Api.ObjectModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels;

public  sealed class ProductDbContext : ApplicationDbContextBase
{
    public DbSet<Product> Products { get; set; }

    private readonly string _schema = "product";
    public ProductDbContext(DbContextOptions<ProductDbContext> contextOptions)
        : base(contextOptions)
    {
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Products", _schema);
            
        base.OnModelCreating(modelBuilder);
    }
}