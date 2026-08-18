using CommercePlatform.Domain.Domain.Exceptions;
using CommercePlatform.Domain.Enums;

namespace CommercePlatform.Domain.Entities;

public class PaymentAttempt : BaseEntity
{
    public Guid OrderId { get; private set; }

    public decimal Amount { get; private set; }

    public PaymentStatus Status { get; private set; }

    public PaymentMethod Method { get; private set; }

    public string? ProviderTransactionId { get; private set; }

    public string IdempotencyKey { get; private set; } = null!;

    public Order Order { get; private set; } = null!;

    public void MarkAsProcessing()
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException(
                "Payment cannot be processed.");

        Status = PaymentStatus.Processing;
        Touch();
    }

    public void MarkAsSucceeded(string providerTransactionId)
    {
        if (Status != PaymentStatus.Processing)
            throw new DomainException(
                "Payment must be processing.");

        if (string.IsNullOrWhiteSpace(providerTransactionId))
            throw new DomainException(
                "Provider transaction ID is required.");

        ProviderTransactionId = providerTransactionId;
        Status = PaymentStatus.Succeeded;

        Touch();
    }

    public void MarkAsFailed()
    {
        if (Status != PaymentStatus.Pending &&
            Status != PaymentStatus.Processing)
        {
            throw new DomainException(
                "Payment cannot be marked as failed.");
        }

        Status = PaymentStatus.Failed;
        Touch();
    }

    public void Refund()
    {
        if (Status != PaymentStatus.Succeeded)
            throw new DomainException(
                "Only succeeded payment can be refunded.");

        Status = PaymentStatus.Refunded;
        Touch();
    }


    public static PaymentAttempt Create(
    Order order,
    decimal amount,
    PaymentMethod method,
    string idempotencyKey)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (amount <= 0)
            throw new DomainException(
                "Payment amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(idempotencyKey))
            throw new DomainException(
                "Idempotency key is required.");

        return new PaymentAttempt
        {
            Order = order,
            OrderId = order.Id,
            Amount = amount,
            Method = method,
            Status = PaymentStatus.Pending,
            IdempotencyKey = idempotencyKey
        };
    }
}