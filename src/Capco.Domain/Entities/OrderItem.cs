using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capco.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductVariantId { get; set; }

    [Required, MaxLength(160)]
    public string NameSnapshot { get; set; } = string.Empty;

    [MaxLength(40)]
    public string? ColorSnapshot { get; set; }

    [MaxLength(60)]
    public string? SizeSnapshot { get; set; }

    public int Qty { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPriceSnapshot { get; set; }

    public Order Order { get; set; } = null!;

    public ProductVariant Variant { get; set; } = null!;
}
