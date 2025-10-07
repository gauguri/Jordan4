using Capco.Domain.Entities;

namespace Capco.Services.Carts;

public interface ICartService
{
    Task<Cart> GetOrCreateCartAsync(string? userId, string? guestToken, CancellationToken cancellationToken = default);

    Task<Cart> AddItemAsync(Cart cart, ProductVariant variant, int quantity, CancellationToken cancellationToken = default);

    Task<Cart> UpdateQuantityAsync(Cart cart, int cartItemId, int quantity, CancellationToken cancellationToken = default);

    Task MergeCartsAsync(string userId, string guestToken, CancellationToken cancellationToken = default);
}
