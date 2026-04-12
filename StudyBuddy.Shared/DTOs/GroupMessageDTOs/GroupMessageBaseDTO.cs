using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.GroupMessageDTO;

public class GroupMessageBaseDTO
{

    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public int GroupChatId { get; set; }
  
}
