using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FriendRequestDTO;

public class FriendRequestBaseDTO
{
    [Required]
    public int FromClientUserId { get;  set; }

    [Required]
    public int ToClientUserId { get;  set; }

    public bool? IsAccepted { get; private set; }

}
