using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.PostReplys;
using StudyBuddy.Shared.DTOs.PostReplayDTOs;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudyBuddy.Application.Services
{
    public class PostReplyService : IPostReplyService
    {
        private readonly IRepo<PostReply, Guid> postReplyRepo;
        private readonly IRepo<ClientUserLikeReply> clientUserLikeReplyRepo;
        private readonly IPostReplyDomainService postReplyDomainService;


        public PostReplyService(IRepo<PostReply, Guid> postReplyRepo, IRepo<ClientUserLikeReply> clientUserLikeReplyRepo, IPostReplyDomainService postReplyDomainService)
        {
            this.postReplyRepo = postReplyRepo;
            this.clientUserLikeReplyRepo = clientUserLikeReplyRepo;
            this.postReplyDomainService = postReplyDomainService;

        }

        public async Task<Result<GetPostReplyDTO>> Create(int clientId, CreatePostReplyDTO postReplyDTO)
        {
            var valid = await postReplyDomainService.Create(clientId, postReplyDTO);
            if (!valid.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(valid.Error!);

            var result = PostReply.Create(clientId, postReplyDTO);

            if (!result.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetPostReplyDTO>.Failure(Error.CreateFailed);

            var postReply = result.Value;
            await postReplyRepo.AddAsync(postReply);

            try
            {
                await postReplyRepo.SaveAsync();
                var dto = postReply.Adapt<GetPostReplyDTO>();
                return Result<GetPostReplyDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetPostReplyDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId, Guid id)
        {
            var valid = await postReplyDomainService.Delete(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var postReply = await postReplyRepo.GetByIdAsync(id);
            if (postReply == null)
                return Result.Failure(Error.PostReplyNotFound);
            postReplyRepo.Remove(postReply);
            try
            {
                await postReplyRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<InfoPostReplyDTO>> GetPostReplyById(int clientId,Guid id)
        {
            var postReply = await postReplyRepo.GetByIdAsync(id);
            if (postReply == null)
                return Result<InfoPostReplyDTO>.Failure(Error.PostReplyNotFound);
            var postReplyDTO = postReply.Adapt<InfoPostReplyDTO>();
            postReplyDTO.IsLiked = await clientUserLikeReplyRepo.ExistsAsync(r => r.ClientUserId == clientId && r.PostReplyId == id);
            return Result<InfoPostReplyDTO>.Success(postReplyDTO);
        }

       

        public async Task<Result> Like(int clientId, Guid id)
        {
            var valid = await postReplyDomainService.Like(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var like = ClientUserLikeReply.Create(clientId, id);


            await clientUserLikeReplyRepo.AddAsync(like);
            try
            {
                await postReplyRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result> UnLike(int clientId, Guid id)
        {
            var valid = await postReplyDomainService.UnLike(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var like = await clientUserLikeReplyRepo.GetQuery()
                .Where(r => r.ClientUserId == clientId && r.PostReplyId == id)
                .FirstOrDefaultAsync();
            if (like == null)
                return Result.Failure(OperationErrorMessage.ItemNotFound);

             clientUserLikeReplyRepo.Remove(like);
            try
            {
                await postReplyRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result<GetPostReplyDTO>> Update(int clientId, UpdatePostReplyDTO postReplyDTO)
        {
            var valid = await postReplyDomainService.Update(clientId, postReplyDTO);
            if (!valid.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(valid.Error!);

            var postReply = await postReplyRepo.GetByIdAsync(postReplyDTO.Id);
            if (postReply == null)
                return Result<GetPostReplyDTO>.Failure(Error.PostReplyNotFound);

            var result = postReply.Update(postReplyDTO);

            if (!result.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(result.Error!);

            postReplyRepo.Update(postReply);
            try
            {
                await postReplyRepo.SaveAsync();
                var dto = postReply.Adapt<GetPostReplyDTO>();
                return Result<GetPostReplyDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetPostReplyDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
