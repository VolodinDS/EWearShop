using EWearShop.Domain.Orders;
using EWearShop.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace EWearShop.DAL;

public interface IEWearShopDbContext
{
    IQueryable<Product> Products { get; }
    IQueryable<Order> Orders { get; }
    IQueryable<OrderItem> OrderItems { get; }

    void AddEntity<T>(T entity) where T : class;
    void AddEntities<T>(IEnumerable<T> entities) where T : class;
    void UpdateEntity<T>(T entity) where T : class;
    void RemoveEntity<T>(T entity) where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

internal sealed class EWearShopDbContext : DbContext, IEWearShopDbContext
{
    IQueryable<Order> IEWearShopDbContext.Orders => Orders;
    IQueryable<OrderItem> IEWearShopDbContext.OrderItems => OrderItems;
    IQueryable<Product> IEWearShopDbContext.Products => Products;

    public required DbSet<Product> Products { get; init; }
    public required DbSet<Order> Orders { get; init; }
    public required DbSet<OrderItem> OrderItems { get; init; }

    public EWearShopDbContext(DbContextOptions<EWearShopDbContext> options) : base(options)
    {
    }

    public void AddEntity<T>(T entity) where T : class
    {
        Set<T>().Add(entity);
    }

    public void AddEntities<T>(IEnumerable<T> entities) where T : class
    {
        Set<T>().AddRange(entities);
    }

    public void UpdateEntity<T>(T entity) where T : class
    {
        Set<T>().Update(entity);
    }

    public void RemoveEntity<T>(T entity) where T : class
    {
        Set<T>().Remove(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EWearShopDbContext).Assembly);
    }
}