using CommercePlatform.Application.Abstractions.Persistence;
using CommercePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommercePlatform.Infrastructure.Data;

public class CommerceDbContext(DbContextOptions<CommerceDbContext> options)
    : DbContext(options), ICommerceDbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    public DbSet<Inventory> Inventories => Set<Inventory>();

    public DbSet<Reservation> Reservations => Set<Reservation>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<PaymentAttempt> PaymentAttempts => Set<PaymentAttempt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(  
            typeof(CommerceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
/* dùng ApplyConfigurationsFromAssembly thay vì nhét hết vào OnModelCreating
        thì ta tách với mỗi entity ra một file riêng để dễ quản lý. 
        vd: UserConfiguration
            ProductConfiguration
            OrderConfiguration
*/
    }
}   