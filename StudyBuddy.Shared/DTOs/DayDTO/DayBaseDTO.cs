using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Shared.DTOs.DayDTO
{
    public class DayBaseDTO
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}