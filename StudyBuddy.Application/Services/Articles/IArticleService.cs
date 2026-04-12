using Mapster;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IArticleService
     {
         Task<Result<GetArticleDTO>> Create(CreateArticleDTO articleDTO);
         Task<Result<GetArticleDTO>> Update(UpdateArticleDTO articleDTO);
         Task<Result<GetArticleDTO>> GetArticleById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetArticleDTO>>> GetArticles(int skip, int take);
     }
}
     
