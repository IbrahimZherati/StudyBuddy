using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.UniversityDTO;

public class UniversityBaseDTO
{

    [Required]
    public string Name { get; set; } = null!;
}
