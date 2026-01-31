namespace EWearShop.Domain.Orders;

public sealed class OrderFactory(TimeProvider timeProvider)
{
    private readonly TimeProvider _timeProvider = timeProvider;

    public Order Create(
        OrderCustomer customerInfo,
        IList<OrderItem> items)
    {
        OrderException.ThrowIfNoItems(items);

        return new Order
        {
            Id = Guid.NewGuid(),
            OrderDate = _timeProvider.GetUtcNow(),
            CustomerInfo = customerInfo,
            Items = items
        };
    }
}