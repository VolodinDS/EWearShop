using EWearShop.Domain.Products;

namespace EWearShop.Domain.Orders;

public sealed class Order
{
    public required Guid Id { get; init; }
    public required DateTimeOffset OrderDate { get; init; }
    public required OrderCustomer CustomerInfo { get; init; }

    public IList<OrderItem> Items { get; init; } = [];
    public IList<Product> Products { get; init; } = [];

    internal Order() { }
}