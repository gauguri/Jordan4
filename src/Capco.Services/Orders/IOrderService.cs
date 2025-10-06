using Capco.Domain.Entities;

namespace Capco.Services.Orders;

public interface IOrderService
{
    Task<Order> CreateOrderFromCartAsync(Cart cart, Address shipping, Address? billing, string email, CancellationToken cancellationToken = default);

    Task MarkPaidAsync(Order order, string paymentIntentId, CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(Order order, string status, CancellationToken cancellationToken = default);
}
