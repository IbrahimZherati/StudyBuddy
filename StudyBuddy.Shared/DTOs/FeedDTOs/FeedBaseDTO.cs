using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FeedDTO;

public class FeedBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int ShareCount { get; set; }
    [Required]
    public int LikeCount { get; set; }
}
