using EWearShop.Domain.Orders;

namespace EWearShop.Api.Features.Orders.GetOrdersForAdmin;

public sealed record GetOrdersForAdminResponseItem
{
    public required Guid Id { get; init; }
    public required DateTimeOffset OrderDate { get; init; }
    public required OrderCustomer CustomerInfo { get; init; }
    public required Item[] Items { get; init; }

    public sealed record Item
    {
        public required Guid Id { get; init; }
        public required int Quantity { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required decimal Price { get; init; }
        public required string Currency { get; init; }
        public required string ImageUrl { get; init; }
    }
}