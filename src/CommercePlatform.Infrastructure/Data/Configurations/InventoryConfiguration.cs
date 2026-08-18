using CommercePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePlatform.Infrastructure.Data.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.AvailableQuantity)
            .IsRequired();

        builder.Property(x => x.ReservedQuantity)
            .IsRequired();

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.HasOne(x => x.Product)
            .WithOne(x => x.Inventory)
            .HasForeignKey<Inventory>(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}