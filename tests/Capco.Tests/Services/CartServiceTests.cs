using System.Linq;
using Capco.Domain.Entities;
using Capco.Services.Carts;
using Capco.Services.Pricing;
using Xunit;

namespace Capco.Tests.Services;

public class CartServiceTests
{
    [Fact]
    public async Task MergeCartsAsync_AppendsGuestItems()
    {
        var context = TestDbContextFactory.CreateContext(nameof(MergeCartsAsync_AppendsGuestItems));
        var variant = new ProductVariant { Id = 1, Price = 10m, Sku = "SKU4", Color = "White", SizeLabel = "1 lb", ProductId = 1, InventoryQty = 10, AllowBackorder = true, IsActive = true, Product = new Product { Id = 1, Name = "White", Slug = "white" } };
        context.Products.Add(variant.Product);
        context.ProductVariants.Add(variant);
        var customer = new Customer { Id = 1, UserId = "user", Email = "user@example.com" };
        context.Customers.Add(customer);
        var userCart = new Cart { CustomerId = 1, Customer = customer, Items = { new CartItem { ProductVariantId = 1, Qty = 1, UnitPriceSnapshot = 10m, Variant = variant } } };
        var guestCart = new Cart { GuestToken = "guest", Items = { new CartItem { ProductVariantId = 1, Qty = 2, UnitPriceSnapshot = 10m, Variant = variant } } };
        context.Carts.AddRange(userCart, guestCart);
        await context.SaveChangesAsync();

        var service = new CartService(context);
        await service.MergeCartsAsync("user", "guest");

        Assert.Single(context.Carts.Where(c => c.CustomerId == 1));
        Assert.Equal(3, context.Carts.First(c => c.CustomerId == 1).Items.Sum(i => i.Qty));
    }
}
