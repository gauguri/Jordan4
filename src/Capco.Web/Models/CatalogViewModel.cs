using Capco.Domain.Entities;

namespace Capco.Web.Models;

public class CatalogViewModel
{
    public IReadOnlyList<Product> Products { get; set; } = Array.Empty<Product>();
    public int TotalCount { get; set; }
    public string? Search { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string? Collection { get; set; }
    public string? Sort { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<string> Colors { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Sizes { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Collections { get; set; } = Array.Empty<string>();
}
