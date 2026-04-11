using Mapster;
using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
    public interface IArticleService
    {
        Task<Result> Create(CreateArticleDTO articleDTO);
        Task<Result> Update(UpdateArticleDTO articleDTO);
        Task<Result<GetArticleDTO>> GetArticleById(int id);
        Task<Result> Delete(int id);
        Task<Result<DataResponse<GetArticleDTO>>> GetArticles(int skip, int take);
    }
}

