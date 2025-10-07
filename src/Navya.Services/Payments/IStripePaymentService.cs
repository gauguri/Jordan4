using Navya.Domain.Entities;
using Stripe;

namespace Navya.Services.Payments;

public interface IStripePaymentService
{
    Task<PaymentIntent> CreateOrUpdatePaymentIntentAsync(Order order, CancellationToken cancellationToken = default);
}
