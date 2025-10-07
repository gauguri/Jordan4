using Microsoft.AspNetCore.Identity;

namespace Navya.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
