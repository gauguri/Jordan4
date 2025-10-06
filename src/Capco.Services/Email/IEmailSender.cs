using Capco.Domain.Entities;

namespace Capco.Services.Email;

public interface IEmailSender
{
    Task SendOrderConfirmationAsync(Order order, CancellationToken cancellationToken = default);
}
