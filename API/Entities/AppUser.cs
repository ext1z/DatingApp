using System;

namespace API.Entities;

public class AppUser
{
    public string Id  { get; set; } = Guid.NewGuid().ToString();
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

}
