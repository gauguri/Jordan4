using Capco.Domain.Entities;

namespace Capco.Services.Pricing;

public class PricingService : IPricingService
{
    private static readonly Dictionary<string, (decimal Value, bool Percent)> _promos = new(StringComparer.OrdinalIgnoreCase)
    {
        ["WELCOME10"] = (0.10m, true),
        ["SPRING5"] = (5m, false)
    };

    public decimal CalculateSubtotal(IEnumerable<CartItem> items)
    {
        return items.Sum(i => i.UnitPriceSnapshot * i.Qty);
    }

    public decimal ApplyDiscount(decimal subtotal, string? promoCode, out string? message)
    {
        message = null;
        if (string.IsNullOrWhiteSpace(promoCode))
        {
            return subtotal;
        }

        if (_promos.TryGetValue(promoCode.Trim(), out var discount))
        {
            var amount = discount.Percent ? subtotal * discount.Value : discount.Value;
            message = discount.Percent ? "Applied 10% welcome discount." : "Applied $5 savings.";
            return Math.Max(0, subtotal - amount);
        }

        message = "Promo code not recognized.";
        return subtotal;
    }
}
