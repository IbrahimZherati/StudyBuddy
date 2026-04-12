using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.CityDTO;

public class CityBaseDTO
{

    [Required]
    public int CountryId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
