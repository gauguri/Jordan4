namespace Capco.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? GuestToken { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Customer? Customer { get; set; }

    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
