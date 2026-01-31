namespace EWearShop.Domain.Orders;

public sealed class OrderCustomer
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? FatherName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required OrderCustomerAddress Address { get; init; }
    public required Guid OrderId { get; init; }
    public Order Order { get; init; } = null!;
}