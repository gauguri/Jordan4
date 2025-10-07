using Navya.Domain.Entities;
using Navya.Services.Email;
using Navya.Services.Inventory;
using Navya.Services.Orders;
using Navya.Services.Pricing;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Navya.Tests.Services;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrderFromCartAsync_GeneratesOrder()
    {
        var context = TestDbContextFactory.CreateContext(nameof(CreateOrderFromCartAsync_GeneratesOrder));
        var variant = new ProductVariant { Id = 1, Color = "White", SizeLabel = "1 lb", Price = 12m, Sku = "SKU3", ProductId = 1, InventoryQty = 10, AllowBackorder = false, IsActive = true, Product = new Product { Id = 1, Name = "White Almonds", Slug = "white", Description = "" } };
        context.Products.Add(variant.Product);
        context.ProductVariants.Add(variant);
        await context.SaveChangesAsync();

        var cart = new Cart
        {
            Items =
            {
                new CartItem { ProductVariantId = variant.Id, Variant = variant, Qty = 2, UnitPriceSnapshot = variant.Price }
            }
        };

        var inventory = new InventoryService(context);
        var pricing = new PricingService();
        var email = new StubEmailSender(new NullLogger<StubEmailSender>());
        var service = new OrderService(context, inventory, pricing, email);

        var order = await service.CreateOrderFromCartAsync(cart, new Address { Line1 = "1 Main", City = "East Hanover", State = "NJ", Zip = "07936", Country = "USA", Type = "Shipping" }, null, "guest@example.com");

        Assert.NotNull(order.OrderNumber);
        Assert.Single(order.Items);
        Assert.Equal("pending", order.Status);
    }
}
