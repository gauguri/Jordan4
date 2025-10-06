using Capco.Domain.Entities;

namespace Capco.Web.Models;

public class CartViewModel
{
    public Cart Cart { get; set; } = null!;
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string? PromoMessage { get; set; }
}
