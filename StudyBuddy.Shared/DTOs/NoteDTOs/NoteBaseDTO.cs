using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.NoteDTO;

public class NoteBaseDTO
{

  
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Notes { get; set; } = null!;
}
