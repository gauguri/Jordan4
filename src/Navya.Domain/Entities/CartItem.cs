using System.ComponentModel.DataAnnotations.Schema;

namespace Navya.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductVariantId { get; set; }

    public int Qty { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPriceSnapshot { get; set; }

    public Cart Cart { get; set; } = null!;

    public ProductVariant Variant { get; set; } = null!;
}
