using EWearShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWearShop.DAL.Configurations;

public sealed class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => new { oi.OrderId, oi.ProductId });

        builder.Property(oi => oi.Quantity)
            .IsRequired();
    }
}