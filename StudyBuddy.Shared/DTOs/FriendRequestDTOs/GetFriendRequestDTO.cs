using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Shared.DTOs.FriendRequestDTO;

public class GetFriendRequestDTO : FriendRequestBaseDTO
{
    public int Id { get; set; }
    [Required]
    public string From { get; set; } = null!;
}
