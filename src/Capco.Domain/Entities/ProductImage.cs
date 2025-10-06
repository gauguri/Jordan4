using System.ComponentModel.DataAnnotations;

namespace Capco.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Required, MaxLength(512)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? Alt { get; set; }

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; }

    public Product Product { get; set; } = null!;
}
