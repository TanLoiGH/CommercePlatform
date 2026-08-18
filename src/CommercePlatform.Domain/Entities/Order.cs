using CommercePlatform.Domain.Domain.Exceptions;
using CommercePlatform.Domain.Enums;

namespace CommercePlatform.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public User User { get; private set; } = null!;

    public ICollection<OrderItem> Items { get; private set; } = [];

    public ICollection<Reservation> Reservations { get; private set; } = [];

    public ICollection<PaymentAttempt> PaymentAttempts { get; private set; } = [];

    public void MarkAsPaid()
    {
        EnsureStatus(OrderStatus.PendingPayment);

        Status = OrderStatus.Paid;
        Touch();
    }

    public void StartProcessing()
    {
        EnsureStatus(OrderStatus.Paid);

        Status = OrderStatus.Processing;
        Touch();
    }

    public void Complete()
    {
        EnsureStatus(OrderStatus.Processing);

        Status = OrderStatus.Completed;
        Touch();
    }

    public void Cancel()
    {
        if (Status != OrderStatus.PendingPayment &&
            Status != OrderStatus.Paid)
        {
            throw new DomainException(
                $"Order cannot be cancelled from status {Status}.");
        }

        Status = OrderStatus.Cancelled;
        Touch();
    }

    public void Refund()
    {
        if (Status != OrderStatus.Paid &&
            Status != OrderStatus.Completed)
        {
            throw new DomainException(
                $"Order cannot be refunded from status {Status}.");
        }

        Status = OrderStatus.Refunded;
        Touch();
    }

    private void EnsureStatus(OrderStatus expected)
    {
        if (Status != expected)
        {
            throw new DomainException(
                $"Order must be {expected}.");
        }
    }


    public static Order Create(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new Order
        {
            User = user,
            UserId = user.Id,
            Status = OrderStatus.PendingPayment
        };
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        var item = OrderItem.Create(
            this,
            product,
            quantity);

        Items.Add(item);

        TotalAmount += item.SubTotal;

        Touch();
    }
}