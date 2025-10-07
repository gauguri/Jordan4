using Navya.Domain.Entities;
using Navya.Services.Pricing;
using Xunit;

namespace Navya.Tests.Services;

public class PricingServiceTests
{
    [Fact]
    public void CalculateSubtotal_SumsLineItems()
    {
        var service = new PricingService();
        var items = new List<CartItem>
        {
            new() { Qty = 2, UnitPriceSnapshot = 10m },
            new() { Qty = 1, UnitPriceSnapshot = 5.5m }
        };

        var subtotal = service.CalculateSubtotal(items);

        Assert.Equal(25.5m, subtotal);
    }

    [Fact]
    public void ApplyDiscount_Welcome10_AppliesPercentage()
    {
        var service = new PricingService();
        var subtotal = 100m;

        var total = service.ApplyDiscount(subtotal, "WELCOME10", out var message);

        Assert.Equal(90m, total);
        Assert.NotNull(message);
    }
}
