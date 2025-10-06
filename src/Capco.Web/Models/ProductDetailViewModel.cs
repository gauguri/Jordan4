using Capco.Domain.Entities;

namespace Capco.Web.Models;

public class ProductDetailViewModel
{
    public Product Product { get; set; } = null!;
    public ProductVariant? SelectedVariant { get; set; }
    public int Quantity { get; set; } = 1;
}
