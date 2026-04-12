using Mapster;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IArticleService
     {
         Task<Result<GetArticleDTO>> Create(int clientId ,CreateArticleDTO articleDTO);
         Task<Result<GetArticleDTO>> Update(int clientId ,UpdateArticleDTO articleDTO);
         Task<Result> Delete(int clientId, int id);
         Task<Result<GetArticleDTO>> GetArticleById(int id);
         Task<Result<DataResponse<GetArticleDTO>>> GetArticles(int skip, int take);
     }
}
     
