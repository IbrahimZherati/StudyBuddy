
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Articles;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepo<Article> articleRepo;
        private readonly IArticleDomainService articleDomainService;


        public ArticleService(IRepo<Article> articleRepo, IArticleDomainService articleDomainService)
        {
            this.articleRepo = articleRepo;
            this.articleDomainService = articleDomainService;

        }

        public async Task<Result<GetArticleDTO>> Create(int clientId, CreateArticleDTO articleDTO)
        {
            var valid = await articleDomainService.Create(clientId,articleDTO);
            if (!valid.IsSuccess)
                return Result<GetArticleDTO>.Failure(valid.Error!);

            var result = Article.Create(clientId, articleDTO);

            if (!result.IsSuccess)
                return Result<GetArticleDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetArticleDTO>.Failure(Error.CreateFailed);

            var article = result.Value;
            await articleRepo.AddAsync(article);

            try
            {
                await articleRepo.SaveAsync();
                var dto = article.Adapt<GetArticleDTO>();
                return Result<GetArticleDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetArticleDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId ,int id)
        {
            var valid = await articleDomainService.Delete(clientId, id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var article = await articleRepo.GetByIdAsync(id);
            if (article == null)
                return Result.Failure(Error.ArticleNotFound);
            articleRepo.Remove(article);
            try
            {
                await articleRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetArticleDTO>> GetArticleById(int id)
        {
            var article = await articleRepo.GetByIdAsync(id);
            if (article == null)
                return Result<GetArticleDTO>.Failure(Error.ArticleNotFound);
            var articleDTO = article.Adapt<GetArticleDTO>();
            return Result<GetArticleDTO>.Success(articleDTO);
        }

        public async Task<Result<DataResponse<GetArticleDTO>>> GetArticles(int skip, int take)
        {
            var result = articleRepo.GetQuery();

            var query = result.ProjectToType<GetArticleDTO>();

            var data = new DataResponse<GetArticleDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetArticleDTO>>.Success(data);
        }

        public async Task<Result<GetArticleDTO>> Update(int clientId, UpdateArticleDTO articleDTO)
        {
            var valid = await articleDomainService.Update(clientId,articleDTO);
            if (!valid.IsSuccess)
                return Result<GetArticleDTO>.Failure(valid.Error!);

            var article = await articleRepo.GetByIdAsync(articleDTO.Id);
            if (article == null)
                return Result<GetArticleDTO>.Failure(Error.ArticleNotFound);

            var result = article.Update(articleDTO);

            if (!result.IsSuccess)
                return Result<GetArticleDTO>.Failure(result.Error!);

            articleRepo.Update(article);
            try
            {
                await articleRepo.SaveAsync();
                var dto = article.Adapt<GetArticleDTO>();
                return Result<GetArticleDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetArticleDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
