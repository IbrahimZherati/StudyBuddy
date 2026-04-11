using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.EventDTO;

public class EventBaseDTO
{

    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int ClientUserId { get; set; }
}
