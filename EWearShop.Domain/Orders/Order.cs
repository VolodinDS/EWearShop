using EWearShop.Domain.Products;

namespace EWearShop.Domain.Orders;

public sealed class Order
{
    public Guid Id { get; init; }
    public DateTimeOffset OrderDate { get; init; }
    public OrderCustomer CustomerInfo { get; init; } = null!;

    public IList<OrderItem> Items { get; init; } = [];
    public IList<Product> Products { get; init; } = [];

    // Private constructor for ORM or serialization purposes
    private Order() { }

    public static Order Create(
        DateTimeOffset orderDate,
        OrderCustomer customerInfo,
        IList<OrderItem> items)
    {
        OrderException.ThrowIfNoItems(items);

        return new Order
        {
            Id = Guid.NewGuid(),
            OrderDate = orderDate,
            CustomerInfo = customerInfo,
            Items = items
        };
    }
}