namespace StudyBuddy.Domain.Entities;

public partial class Article
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public string Title { get; set; } = null!;

    public string Discription { get; set; } = null!;

    public int ArticleTypeId { get; set; }

    public virtual ArticleType ArticleType { get; set; } = null!;

    public virtual ClientUser ClientUser { get; set; } = null!;
}
