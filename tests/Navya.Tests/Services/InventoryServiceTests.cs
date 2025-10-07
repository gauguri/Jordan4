using Navya.Domain.Entities;
using Navya.Services.Inventory;
using Xunit;

namespace Navya.Tests.Services;

public class InventoryServiceTests
{
    [Fact]
    public async Task ReserveAsync_DecrementsInventory()
    {
        var context = TestDbContextFactory.CreateContext(nameof(ReserveAsync_DecrementsInventory));
        var variant = new ProductVariant { Id = 1, InventoryQty = 5, AllowBackorder = false, Price = 10m, Color = "White", SizeLabel = "1 lb", Sku = "SKU", ProductId = 1, IsActive = true };
        context.ProductVariants.Add(variant);
        await context.SaveChangesAsync();
        var service = new InventoryService(context);

        var result = await service.ReserveAsync(variant, 2);

        Assert.True(result);
        Assert.Equal(3, variant.InventoryQty);
    }

    [Fact]
    public async Task ReserveAsync_BlocksOversell()
    {
        var context = TestDbContextFactory.CreateContext(nameof(ReserveAsync_BlocksOversell));
        var variant = new ProductVariant { Id = 1, InventoryQty = 1, AllowBackorder = false, Price = 10m, Color = "White", SizeLabel = "1 lb", Sku = "SKU2", ProductId = 1, IsActive = true };
        context.ProductVariants.Add(variant);
        await context.SaveChangesAsync();
        var service = new InventoryService(context);

        var result = await service.ReserveAsync(variant, 5);

        Assert.False(result);
    }
}
