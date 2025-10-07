using Navya.Data;
using Navya.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Navya.Services.Carts;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetOrCreateCartAsync(string? userId, string? guestToken, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Variant)
            .ThenInclude(v => v.Product)
            .Include(c => c.Customer)
            .AsQueryable();

        Cart? cart = null;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            cart = await query.FirstOrDefaultAsync(c => c.CustomerId != null && c.Customer!.UserId == userId, cancellationToken);
        }

        if (cart == null && !string.IsNullOrWhiteSpace(guestToken))
        {
            cart = await query.FirstOrDefaultAsync(c => c.GuestToken == guestToken, cancellationToken);
        }

        if (cart == null)
        {
            cart = new Cart
            {
                GuestToken = guestToken,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return cart;
    }

    public async Task<Cart> AddItemAsync(Cart cart, ProductVariant variant, int quantity, CancellationToken cancellationToken = default)
    {
        var existing = cart.Items.FirstOrDefault(i => i.ProductVariantId == variant.Id);
        if (existing is null)
        {
            existing = new CartItem
            {
                ProductVariantId = variant.Id,
                Qty = quantity,
                UnitPriceSnapshot = variant.Price
            };
            cart.Items.Add(existing);
        }
        else
        {
            existing.Qty += quantity;
        }

        cart.UpdatedAt = DateTime.UtcNow;
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
        await _context.Entry(cart).Collection(c => c.Items).Query().Include(i => i.Variant).ThenInclude(v => v.Product).LoadAsync(cancellationToken);
        return cart;
    }

    public async Task<Cart> UpdateQuantityAsync(Cart cart, int cartItemId, int quantity, CancellationToken cancellationToken = default)
    {
        var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
        if (item is null)
        {
            throw new InvalidOperationException("Cart item not found.");
        }

        if (quantity <= 0)
        {
            cart.Items.Remove(item);
            _context.CartItems.Remove(item);
        }
        else
        {
            item.Qty = quantity;
            _context.CartItems.Update(item);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task MergeCartsAsync(string userId, string guestToken, CancellationToken cancellationToken = default)
    {
        var userCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Customer != null && c.Customer.UserId == userId, cancellationToken);

        var guestCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.GuestToken == guestToken, cancellationToken);

        if (guestCart == null)
        {
            return;
        }

        if (userCart == null)
        {
            guestCart.GuestToken = null;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        foreach (var item in guestCart.Items)
        {
            var existing = userCart.Items.FirstOrDefault(i => i.ProductVariantId == item.ProductVariantId);
            if (existing is null)
            {
                existing = new CartItem
                {
                    ProductVariantId = item.ProductVariantId,
                    Qty = item.Qty,
                    UnitPriceSnapshot = item.UnitPriceSnapshot
                };
                userCart.Items.Add(existing);
            }
            else
            {
                existing.Qty += item.Qty;
            }
        }

        _context.Carts.Remove(guestCart);
        userCart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
