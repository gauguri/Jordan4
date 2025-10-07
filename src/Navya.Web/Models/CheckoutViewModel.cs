using System.ComponentModel.DataAnnotations;

namespace Navya.Web.Models;

public class CheckoutViewModel
{
    public CheckoutAddress Shipping { get; set; } = new();
    public CheckoutAddress Billing { get; set; } = new();
    public bool UseDifferentBillingAddress { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PromoCode { get; set; }
}

public class CheckoutAddress
{
    [Required]
    public string Line1 { get; set; } = string.Empty;

    public string? Line2 { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string Zip { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = "United States";
}
