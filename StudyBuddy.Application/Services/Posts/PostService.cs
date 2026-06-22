
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Posts;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IRepo<Post,Guid> postRepo;
        private readonly IRepo<ClientUserLikePost> clientUserLikePost;
        private readonly IRepo<PostReply> postReplyRepo;
        private readonly IPostDomainService postDomainService;


        public PostService(IRepo<Post,Guid> postRepo,IRepo<ClientUserLikePost> clientUserLikePost, IRepo<PostReply> postReplyRepo,IPostDomainService postDomainService)
        {
            this.postRepo = postRepo;
            this.clientUserLikePost = clientUserLikePost;
            this.postReplyRepo = postReplyRepo;
            this.postDomainService = postDomainService;

        }

        public async Task<Result<GetPostDTO>> Create(int clientId, CreatePostDTO postDTO)
        {
            var valid = await postDomainService.Create(clientId ,postDTO);
            if (!valid.IsSuccess)
                return Result<GetPostDTO>.Failure(valid.Error!);

            var result = Post.Create(clientId, postDTO);

            if (!result.IsSuccess)
                return Result<GetPostDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetPostDTO>.Failure(Error.CreateFailed);

            var post = result.Value;
            await postRepo.AddAsync(post);

            try
            {
                await postRepo.SaveAsync();
                var dto = post.Adapt<GetPostDTO>();
                return Result<GetPostDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetPostDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId, Guid id)
        {
            var valid = await postDomainService.Delete(clientId, id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var post = await postRepo.GetByIdAsync(id);
            if (post == null)
                return Result.Failure(Error.PostNotFound);
            postRepo.Remove(post);
            try
            {
                await postRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetPostDTO>> GetPostById(int clientId ,Guid id)
        {
            var post = await postRepo.GetQuery().Where(p => p.Id == id)
                .ProjectToType<GetPostDTO>()
                .FirstOrDefaultAsync();
            if (post == null)
                return Result<GetPostDTO>.Failure(Error.PostNotFound);
            
            post.IsLiked = await clientUserLikePost.ExistsAsync(c => c.ClientUserId == clientId && c.PostId == id);

            return Result<GetPostDTO>.Success(post);
        }

        public async Task<Result<DataResponse<GetPostReplyDTO>>> GetPostReplys(Guid id, int skip, int take)
        {
            var result = postReplyRepo.GetQuery().Where(r => r.PostId == id);

            var query = result.ProjectToType<GetPostReplyDTO>();

            var data = new DataResponse<GetPostReplyDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetPostReplyDTO>>.Success(data);

        }

        public async Task<Result<DataResponse<GetPostDTO>>> GetMyPosts(int clientId ,int skip, int take)
        {
            var result = postRepo.GetQuery()
                .Where(p => p.ClientUserId == clientId);

            var query = result.ProjectToType<GetPostDTO>();

            var data = new DataResponse<GetPostDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var post in data.Data)
            {
                post.IsLiked = await clientUserLikePost.ExistsAsync(c => c.ClientUserId == clientId && c.PostId == post.Id);
            }
            return Result<DataResponse<GetPostDTO>>.Success(data);
        }

        public async Task<Result> Like(int clientId, Guid Id)
        {
            var valid = await postDomainService.Like(clientId, Id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var like = ClientUserLikePost.Create(clientId , Id);
            var post = await postRepo.GetQuery()
                .Include(p => p.ClientUserLikePosts)
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (post == null)
                return Result.Failure(Error.PostNotFound);
            post.Like(like);
            postRepo.Update(post);
            try
            {
                await postRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result> Share(int clientId, Guid Id)
        {
            var valid = await postDomainService.Share(clientId, Id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var post = await postRepo.GetByIdAsync(Id);
            if (post == null)
                return Result.Failure(Error.PostNotFound);
            post.Share();
            postRepo.Update(post);
            try
            {
                await postRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.UpdateFailed);
            }
        }

        public async Task<Result> UnLike(int clientId, Guid Id)
        {
            var valid = await postDomainService.UnLike(clientId, Id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var post = await postRepo.GetQuery()
               .Include(p => p.ClientUserLikePosts)
               .FirstOrDefaultAsync(p => p.Id == Id);

            var oldLike = post.ClientUserLikePosts.FirstOrDefault(l => l.PostId == Id && l.ClientUserId == clientId);
            if (oldLike == null)
                return Result.Failure(Error.ClientUserNotLikeThisPostYet);
            post.UnLike(oldLike);
            postRepo.Update(post);
            try
            {
                await postRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.UpdateFailed);
            }

        }

        public async Task<Result<GetPostDTO>> Update(int clientId, UpdatePostDTO postDTO)
        {
            var valid = await postDomainService.Update(clientId, postDTO);
            if (!valid.IsSuccess)
                return Result<GetPostDTO>.Failure(valid.Error!);

            var post = await postRepo.GetByIdAsync(postDTO.Id);
            if (post == null)
                return Result<GetPostDTO>.Failure(Error.PostNotFound);

            var result = post.Update(postDTO);

            if (!result.IsSuccess)
                return Result<GetPostDTO>.Failure(result.Error!);

            postRepo.Update(post);
            try
            {
                await postRepo.SaveAsync();
                var dto = post.Adapt<GetPostDTO>();
                return Result<GetPostDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetPostDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
