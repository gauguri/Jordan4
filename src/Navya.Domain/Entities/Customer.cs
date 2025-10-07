using System.ComponentModel.DataAnnotations;

namespace Navya.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(80)]
    public string? FirstName { get; set; }

    [MaxLength(80)]
    public string? LastName { get; set; }

    [Phone]
    public string? Phone { get; set; }

    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
