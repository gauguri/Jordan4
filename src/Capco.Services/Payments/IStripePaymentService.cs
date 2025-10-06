using Capco.Domain.Entities;
using Stripe;

namespace Capco.Services.Payments;

public interface IStripePaymentService
{
    Task<PaymentIntent> CreateOrUpdatePaymentIntentAsync(Order order, CancellationToken cancellationToken = default);
}
