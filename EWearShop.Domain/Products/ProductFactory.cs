namespace EWearShop.Domain.Products;

public sealed class ProductFactory(TimeProvider timeProvider)
{
    private readonly TimeProvider _timeProvider = timeProvider;

    public Product Create(
        Guid id,
        string name,
        string description,
        ProductCategory category,
        decimal price,
        string currency,
        string imageUrl)
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
            Id = id,
            Name = name,
            Description = description,
            Category = category,
            Price = price,
            Currency = currency,
            ImageUrl = imageUrl,
            CreatedAt = _timeProvider.GetUtcNow(),
            UpdatedAt = _timeProvider.GetUtcNow()
        };
    }
}