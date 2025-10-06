using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capco.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    [Required, MaxLength(32)]
    public string OrderNumber { get; set; } = string.Empty;

    public int? CustomerId { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(32)]
    public string Status { get; set; } = "pending";

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Shipping { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [MaxLength(160)]
    public string? StripePaymentIntentId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? ShippingAddressId { get; set; }

    public int? BillingAddressId { get; set; }

    public Customer? Customer { get; set; }

    public Address? ShippingAddress { get; set; }

    public Address? BillingAddress { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
