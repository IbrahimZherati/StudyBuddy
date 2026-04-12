using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.FeedDTO;

public class FeedBaseDTO
{

   
    [Required]
    public string Description { get; set; } = null!;
  
}
