using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Navya.Domain.Entities;

public class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Required, MaxLength(40)]
    public string Color { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string SizeLabel { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string Sku { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CompareAtPrice { get; set; }

    public int WeightGrams { get; set; }

    [MaxLength(32)]
    public string? UPC { get; set; }

    public int InventoryQty { get; set; }

    public bool AllowBackorder { get; set; }

    public bool IsActive { get; set; } = true;

    public Product Product { get; set; } = null!;
}
