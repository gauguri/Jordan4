using Navya.Domain.Entities;

namespace Navya.Services.Email;

public interface IEmailSender
{
    Task SendOrderConfirmationAsync(Order order, CancellationToken cancellationToken = default);
}
