using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.MessageDTO;

public class MessageBaseDTO
{

    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public int ToClientUserId { get; set; }
  
}
