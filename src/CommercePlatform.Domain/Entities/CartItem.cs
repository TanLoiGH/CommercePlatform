using CommercePlatform.Domain.Domain.Exceptions;

namespace CommercePlatform.Domain.Entities;

public class CartItem : BaseEntity
{
    private CartItem()
    {
    }

    public Guid CartId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Cart Cart { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    internal static CartItem Create(
        Cart cart,
        Product product,
        int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        return new CartItem
        {
            Cart = cart,
            CartId = cart.Id,
            Product = product,
            ProductId = product.Id,
            Quantity = quantity
        };
    }

    internal void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        Quantity += quantity;
        Touch();
    }

    internal void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        Quantity = quantity;
        Touch();
    }
}