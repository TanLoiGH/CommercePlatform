using CommercePlatform.Domain.Domain.Exceptions;
using CommercePlatform.Domain.Enums;

namespace CommercePlatform.Domain.Entities;

public class Reservation : BaseEntity
{
    private Reservation()
    {
    }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public ReservationStatus Status { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public Order Order { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new DomainException("Reservation cannot be confirmed.");

        if (DateTime.UtcNow >= ExpiresAt)
            throw new DomainException("Reservation has expired.");

        Status = ReservationStatus.Confirmed;
        Touch();
    }

    public void Release()
    {
        if (Status != ReservationStatus.Pending)
            throw new DomainException("Reservation cannot be released.");

        Status = ReservationStatus.Released;
        Touch();
    }

    public void Expire()
    {
        if (Status != ReservationStatus.Pending)
            throw new DomainException("Reservation cannot expire.");

        Status = ReservationStatus.Expired;
        Touch();
    }


    public static Reservation Create(
    Order order,
    Product product,
    int quantity,
    DateTime expiresAt)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(product);

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        if (expiresAt <= DateTime.UtcNow)
            throw new DomainException(
                "Reservation expiration must be in the future.");

        return new Reservation
        {
            Order = order,
            OrderId = order.Id,
            Product = product,
            ProductId = product.Id,
            Quantity = quantity,
            Status = ReservationStatus.Pending,
            ExpiresAt = expiresAt
        };
    }
}