using Navya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Navya.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasIndex(o => o.OrderNumber).IsUnique();

        builder.Property(o => o.Subtotal).HasPrecision(18, 2);
        builder.Property(o => o.Shipping).HasPrecision(18, 2);
        builder.Property(o => o.Tax).HasPrecision(18, 2);
        builder.Property(o => o.Total).HasPrecision(18, 2);

        builder.HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.ShippingAddress)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.BillingAddress)
            .WithMany()
            .HasForeignKey(o => o.BillingAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
