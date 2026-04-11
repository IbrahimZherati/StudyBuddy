using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.MajorDTO;

public class MajorBaseDTO
{

    [Required]
    public string Name { get; set; } = null!;
}
