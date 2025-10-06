using Capco.Data;
using Capco.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capco.Services.Inventory;

public class InventoryService : IInventoryService
{
    private readonly ApplicationDbContext _context;

    public InventoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ReserveAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default)
    {
        if (variant.InventoryQty < quantity && !variant.AllowBackorder)
        {
            return false;
        }

        variant.InventoryQty -= quantity;
        _context.ProductVariants.Update(variant);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task ReleaseAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default)
    {
        variant.InventoryQty += quantity;
        _context.ProductVariants.Update(variant);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DecrementAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default)
    {
        if (variant.InventoryQty < quantity && !variant.AllowBackorder)
        {
            return false;
        }

        variant.InventoryQty -= quantity;
        _context.ProductVariants.Update(variant);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
