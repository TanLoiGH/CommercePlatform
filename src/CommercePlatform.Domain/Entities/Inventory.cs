using CommercePlatform.Domain.Domain.Exceptions;

namespace CommercePlatform.Domain.Entities;

public class Inventory
{
    private Inventory()
    {
    }

    public Guid ProductId { get; private set; }

    public int AvailableQuantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public byte[] RowVersion { get; private set; } = [];

    public Product Product { get; private set; } = null!;



    public void Increase(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        AvailableQuantity += quantity;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (quantity > AvailableQuantity)
            throw new DomainException("Insufficient inventory.");

        AvailableQuantity -= quantity;
        ReservedQuantity += quantity;
    }

    public void Release(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (quantity > ReservedQuantity)
            throw new DomainException("Cannot release more than reserved quantity.");

        ReservedQuantity -= quantity;
        AvailableQuantity += quantity;
    }

    public void ConfirmReservation(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (quantity > ReservedQuantity)
            throw new DomainException("Cannot confirm more than reserved quantity.");

        ReservedQuantity -= quantity;
    }


    public static Inventory Create(Guid productId, int quantity)
    {
        if (productId == Guid.Empty)
            throw new DomainException("ProductId is required.");

        if (quantity < 0)
            throw new DomainException("Quantity cannot be negative.");

        return new Inventory
        {
            ProductId = productId,
            AvailableQuantity = quantity,
            ReservedQuantity = 0
        };
    }
}