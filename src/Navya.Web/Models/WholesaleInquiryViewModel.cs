using System.ComponentModel.DataAnnotations;

namespace Navya.Web.Models;

public class WholesaleInquiryViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Company { get; set; } = string.Empty;

    [Required]
    public string EstimatedQuantity { get; set; } = string.Empty;

    public string? Occasion { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;
}
