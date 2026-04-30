using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.StudyInterestDTO;

public class StudyInterestBaseDTO
{

    [Required]
        public string Name { get;  set; } = null!;
   
}
