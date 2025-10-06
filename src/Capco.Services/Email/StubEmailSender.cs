using Capco.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Capco.Services.Email;

public class StubEmailSender : IEmailSender
{
    private readonly ILogger<StubEmailSender> _logger;

    public StubEmailSender(ILogger<StubEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendOrderConfirmationAsync(Order order, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Order confirmation email queued for {OrderNumber} to {Email}", order.OrderNumber, order.Email);
        return Task.CompletedTask;
    }
}
