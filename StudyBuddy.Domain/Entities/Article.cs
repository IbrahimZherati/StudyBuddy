using Mapster;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Article : EntityBase<int>
{
    public int? ClientUserId { get; private set; }

    public string Title { get; private set; } = null!;

    public string Discription { get; private set; } = null!;

    public int ArticleTypeId { get; private set; }

    public virtual ArticleType ArticleType { get; private set; } = null!;

    public virtual ClientUser ClientUser { get; private set; } = null!;


    private Article() { }

    public static Result<Article> Create(CreateArticleDTO articleDTO)
    {
        var newArticle = new Article();
        articleDTO.Adapt(newArticle);
        newArticle.CreateDate = DateTime.Now;
        return Result<Article>.Success(newArticle);
    }

    public Result<Article> Update(UpdateArticleDTO articleDTO)
    {
        articleDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<Article>.Success(this);
    }


}
