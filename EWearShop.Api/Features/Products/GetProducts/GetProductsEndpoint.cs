using EWearShop.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EWearShop.Api.Features.Products.GetProducts;

using GetProductsResult = Results<Ok<GetProductsResponseItem[]>, ProblemHttpResult>;

internal static class GetProductsEndpoint
{
    extension(IEndpointRouteBuilder endpoints)
    {
        public IEndpointRouteBuilder MapGetProductsEndpoint()
        {
            endpoints.MapGet("/api/products", Handle)
                .WithTags("Products")
                .WithName("GetProducts")
                .WithDescription("Gets all products.");

            return endpoints;
        }

        private static async Task<GetProductsResult> Handle(IEWearShopDbContext dbContext, CancellationToken cancellationToken)
        {
            GetProductsResponseItem[] products = await dbContext.Products.Where(p => !p.IsDeleted)
                .Select(product => new GetProductsResponseItem
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    Currency = product.Currency,
                    ImageUrl = product.ImageUrl,
                })
                .ToArrayAsync(cancellationToken);

            return TypedResults.Ok(products);
        }
    }
}