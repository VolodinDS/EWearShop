namespace EWearShop.Domain.Orders;

public sealed class OrderCustomer
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string FatherName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public OrderCustomerAddress Address { get; init; } = null!;

    public Guid OrderId { get; init; }
    public Order Order { get; init; } = null!;

    // Private constructor for ORM or serialization purposes
    private OrderCustomer() { }

    public static OrderCustomer Create(
        string firstName,
        string lastName,
        string fatherName,
        string email,
        string phoneNumber,
        OrderCustomerAddress address)
    {
        return new OrderCustomer
        {
            FirstName = firstName,
            LastName = lastName,
            FatherName = fatherName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address
        };
    }
}