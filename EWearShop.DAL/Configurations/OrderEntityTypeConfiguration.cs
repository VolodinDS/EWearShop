using EWearShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWearShop.DAL.Configurations;

public sealed class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.CustomerInfo, customerInfoBuilder =>
        {
            customerInfoBuilder.Property(x => x.FirstName).IsRequired();
            customerInfoBuilder.Property(x => x.LastName).IsRequired();
            customerInfoBuilder.Property(x => x.Email).IsRequired();
            customerInfoBuilder.Property(x => x.PhoneNumber).IsRequired(false);
            customerInfoBuilder.Property(x => x.Address).IsRequired();
            customerInfoBuilder.OwnsOne(x => x.Address, addressBuilder =>
            {
                addressBuilder.Property(x => x.Country).IsRequired();
                addressBuilder.Property(x => x.City).IsRequired();
                addressBuilder.Property(x => x.Street).IsRequired();
                addressBuilder.Property(x => x.ZipCode).IsRequired(false);
            });
        });

        builder.HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<OrderItem>();
    }
}