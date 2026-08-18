using CommercePlatform.Domain.Domain.Exceptions;

namespace CommercePlatform.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal SubTotal { get; private set; }

    public Order Order { get; private set; } = null!;

    public Product Product { get; private set; } = null!;


    public static OrderItem Create(
    Order order,
    Product product,
    int quantity)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        return new OrderItem
        {
            Order = order,
            OrderId = order.Id,
            Product = product,
            ProductId = product.Id,
            Quantity = quantity,
            UnitPrice = product.Price,
            SubTotal = product.Price * quantity
        };
    }
}