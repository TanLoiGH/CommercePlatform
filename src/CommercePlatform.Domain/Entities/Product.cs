using CommercePlatform.Domain.Domain.Exceptions;
using CommercePlatform.Domain.Enums;

namespace CommercePlatform.Domain.Entities;

public class Product : BaseEntity
{
    public string Sku { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public ProductStatus Status { get; private set; }

    public Inventory? Inventory { get; private set; }

    public ICollection<CartItem> CartItems { get; private set; } = [];

    public ICollection<OrderItem> OrderItems { get; private set; } = [];

    public ICollection<Reservation> Reservations { get; private set; } = [];

    public void ChangePrice(decimal price)
    {
        if (price < 0)
            throw new DomainException("Product price cannot be negative.");

        Price = price;
        Touch();
    }

    public void Activate()
    {
        Status = ProductStatus.Active;
        Touch();
    }

    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
        Touch();
    }

    public static Product Create(
    string sku,
    string name,
    string? description,
    decimal price)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException("SKU is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required.");

        if (price < 0)
            throw new DomainException("Product price cannot be negative.");

        return new Product
        {
            Sku = sku,
            Name = name,
            Description = description,
            Price = price,
            Status = ProductStatus.Active
        };
    }
}