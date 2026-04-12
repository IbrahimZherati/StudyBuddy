using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FriendRequestDTO;

public class FriendRequestBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public int FriendId { get; set; }
    public bool? IsAccepted { get; set; }
}
