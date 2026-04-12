
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Articles
{
    public interface IArticleDomainService
    {
        Task<Result> Create(int clientId, CreateArticleDTO articleDTO);
        Task<Result> Update(int clientId, UpdateArticleDTO articleDTO);
        Task<Result> Delete(int clientId, int Id);
    } 
}
