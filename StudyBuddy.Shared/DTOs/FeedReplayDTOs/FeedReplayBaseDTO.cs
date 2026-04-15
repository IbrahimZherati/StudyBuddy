using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FeedReplayDTO;

public class FeedReplayBaseDTO
{

       [Required]
    public string Text { get; set; } = null!;
}
