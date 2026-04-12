using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FriendDTO;

public class FriendBaseDTO
{

    public int FirstFriendId { get; private set; }

    public int SecondFriendId { get; private set; }
}
