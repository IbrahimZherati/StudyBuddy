namespace StudyBuddy.Shared.DTOs.ArticleDTO;

public class ArticleBaseDTO
{

    public int? ClientUserId { get; set; }
    public string Title { get; set; } = null!;
    public string Discription { get; set; } = null!;
    public int ArticleTypeId { get; set; }
}
