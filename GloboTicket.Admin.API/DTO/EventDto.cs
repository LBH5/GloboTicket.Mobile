using System.ComponentModel.DataAnnotations;

namespace GloboTicket.Admin.API.DTO;

public class EventDto
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(25, 150)]
    public double Price { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public List<string> Artists { get; set; } = default!;

    [MaxLength(250)]
    public string? Description { get; set; }

    [Required]
    public EventStatus Status { get; set; }

    [Required]
    public CategoryDto Category { get; set; } = default!;
}
