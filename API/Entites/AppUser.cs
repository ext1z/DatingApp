using System;

namespace API.Entites;

public class AppUser
{
    public string Id  { get; set; } = Guid.NewGuid().ToString();
    public string? DisplayName { get; set; }
    public string? Email { get; set; }

}
