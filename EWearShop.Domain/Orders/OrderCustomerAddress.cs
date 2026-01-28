namespace EWearShop.Domain.Orders;

public sealed class OrderCustomerAddress
{
    public string Country { get; init; } = null!;
    public string City { get; init; } = null!;
    public string? State { get; init; }
    public string Street { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? ApartmentNumber { get; init; }
    public string? AdditionalInfo { get; init; }

    // Private constructor for ORM or serialization purposes
    private OrderCustomerAddress() { }

    public static OrderCustomerAddress Create(
        string country,
        string city,
        string? state,
        string street,
        string zipCode,
        string houseNumber,
        string? apartmentNumber,
        string? additionalInfo)
    {
        return new OrderCustomerAddress
        {
            Country = country,
            City = city,
            State = state,
            Street = street,
            ZipCode = zipCode,
            HouseNumber = houseNumber,
            ApartmentNumber = apartmentNumber,
            AdditionalInfo = additionalInfo
        };
    }
}