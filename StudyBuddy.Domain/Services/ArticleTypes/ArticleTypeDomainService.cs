
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.ArticleTypes
{
    public class ArticleTypeDomainService : IArticleTypeDomainService
    {
        private readonly IRepo<ArticleType> articleTypeRepo;


        public ArticleTypeDomainService(IRepo<ArticleType> articleTypeRepo
        )
        {
            this.articleTypeRepo = articleTypeRepo;

        }

        public async Task<Result> Create(CreateArticleTypeDTO articleTypeDTO)
        {
            
            if(await articleTypeRepo.ExistsAsync(a => a.Name == articleTypeDTO.Name))
                return Result.Failure(Error.ArticleTypeAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await articleTypeRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.ArticleTypeNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateArticleTypeDTO articleTypeDTO)
        { 
            if (!await articleTypeRepo.ExistsAsync(a => a.Id == articleTypeDTO.Id))
                return Result.Failure(Error.ArticleTypeNotFound);
            
            if (await articleTypeRepo.ExistsAsync(a => a.Name == articleTypeDTO.Name && a.Id != articleTypeDTO.Id ))
                return Result.Failure(Error.ArticleTypeAlreadyExists);
            return Result.Success();
        }
    }
}
