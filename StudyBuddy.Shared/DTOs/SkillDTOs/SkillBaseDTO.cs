using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.SkillDTO;

public class SkillBaseDTO
{

    [Required]
    public string Name { get; set; } = null!;
}
