using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.PostReplays;
using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class PostReplayService : IPostReplayService
    {
        private readonly IRepo<PostReplay,Guid> postReplayRepo;
        private readonly IPostReplayDomainService postReplayDomainService;


        public PostReplayService(IRepo<PostReplay,Guid> postReplayRepo, IPostReplayDomainService postReplayDomainService)
        {
            this.postReplayRepo = postReplayRepo;
            this.postReplayDomainService = postReplayDomainService;

        }

        public async Task<Result<GetPostReplayDTO>> Create(int clientId, CreatePostReplayDTO postReplayDTO)
        {
            var valid = await postReplayDomainService.Create(clientId ,postReplayDTO);
            if (!valid.IsSuccess)
                return Result<GetPostReplayDTO>.Failure(valid.Error!);

            var result = PostReplay.Create(clientId, postReplayDTO);

            if (!result.IsSuccess)
                return Result<GetPostReplayDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetPostReplayDTO>.Failure(Error.CreateFailed);

            var postReplay = result.Value;
            await postReplayRepo.AddAsync(postReplay);

            try
            {
                await postReplayRepo.SaveAsync();
                var dto = postReplay.Adapt<GetPostReplayDTO>();
                return Result<GetPostReplayDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetPostReplayDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId ,Guid id)
        {
            var valid = await postReplayDomainService.Delete(clientId ,id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var postReplay = await postReplayRepo.GetByIdAsync(id);
            if (postReplay == null)
                return Result.Failure(Error.PostReplayNotFound);
            postReplayRepo.Remove(postReplay);
            try
            {
                await postReplayRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetPostReplayDTO>> GetPostReplayById(Guid id)
        {
            var postReplay = await postReplayRepo.GetByIdAsync(id);
            if (postReplay == null)
                return Result<GetPostReplayDTO>.Failure(Error.PostReplayNotFound);
            var postReplayDTO = postReplay.Adapt<GetPostReplayDTO>();
            return Result<GetPostReplayDTO>.Success(postReplayDTO);
        }

        public async Task<Result<DataResponse<GetPostReplayDTO>>> GetPostReplays(int skip, int take)
        {
            var result = postReplayRepo.GetQuery();

            var query = result.ProjectToType<GetPostReplayDTO>();

            var data = new DataResponse<GetPostReplayDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetPostReplayDTO>>.Success(data);
        }

        public async Task<Result<GetPostReplayDTO>> Update(int clientId ,UpdatePostReplayDTO postReplayDTO)
        {
            var valid = await postReplayDomainService.Update(clientId, postReplayDTO);
            if (!valid.IsSuccess)
                return Result<GetPostReplayDTO>.Failure(valid.Error!);

            var postReplay = await postReplayRepo.GetByIdAsync(postReplayDTO.Id);
            if (postReplay == null)
                return Result<GetPostReplayDTO>.Failure(Error.PostReplayNotFound);

            var result = postReplay.Update(postReplayDTO);

            if (!result.IsSuccess)
                return Result<GetPostReplayDTO>.Failure(result.Error!);

            postReplayRepo.Update(postReplay);
            try
            {
                await postReplayRepo.SaveAsync();
                var dto = postReplay.Adapt<GetPostReplayDTO>();
                return Result<GetPostReplayDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetPostReplayDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
