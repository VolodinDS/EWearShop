using EWearShop.Domain.Products;

namespace EWearShop.Domain.Orders;

public sealed class OrderItem
{
    public Guid OrderId { get; init; }
    public Order Order { get; init; } = null!;

    public Guid ProductId { get; init; }
    public Product Product { get; init; } = null!;

    public int Quantity { get; init; }
}