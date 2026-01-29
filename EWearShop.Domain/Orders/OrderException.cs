namespace EWearShop.Domain.Orders;

public sealed class OrderException : Exception
{
    public static OrderException NoItems() => new("An order must contain at least one item.");

    private OrderException(string message) : base(message) { }

    public static void ThrowIfNoItems(IList<OrderItem> items)
    {
        if (items == null || items.Count == 0)
        {
            throw NoItems();
        }
    }
}