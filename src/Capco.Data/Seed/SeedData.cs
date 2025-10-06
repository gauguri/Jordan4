using Capco.Data;
using Capco.Domain.Entities;
using Capco.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Capco.Data.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await EnsureRolesAsync(roleManager, logger);
        await EnsureAdminAsync(userManager, logger);

        if (!context.Products.Any())
        {
            var products = BuildProducts();
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

        if (!context.ContentBlocks.Any())
        {
            context.ContentBlocks.AddRange(
                new ContentBlock { Key = "about", Html = "<p>Capco Enterprises Inc. crafts premium Jordan Almonds in East Hanover, New Jersey.</p>" },
                new ContentBlock { Key = "wholesale", Html = "<p>Contact our wholesale concierge for custom palettes and packaging.</p>" },
                new ContentBlock { Key = "contact", Html = "<p>Call us at (973) 555-0110 or visit 12 Almond Way, East Hanover, NJ.</p>" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        foreach (var role in new[] { "Admin", "Customer" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create role {Role}: {Errors}", role, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

    private static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager, ILogger logger)
    {
        var admin = await userManager.FindByEmailAsync("admin@capco.local");
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = "admin@capco.local",
                Email = "admin@capco.local",
                EmailConfirmed = true,
                FirstName = "Capco",
                LastName = "Admin"
            };

            var result = await userManager.CreateAsync(admin, "Pass!123");
            if (!result.Succeeded)
            {
                logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    private static IEnumerable<Product> BuildProducts()
    {
        var colors = new[]
        {
            (Name: "White", Hex: "#FFFFFF"),
            (Name: "Pink", Hex: "#F7C7D9"),
            (Name: "Blue", Hex: "#BFD7FF"),
            (Name: "Yellow", Hex: "#FFF2B2"),
            (Name: "Green", Hex: "#CDEBC8")
        };

        var sizes = new[]
        {
            (Label: "1 lb pouch", Price: 12.99m, Weight: 454, SkuSuffix: "-1LB"),
            (Label: "5 lb box", Price: 49.99m, Weight: 2268, SkuSuffix: "-5LB"),
            (Label: "10 lb box", Price: 89.99m, Weight: 4536, SkuSuffix: "-10LB")
        };

        var products = new List<Product>();
        foreach (var color in colors)
        {
            var product = new Product
            {
                Name = $"Jordan Almonds – {color.Name}",
                Slug = $"jordan-almonds-{color.Name.ToLowerInvariant()}",
                Description = $"Classic {color.Name.ToLowerInvariant()} Jordan almonds perfected by Capco Confectionery.",
                Collection = "Pastel",
                IsActive = true,
                Images =
                {
                    new ProductImage
                    {
                        Url = $"/images/almonds-{color.Name.ToLowerInvariant()}.jpg",
                        Alt = $"{color.Name} Jordan Almonds",
                        SortOrder = 1,
                        IsPrimary = true
                    }
                }
            };

            int variantIndex = 1;
            foreach (var size in sizes)
            {
                product.Variants.Add(new ProductVariant
                {
                    Color = color.Name,
                    SizeLabel = size.Label,
                    Sku = $"CAPCO-{color.Name.Substring(0, Math.Min(3, color.Name.Length)).ToUpperInvariant()}{size.SkuSuffix}",
                    Price = size.Price,
                    WeightGrams = size.Weight,
                    UPC = $"000{variantIndex:0000000000}",
                    InventoryQty = 200 - (variantIndex * 10),
                    AllowBackorder = true,
                    IsActive = true
                });
                variantIndex++;
            }

            products.Add(product);
        }

        return products;
    }
}
