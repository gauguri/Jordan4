using Capco.Domain.Entities;

namespace Capco.Web.Models;

public class OrderConfirmationViewModel
{
    public Order Order { get; set; } = null!;
    public string Message { get; set; } = "Thank you for celebrating with Capco.";
}
