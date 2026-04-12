
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Articles
{
    public class ArticleDomainService : IArticleDomainService
    {
        private readonly IRepo<Article> articleRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ArticleType> articleTypeRepo;


        public ArticleDomainService(IRepo<Article> articleRepo
        ,IRepo<ClientUser> clientUserRepo
        ,IRepo<ArticleType> articleTypeRepo
        )
        {
            this.articleRepo = articleRepo;
            this.clientUserRepo = clientUserRepo;
            this.articleTypeRepo = articleTypeRepo;

        }

        public async Task<Result> Create(int clientId, CreateArticleDTO articleDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            if (!await articleTypeRepo.ExistsAsync(a => a.Id == articleDTO.ArticleTypeId))
                return Result.Failure(Error.ArticleTypeNotFound);


            if(await articleRepo.ExistsAsync(a => a.Title == articleDTO.Title))
                return Result.Failure(Error.ArticleAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var article = await articleRepo.GetByIdAsync(Id);
            if(article == null)
                return Result.Failure(Error.ArticleNotFound);

            if(article.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId, UpdateArticleDTO articleDTO)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var article = await articleRepo.GetByIdAsync(articleDTO.Id);
            if (article == null)
                return Result.Failure(Error.ArticleNotFound);

            if (article.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);


            if (!await articleTypeRepo.ExistsAsync(a => a.Id == articleDTO.ArticleTypeId))
                return Result.Failure(Error.ArticleTypeNotFound);


            if (await articleRepo.ExistsAsync(a => a.Title == articleDTO.Title && a.Id != articleDTO.Id ))
                return Result.Failure(Error.ArticleAlreadyExists);
            return Result.Success();
        }
    }
}
