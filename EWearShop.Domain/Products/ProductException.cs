namespace EWearShop.Domain.Products;

public sealed class ProductException : Exception
{
    public static ProductException EmptyName => new("Product name cannot be empty.");
    public static ProductException NameTooLong => new("Product name is too long.");
    public static ProductException NegativePrice => new("Product price cannot be negative.");
    public static ProductException InvalidCurrency => new("Product currency is invalid.");
    public static ProductException InvalidImageUrl => new("Product image URL is invalid.");
    public static ProductException ImageUrlTooLong => new("Product image URL is too long.");
    public static ProductException DescriptionTooLong => new("Product description is too long.");
    public static ProductException InvalidCategory => new("Product category is invalid.");

    private ProductException(string message) : base(message) { }

    public static void ThrowIfEmptyName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw EmptyName;
        }
    }

    public static void ThrowIfNameTooLong(string name)
    {
        if (name.Length > ProductConstants.MaxNameLength)
        {
            throw NameTooLong;
        }
    }

    public static void ThrowIfNegativePrice(decimal price)
    {
        if (price < 0)
        {
            throw NegativePrice;
        }
    }

    public static void ThrowIfInvalidCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != ProductConstants.CurrencyLength)
        {
            throw InvalidCurrency;
        }
    }

    public static void ThrowIfInvalidImageUrl(string imageUrl)
    {
        if (!Uri.IsWellFormedUriString(imageUrl, UriKind.RelativeOrAbsolute))
        {
            throw InvalidImageUrl;
        }
    }

    public static void ThrowIfImageUrlTooLong(string imageUrl)
    {
        if (imageUrl.Length > ProductConstants.MaxImageUrlLength)
        {
            throw ImageUrlTooLong;
        }
    }

    public static void ThrowIfDescriptionTooLong(string description)
    {
        if (description.Length > ProductConstants.MaxDescriptionLength)
        {
            throw DescriptionTooLong;
        }
    }

    public static void ThrowIfInvalidCategory(ProductCategory category)
    {
        if (!Enum.IsDefined(category))
        {
            throw InvalidCategory;
        }
    }
}