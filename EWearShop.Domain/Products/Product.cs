using EWearShop.Domain.Orders;

namespace EWearShop.Domain.Products;

public sealed class Product
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public ProductCategory Category { get; init; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }

    public IList<OrderItem> OrderItems { get; init; } = [];
    public IList<Order> Orders { get; init; } = [];

    // Private constructor for ORM or serialization purposes
    private Product() { }

    public static Product Create(string name, string description, ProductCategory category, decimal price, string currency, string imageUrl)
    {
        ProductException.ThrowIfEmptyName(name);
        ProductException.ThrowIfNameTooLong(name);
        ProductException.ThrowIfDescriptionTooLong(description);
        ProductException.ThrowIfInvalidCategory(category);
        ProductException.ThrowIfInvalidImageUrl(imageUrl);
        ProductException.ThrowIfImageUrlTooLong(imageUrl);
        ProductException.ThrowIfNegativePrice(price);
        ProductException.ThrowIfInvalidCurrency(currency);

        return new Product
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            Description = description,
            Category = category,
            Price = price,
            Currency = currency,
            ImageUrl = imageUrl,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
}