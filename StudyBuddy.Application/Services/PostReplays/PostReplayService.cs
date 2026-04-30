using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.PostReplys;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class PostReplyService : IPostReplyService
    {
        private readonly IRepo<PostReply,Guid> postReplyRepo;
        private readonly IPostReplyDomainService postReplyDomainService;


        public PostReplyService(IRepo<PostReply,Guid> postReplyRepo, IPostReplyDomainService postReplyDomainService)
        {
            this.postReplyRepo = postReplyRepo;
            this.postReplyDomainService = postReplyDomainService;

        }

        public async Task<Result<GetPostReplyDTO>> Create(int clientId, CreatePostReplyDTO postReplyDTO)
        {
            var valid = await postReplyDomainService.Create(clientId ,postReplyDTO);
            if (!valid.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(valid.Error!);

            var result = PostReply.Create(clientId, postReplyDTO);

            if (!result.IsSuccess)
                return Result<GetPostReplyDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetPostReplyDTO>.Failure(Error.CreateFailed);

            var postReply = result.Value;
            await postReplyRepo.AddAsync(postReply);

            try
            {
                await postReplyRepo.SaveAsync();
                var dto = postReply.Adapt<GetPostReplyDTO>();
                return Result<GetPostReplyDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetPostReplyDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId ,Guid id)
        {
            var valid = await postReplyDomainService.Delete(clientId ,id);
            if(!valid.IsSuccess)
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
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetPostReplyDTO>> GetPostReplyById(Guid id)
        {
            var postReply = await postReplyRepo.GetByIdAsync(id);
            if (postReply == null)
                return Result<GetPostReplyDTO>.Failure(Error.PostReplyNotFound);
            var postReplyDTO = postReply.Adapt<GetPostReplyDTO>();
            return Result<GetPostReplyDTO>.Success(postReplyDTO);
        }

        public async Task<Result<DataResponse<GetPostReplyDTO>>> GetPostReplys(int skip, int take)
        {
            var result = postReplyRepo.GetQuery();

            var query = result.ProjectToType<GetPostReplyDTO>();

            var data = new DataResponse<GetPostReplyDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetPostReplyDTO>>.Success(data);
        }

        public async Task<Result<GetPostReplyDTO>> Update(int clientId ,UpdatePostReplyDTO postReplyDTO)
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
            catch(DbUpdateException e)
            {
                return Result<GetPostReplyDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
