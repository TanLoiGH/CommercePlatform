using CommercePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommercePlatform.Application.Abstractions.Persistence;

public interface ICommerceDbContext
{
    DbSet<User> Users { get; }

    DbSet<Product> Products { get; }

    DbSet<Inventory> Inventories { get; }

    DbSet<Cart> Carts { get; }

    DbSet<CartItem> CartItems { get; }

    DbSet<Order> Orders { get; }

    DbSet<OrderItem> OrderItems { get; }

    DbSet<Reservation> Reservations { get; }

    DbSet<PaymentAttempt> PaymentAttempts { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}