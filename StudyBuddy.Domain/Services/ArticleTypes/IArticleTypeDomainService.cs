
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.ArticleTypes
{
    public interface IArticleTypeDomainService
    {
        Task<Result> Create(CreateArticleTypeDTO articleTypeDTO);
        Task<Result> Update(UpdateArticleTypeDTO articleTypeDTO);
        Task<Result> Delete(int Id);
    } 
}
