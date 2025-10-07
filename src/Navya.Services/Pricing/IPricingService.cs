using Navya.Domain.Entities;

namespace Navya.Services.Pricing;

public interface IPricingService
{
    decimal CalculateSubtotal(IEnumerable<CartItem> items);

    decimal ApplyDiscount(decimal subtotal, string? promoCode, out string? message);
}
