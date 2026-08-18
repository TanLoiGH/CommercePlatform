using CommercePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePlatform.Infrastructure.Data.Configurations;

public class PaymentAttemptConfiguration
    : IEntityTypeConfiguration<PaymentAttempt>
{
    public void Configure(EntityTypeBuilder<PaymentAttempt> builder)
    {
        builder.ToTable("PaymentAttempts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Method)
            .IsRequired();

        builder.Property(x => x.ProviderTransactionId)
            .HasMaxLength(200);

        builder.Property(x => x.IdempotencyKey)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.OrderId);

        builder.HasIndex(x => x.IdempotencyKey)
            .IsUnique();

        builder.HasIndex(x => x.ProviderTransactionId)
            .IsUnique()
            .HasFilter("[ProviderTransactionId] IS NOT NULL");

        builder.HasOne(x => x.Order)
            .WithMany(x => x.PaymentAttempts)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}