using EWearShop.DAL;
using EWearShop.Domain.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EWearShop.Api.Features.Orders.CreateOrder;

using CreateOrderResult = Results<Created, BadRequest<ProblemDetails>, ProblemHttpResult>;

public static class CreateOrderEndpoint
{
    extension(IEndpointRouteBuilder endpoints)
    {
        public IEndpointRouteBuilder MapCreateOrderEndpoint()
        {
            endpoints.MapPost("/api/orders", Handle)
                .WithTags("Orders")
                .WithName("CreateOrder")
                .WithDescription("Creates a new order.");

            return endpoints;
        }

        private static async Task<CreateOrderResult> Handle(
            IEWearShopDbContext dbContext,
            OrderFactory orderFactory,
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            Order order = orderFactory.Create(
                new OrderCustomer
                {
                    Email = request.CustomerInfo.Email,
                    FirstName =  request.CustomerInfo.FirstName,
                    LastName =  request.CustomerInfo.LastName,
                    FatherName =  request.CustomerInfo.FatherName,
                    PhoneNumber =  request.CustomerInfo.PhoneNumber,
                    Address = new OrderCustomerAddress
                    {
                        City = request.CustomerInfo.Address.City,
                        Country =  request.CustomerInfo.Address.Country,
                        Street =  request.CustomerInfo.Address.Street,
                        HouseNumber =  request.CustomerInfo.Address.HouseNumber,
                        ZipCode =  request.CustomerInfo.Address.ZipCode,
                        State =  request.CustomerInfo.Address.State,
                        ApartmentNumber =  request.CustomerInfo.Address.ApartmentNumber,
                        AdditionalInfo =  request.CustomerInfo.Address.AdditionalInfo
                    }
                },
                request.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList());

            dbContext.AddEntity(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return TypedResults.Created("/api/orders");
        }
    }
}