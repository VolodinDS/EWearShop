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

    // Private constructor for ORM or serialization purposes
    private Product() { }

    public static Product Create(string name, string description, ProductCategory category, decimal price, string currency, string imageUrl)
    {
        // Validation logic
        // 1. Name should not be empty
        if (string.IsNullOrWhiteSpace(name))
        {
            throw ProductException.EmptyName();
        }

        // 2. Price should not be negative
        if (price < 0)
        {
            throw ProductException.NegativePrice();
        }

        // 3. Currency should be a valid ISO 4217 code (basic check)
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
        {
            throw ProductException.InvalidCurrency();
        }

        // 4. ImageUrl should be a valid URL
        if (!Uri.IsWellFormedUriString(imageUrl, UriKind.RelativeOrAbsolute))
        {
            throw ProductException.InvalidImageUrl();
        }

        // 5. Description length check (e.g., max 1000 characters)
        if (description.Length > 1000)
        {
            throw ProductException.DescriptionTooLong();
        }

        // 6. Category validation (assuming all enum values are valid, otherwise implement specific checks)
        if (!Enum.IsDefined(category))
        {
            throw ProductException.InvalidCategory();
        }

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