using System.ComponentModel.DataAnnotations;

namespace Navya.Domain.Entities;

public class Address
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    [Required, MaxLength(160)]
    public string Line1 { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? Line2 { get; set; }

    [Required, MaxLength(80)]
    public string City { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string State { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Zip { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string Country { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string Type { get; set; } = string.Empty;

    public Customer? Customer { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
