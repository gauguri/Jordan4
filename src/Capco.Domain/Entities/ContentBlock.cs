using System.ComponentModel.DataAnnotations;

namespace Capco.Domain.Entities;

public class ContentBlock
{
    public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Html { get; set; } = string.Empty;
}
