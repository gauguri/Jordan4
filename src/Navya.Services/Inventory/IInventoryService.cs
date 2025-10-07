using Navya.Domain.Entities;

namespace Navya.Services.Inventory;

public interface IInventoryService
{
    Task<bool> ReserveAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default);

    Task ReleaseAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default);

    Task<bool> DecrementAsync(ProductVariant variant, int quantity, CancellationToken cancellationToken = default);
}
