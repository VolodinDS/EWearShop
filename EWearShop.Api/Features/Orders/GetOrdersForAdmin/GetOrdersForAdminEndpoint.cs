using EWearShop.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EWearShop.Api.Features.Orders.GetOrdersForAdmin;

using GetOrdersForAdminResult = Results<Ok<GetOrdersForAdminResponseItem[]>, ProblemHttpResult>;

internal static class GetOrdersForAdminEndpoint
{
    extension(IEndpointRouteBuilder endpoints)
    {
        public IEndpointRouteBuilder MapGetOrdersForAdminEndpoint()
        {
            endpoints.MapGet("/api/admin/orders", Handle)
                .WithTags("Orders")
                .WithName("GetOrdersForAdmin")
                .WithDescription("Gets all orders for admin purposes.");

            return endpoints;
        }
    }

    private static async Task<GetOrdersForAdminResult> Handle(IEWearShopDbContext dbContext, CancellationToken cancellationToken)
    {
        GetOrdersForAdminResponseItem[] orders = await dbContext.Orders
            .AsNoTracking()
            .Select(order => new GetOrdersForAdminResponseItem
            {
                Id = order.Id,
                CustomerInfo =  order.CustomerInfo,
                OrderDate =  order.OrderDate,
                Items = order.Items.Select(orderItem => new GetOrdersForAdminResponseItem.Item
                {
                    Id = orderItem.ProductId,
                    Currency = orderItem.Product.Currency,
                    Description = orderItem.Product.Description,
                    ImageUrl = orderItem.Product.ImageUrl,
                    Name = orderItem.Product.Name,
                    Price = orderItem.Product.Price,
                    Quantity = orderItem.Quantity
                }).ToArray()
            })
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(orders);
    }
}