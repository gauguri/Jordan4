using Capco.Services.Cart;
using Capco.Services.Catalog;
using Capco.Services.Email;
using Capco.Services.Inventory;
using Capco.Services.Orders;
using Capco.Services.Payments;
using Capco.Services.Pricing;
using Microsoft.Extensions.DependencyInjection;

namespace Capco.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCapcoServices(this IServiceCollection services)
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
