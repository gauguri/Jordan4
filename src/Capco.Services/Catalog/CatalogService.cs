using Capco.Data;
using Capco.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capco.Services.Catalog;

public class CatalogService : ICatalogService
{
    private readonly ApplicationDbContext _context;

    public CatalogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetProductsAsync(string? search, string? color, string? size, string? collection, string? sort, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Include(p => p.Images)
            .Include(p => p.Variants)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)));
        }

        if (!string.IsNullOrWhiteSpace(color))
        {
            query = query.Where(p => p.Variants.Any(v => v.Color == color));
        }

        if (!string.IsNullOrWhiteSpace(size))
        {
            query = query.Where(p => p.Variants.Any(v => v.SizeLabel == size));
        }

        if (!string.IsNullOrWhiteSpace(collection))
        {
            query = query.Where(p => p.Collection == collection);
        }

        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Variants.Min(v => v.Price)),
            "price_desc" => query.OrderByDescending(p => p.Variants.Max(v => v.Price)),
            _ => query.OrderBy(p => p.Name)
        };

        var total = await query.CountAsync(cancellationToken);
        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (products, total);
    }

    public Task<Product?> GetProductBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return _context.Products
            .Include(p => p.Images)
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);
    }
}
