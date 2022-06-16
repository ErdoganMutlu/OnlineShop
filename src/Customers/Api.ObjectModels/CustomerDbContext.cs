using Api.ObjectModels.Base;
using Api.ObjectModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels;

public  sealed class CustomerDbContext : ApplicationDbContextBase
{
    public DbSet<Customer> Customers { get; set; }

    private readonly string _schema = "customer";
    public CustomerDbContext(DbContextOptions<CustomerDbContext> contextOptions)
        : base(contextOptions)
    {
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers", _schema);
            
        base.OnModelCreating(modelBuilder);
    }
}