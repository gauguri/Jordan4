using Navya.Services.Carts;
using Navya.Services.Catalog;
using Navya.Services.Email;
using Navya.Services.Inventory;
using Navya.Services.Orders;
using Navya.Services.Payments;
using Navya.Services.Pricing;
using Microsoft.Extensions.DependencyInjection;

namespace Navya.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNavyaServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddSingleton<IPricingService, PricingService>();
        services.AddSingleton<IEmailSender, StubEmailSender>();
        services.AddScoped<IStripePaymentService, StripePaymentService>();
        services.AddOptions<StripeOptions>();
        return services;
    }
}
