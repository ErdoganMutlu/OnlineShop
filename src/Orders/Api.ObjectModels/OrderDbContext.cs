using Api.ObjectModels.Base;
using Api.ObjectModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels;

public  sealed class OrderDbContext : ApplicationDbContextBase
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
        
    public DbSet<OrderView> OrderViews { get; set; }

    private readonly string _schema = "order";
    public OrderDbContext(DbContextOptions<OrderDbContext> contextOptions)
        : base(contextOptions)
    {
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("Orders", _schema);
        modelBuilder.Entity<OrderProduct>().ToTable("OrderProducts", _schema);
        modelBuilder.Entity<OrderView>().ToTable("OrderViews", _schema);
            
        base.OnModelCreating(modelBuilder);
    }
}