using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.CountryDTO;

public class CountryBaseDTO
{

    [Required]
    public string Name { get; set; } = null!;
}
