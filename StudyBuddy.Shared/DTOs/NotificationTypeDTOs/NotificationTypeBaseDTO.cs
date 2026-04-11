using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.NotificationTypeDTO;

public class NotificationTypeBaseDTO
{

    [Required]
    public string Type { get; set; } = null!;
}
