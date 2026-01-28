namespace EWearShop.Domain.Products;

public sealed class ProductException : Exception
{
    private ProductException(string message) : base(message) { }

    public static ProductException EmptyName() => new("Product name cannot be empty.");
    public static ProductException NegativePrice() => new("Product price cannot be negative.");
    public static ProductException InvalidCurrency() => new("Product currency is invalid.");
    public static ProductException InvalidImageUrl() => new("Product image URL is invalid.");
    public static ProductException DescriptionTooLong() => new("Product description is too long.");
    public static ProductException InvalidCategory() => new("Product category is invalid.");
}