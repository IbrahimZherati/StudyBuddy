using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ClientUserGroupChatDTO;

public class ClientUserGroupChatBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public int GroupChatId { get; set; }
}
