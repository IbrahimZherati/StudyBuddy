using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.PostDTO;

public class PostBaseDTO
{


    [Required]
    public byte[] Photo { get; set; } = null!;
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Text { get; set; } = null!;
}
