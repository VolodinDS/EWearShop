using EWearShop.Domain.Orders;
using EWearShop.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWearShop.DAL.Configurations;

public sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(ProductConstants.MaxNameLength);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(ProductConstants.CurrencyLength);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(ProductConstants.MaxImageUrlLength);

        builder.Property(p => p.Description)
            .HasMaxLength(ProductConstants.MaxDescriptionLength);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasMany(p => p.Orders)
            .WithMany(p => p.Products)
            .UsingEntity<OrderItem>();
    }
}