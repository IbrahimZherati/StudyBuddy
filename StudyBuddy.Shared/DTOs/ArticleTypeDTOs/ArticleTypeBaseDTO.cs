using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ArticleTypeDTO;

public class ArticleTypeBaseDTO
{

    [Required]
    public string Name { get; set; } = null!;
}
