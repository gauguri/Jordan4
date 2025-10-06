using Capco.Domain.Entities;
using Microsoft.Extensions.Options;
using Stripe;

namespace Capco.Services.Payments;

public class StripeOptions
{
    public string? SecretKey { get; set; }
    public string? PublishableKey { get; set; }
}

public class StripePaymentService : IStripePaymentService
{
    private readonly PaymentIntentService _paymentIntentService;

    public StripePaymentService(IOptions<StripeOptions> options)
    {
        StripeConfiguration.ApiKey = options.Value.SecretKey;
        _paymentIntentService = new PaymentIntentService();
    }

    public async Task<PaymentIntent> CreateOrUpdatePaymentIntentAsync(Order order, CancellationToken cancellationToken = default)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(order.Total * 100),
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                ["orderNumber"] = order.OrderNumber
            }
        };

        if (!string.IsNullOrEmpty(order.StripePaymentIntentId))
        {
            var updateOptions = new PaymentIntentUpdateOptions
            {
                Amount = options.Amount,
                Metadata = options.Metadata
            };

            return await _paymentIntentService.UpdateAsync(order.StripePaymentIntentId, updateOptions, cancellationToken: cancellationToken);
        }

        return await _paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);
    }
}
