using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FriendDTO;

public class FriendBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public int FriendId { get; set; }
}
