using Capco.Domain.Entities;

namespace Capco.Services.Catalog;

public interface ICatalogService
{
    Task<(IReadOnlyList<Product> Products, int TotalCount)> GetProductsAsync(string? search, string? color, string? size, string? collection, string? sort, int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Product?> GetProductBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
