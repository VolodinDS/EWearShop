namespace EWearShop.Domain.Orders;

public sealed class OrderCustomerAddress
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