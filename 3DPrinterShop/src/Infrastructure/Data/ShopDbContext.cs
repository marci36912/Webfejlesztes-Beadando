using Microsoft.EntityFrameworkCore;
using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Infrastructure.Data;

public class ShopDbContext : DbContext
{
    public DbSet<Component> Components { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Printer> Printers { get; set; }
    
    public DbSet<User> Users { get; set; }

    public ShopDbContext()
    {
    }
    
    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>()
            .HasKey(x => x.ModelNumber)
            .HasName("PrimaryKey_ModelNumber");
    }
}