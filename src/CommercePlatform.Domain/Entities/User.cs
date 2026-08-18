using System;
using System.Collections.Generic;
using System.Text;
using CommercePlatform.Domain.Enums;

namespace CommercePlatform.Domain.Entities;

public class User : BaseEntity
{
    private User()
    {
    }
    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public UserStatus Status { get; private set; }

    public Cart? Cart { get; private set; }

    public ICollection<Order> Orders { get; private set; } = [];
    public static User Create(
    string email,
    string passwordHash)
    {
        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            Status = UserStatus.Active
        };
    }
}