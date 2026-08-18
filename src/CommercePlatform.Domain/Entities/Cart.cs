using CommercePlatform.Domain.Domain.Exceptions;
using CommercePlatform.Domain.Enums;
namespace CommercePlatform.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid UserId { get; private set; }

    public CartStatus Status { get; private set; }

    public User User { get; private set; } = null!;

    public ICollection<CartItem> Items { get; private set; } = [];


    public void AddItem(Product product, int quantity)
    {
        if (Status != CartStatus.Active)
            throw new DomainException("Cart is not active.");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        var existingItem = Items.FirstOrDefault(x => x.ProductId == product.Id);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
            return;
        }

        Items.Add(CartItem.Create(this, product, quantity));

        Touch();
    }

    public void RemoveItem(Guid productId)
    {
        if (Status != CartStatus.Active)
            throw new DomainException("Cart is not active.");

        var item = Items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            throw new DomainException("Product is not in the cart.");

        Items.Remove(item);

        Touch();
    }

    public void Checkout()
    {
        if (Status != CartStatus.Active)
            throw new DomainException("Cart is not active.");

        if (Items.Count == 0)
            throw new DomainException("Cart cannot be empty.");

        Status = CartStatus.CheckedOut;
        Touch();
    }


    public static Cart Create(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new Cart
        {
            User = user,
            UserId = user.Id,
            Status = CartStatus.Active
        };
    }
}