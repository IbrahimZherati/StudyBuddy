
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Posts
{
    public class PostDomainService : IPostDomainService
    {
        private readonly IRepo<Post,Guid> postRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ClientUserLikePost> clientUserLikePostRepo;

        public PostDomainService(IRepo<Post,Guid> postRepo
        ,IRepo<ClientUser> clientUserRepo
            ,IRepo<ClientUserLikePost> clientUserLikePostRepo
        )
        {
            this.postRepo = postRepo;
            this.clientUserRepo = clientUserRepo;
            this.clientUserLikePostRepo = clientUserLikePostRepo;
        }

        public async Task<Result> Create(int clientId, CreatePostDTO postDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            var post = await postRepo.GetByIdAsync(Id);
            if (post == null)
                return Result.Failure(Error.PostNotFound);
            if (post.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Like(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await postRepo.ExistsAsync(p => p.Id == Id))
                return Result.Failure(Error.PostNotFound);
            if (await clientUserLikePostRepo.ExistsAsync(c => c.PostId == Id && c.ClientUserId == clientId))
                return Result.Failure(Error.ClientUserAlreadyLikeThisPost);
            return Result.Success();
        }

        public async Task<Result> Share(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await postRepo.ExistsAsync(p => p.Id == Id))
                return Result.Failure(Error.PostNotFound);

            return Result.Success();
        }

        public async Task<Result> UnLike(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await postRepo.ExistsAsync(p => p.Id == Id))
                return Result.Failure(Error.PostNotFound);
            if (!await clientUserLikePostRepo.ExistsAsync(c => c.PostId == Id && c.ClientUserId == clientId))
                return Result.Failure(Error.ClientUserNotLikedThisPost);
                return Result.Success();
        }

        public async Task<Result> Update(int clientId, UpdatePostDTO postDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            var post = await postRepo.GetByIdAsync(postDTO.Id);
            if (post == null)
                return Result.Failure(Error.PostNotFound);
            if (post.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }
    }
}
