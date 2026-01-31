using EWearShop.Domain.Orders;

namespace EWearShop.Domain.Products;

public sealed record Product
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ProductCategory Category { get; init; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; } = null!;
    public required string ImageUrl { get; set; } = null!;
    public required DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }

    public IList<OrderItem> OrderItems { get; init; } = [];
    public IList<Order> Orders { get; init; } = [];

    internal Product() { }
}