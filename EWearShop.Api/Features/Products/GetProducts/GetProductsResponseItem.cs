using System.Text.Json.Serialization;
using EWearShop.Domain.Products;

namespace EWearShop.Api.Features.Products.GetProducts;

public record GetProductsResponseItem
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    [JsonConverter(typeof(JsonStringEnumConverter<ProductCategory>))]
    public required ProductCategory Category { get; init; }
    public required decimal Price { get; init; }
    public required string Currency { get; init; }
    public required string ImageUrl { get; init; }
};