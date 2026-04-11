using Mapster;
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IArticleTypeService
     {
         Task<Result> Create(CreateArticleTypeDTO articletypeDTO);
         Task<Result> Update(UpdateArticleTypeDTO articletypeDTO);
         Task<Result<GetArticleTypeDTO>> GetArticleTypeById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetArticleTypeDTO>>> GetArticleTypes(int skip, int take);
     }
}
     
