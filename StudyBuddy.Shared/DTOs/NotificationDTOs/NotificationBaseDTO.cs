using StudyBuddy.Shared.Enum;
using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.NotificationDTO;

public class NotificationBaseDTO
{

    [Required]
    public int ToClientUserId { get; set; }
    [Required]
    public int FromClientUserId { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Title { get; set; } = null!;
    [Required]    public string? Type { get; set; }    public int? RequestId { get;  set; }    public int? GroupChatId { get;  set; }
}
