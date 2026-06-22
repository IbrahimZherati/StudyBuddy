using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.PostDTO;

public class PostBaseDTO
{


    
    public byte[]? Photo { get; set; } 
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Text { get; set; } = null!;
}
