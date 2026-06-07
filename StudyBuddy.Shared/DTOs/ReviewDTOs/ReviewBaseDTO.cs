using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ReviewDTO;

public class ReviewBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    [Required]
    public float Rating { get; set; }
}
