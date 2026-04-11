using Mapster;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IArticleService
     {
         Task<Result> Create(CreateArticleDTO articleDTO);
         Task<Result> Update(UpdateArticleDTO articleDTO);
         Task<Result<GetArticleDTO>> GetArticleById(int id);
         Task<Result> Delete(int id);
         Task<Result<List<GetArticleDTO>>> GetArticles(int skip, int take);
     }
}
     
