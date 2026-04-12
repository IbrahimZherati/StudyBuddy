
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Articles
{
    public interface IArticleDomainService
    {
        Task<Result> Create(CreateArticleDTO articleDTO);
        Task<Result> Update(UpdateArticleDTO articleDTO);
        Task<Result> Delete(int Id);
    } 
}
