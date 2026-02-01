namespace EWearShop.Api.Features.Orders.CreateOrder;

public sealed record CreateOrderRequest
{
    public required CustomerInfoRequest CustomerInfo { get; init; }
    public required IReadOnlyCollection<ItemRequest> Items { get; init; }

    public sealed record ItemRequest
    {
        public required Guid ProductId { get; init; }
        public required int Quantity { get; init; }
    }
    
    public sealed record CustomerInfoRequest
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public string? FatherName { get; init; }
        public required string Email { get; init; }
        public required string PhoneNumber { get; init; }
        public required CustomerAddressRequest Address { get; init; }
    }

    public sealed record CustomerAddressRequest
    {
        public required string Country { get; init; }
        public required string City { get; init; }
        public string? State { get; init; }
        public required string Street { get; init; }
        public required string ZipCode { get; init; }
        public required string HouseNumber { get; init; }
        public string? ApartmentNumber { get; init; }
        public string? AdditionalInfo { get; init; }
    }
}