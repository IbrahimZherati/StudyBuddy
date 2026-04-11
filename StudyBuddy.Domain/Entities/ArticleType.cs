using Mapster;
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ArticleType : EntityBase<int>
{
     public string Name { get; private set; } = null!;
     private readonly List<Article> _articles = new();
     public virtual IReadOnlyCollection<Article> Articles => _articles;


     private ArticleType() { }

     public static Result<ArticleType> Create(CreateArticleTypeDTO articleTypeDTO)
     {
         var newArticleType = new ArticleType();
         articleTypeDTO.Adapt(newArticleType);
         newArticleType.CreateDate = DateTime.Now;
         return Result<ArticleType>.Success(newArticleType);
     }

     public Result<ArticleType> Update(UpdateArticleTypeDTO articleTypeDTO)
     {
         articleTypeDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<ArticleType>.Success(this);
     }


 }
