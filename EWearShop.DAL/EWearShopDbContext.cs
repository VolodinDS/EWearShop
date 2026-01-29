using EWearShop.Domain.Orders;
using EWearShop.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace EWearShop.DAL;

public sealed class EWearShopDbContext : DbContext
{
    public required DbSet<Product> Products { get; init; }
    public required DbSet<Order> Orders { get; init; }
    public required DbSet<OrderItem> OrderItems { get; init; }

    public EWearShopDbContext(DbContextOptions<EWearShopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EWearShopDbContext).Assembly);
    }
}