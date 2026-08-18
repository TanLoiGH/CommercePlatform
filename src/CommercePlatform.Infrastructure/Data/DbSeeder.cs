using CommercePlatform.Domain.Entities;
using CommercePlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CommercePlatform.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(CommerceDbContext db)
    {
        if (await db.Users.AnyAsync())
            return;
        var user = User.Create(
            "demo@commerce.local",
            "DEMO_PASSWORD_HASH");

        var product1 = Product.Create(
            "SKU-001",
            "Mechanical Keyboard",
            "Mechanical keyboard for testing",
            1_500_000m);

        var product2 = Product.Create(
            "SKU-002",
            "Wireless Mouse",
            "Wireless mouse for testing",
            500_000m);

        db.Users.Add(user);
        db.Products.AddRange(product1, product2);

        await db.SaveChangesAsync();

        var inventory1 = Inventory.Create(product1.Id, 100);
        var inventory2 = Inventory.Create(product2.Id, 50);

        db.Inventories.AddRange(inventory1, inventory2);

        await db.SaveChangesAsync();
    }
}