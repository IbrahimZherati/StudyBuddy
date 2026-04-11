
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.ArticleTypes;
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class ArticleTypeService : IArticleTypeService
    {
        private readonly IRepo<ArticleType> articleTypeRepo;
        private readonly IArticleTypeDomainService articleTypeDomainService;


        public ArticleTypeService(IRepo<ArticleType> articleTypeRepo, IArticleTypeDomainService articleTypeDomainService)
        {
            this.articleTypeRepo = articleTypeRepo;
            this.articleTypeDomainService = articleTypeDomainService;

        }

        public async Task<Result> Create(CreateArticleTypeDTO articleTypeDTO)
        {
            var valid = await articleTypeDomainService.Create(articleTypeDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = ArticleType.Create(articleTypeDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if(result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var articleType = result.Value;
            await articleTypeRepo.AddAsync(articleType);

            try
            {
                await articleTypeRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await articleTypeDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var articleType = await articleTypeRepo.GetByIdAsync(id);
            if (articleType == null)
                return Result.Failure(Error.ArticleTypeNotFound);
            articleTypeRepo.Remove(articleType);
            try
            {
                await articleTypeRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetArticleTypeDTO>> GetArticleTypeById(int id)
        {
            var articleType = await articleTypeRepo.GetByIdAsync(id);
            if (articleType == null)
                return Result<GetArticleTypeDTO>.Failure(Error.ArticleTypeNotFound);
            var articleTypeDTO = articleType.Adapt<GetArticleTypeDTO>();
            return Result<GetArticleTypeDTO>.Success(articleTypeDTO);
        }

        public async Task<Result<DataResponse<GetArticleTypeDTO>>> GetArticleTypes(int skip, int take)
        {
            var result = articleTypeRepo.GetQuery();

            var query = result.ProjectToType<GetArticleTypeDTO>();

            var data = new DataResponse<GetArticleTypeDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetArticleTypeDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateArticleTypeDTO articleTypeDTO)
        {
            var valid = await articleTypeDomainService.Update(articleTypeDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var articleType = await articleTypeRepo.GetByIdAsync(articleTypeDTO.Id);
            if (articleType == null)
                return Result.Failure(Error.ArticleTypeNotFound);

            var result = articleType.Update(articleTypeDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            articleTypeRepo.Update(articleType);
            try
            {
                await articleTypeRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
