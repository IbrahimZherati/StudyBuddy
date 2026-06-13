using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.TopicDTO;

public class TopicBaseDTO
{

    [Required]
    public string? Name { get; set; } = null!;
}
