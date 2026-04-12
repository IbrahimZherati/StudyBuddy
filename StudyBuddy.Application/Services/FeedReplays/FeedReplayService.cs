
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.FeedReplays;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class FeedReplayService : IFeedReplayService
    {
        private readonly IRepo<FeedReplay> feedReplayRepo;
        private readonly IFeedReplayDomainService feedReplayDomainService;


        public FeedReplayService(IRepo<FeedReplay> feedReplayRepo, IFeedReplayDomainService feedReplayDomainService)
        {
            this.feedReplayRepo = feedReplayRepo;
            this.feedReplayDomainService = feedReplayDomainService;

        }

        public async Task<Result<GetFeedReplayDTO>> Create(CreateFeedReplayDTO feedReplayDTO)
        {
            var valid = await feedReplayDomainService.Create(feedReplayDTO);
            if (!valid.IsSuccess)
                return Result<GetFeedReplayDTO>.Failure(valid.Error!);

            var result = FeedReplay.Create(feedReplayDTO);

            if (!result.IsSuccess)
                return Result<GetFeedReplayDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetFeedReplayDTO>.Failure(Error.CreateFailed);

            var feedReplay = result.Value;
            await feedReplayRepo.AddAsync(feedReplay);

            try
            {
                await feedReplayRepo.SaveAsync();
                var dto = feedReplay.Adapt<GetFeedReplayDTO>();
                return Result<GetFeedReplayDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetFeedReplayDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await feedReplayDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var feedReplay = await feedReplayRepo.GetByIdAsync(id);
            if (feedReplay == null)
                return Result.Failure(Error.FeedReplayNotFound);
            feedReplayRepo.Remove(feedReplay);
            try
            {
                await feedReplayRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetFeedReplayDTO>> GetFeedReplayById(int id)
        {
            var feedReplay = await feedReplayRepo.GetByIdAsync(id);
            if (feedReplay == null)
                return Result<GetFeedReplayDTO>.Failure(Error.FeedReplayNotFound);
            var feedReplayDTO = feedReplay.Adapt<GetFeedReplayDTO>();
            return Result<GetFeedReplayDTO>.Success(feedReplayDTO);
        }

        public async Task<Result<DataResponse<GetFeedReplayDTO>>> GetFeedReplays(int feedId ,int skip, int take)
        {
            var result = feedReplayRepo.GetQuery().Where(fr => fr.FeedId == feedId);

            var query = result.ProjectToType<GetFeedReplayDTO>();

            var data = new DataResponse<GetFeedReplayDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetFeedReplayDTO>>.Success(data);
        }

        public async Task<Result<GetFeedReplayDTO>> Update(UpdateFeedReplayDTO feedReplayDTO)
        {
            var valid = await feedReplayDomainService.Update(feedReplayDTO);
            if (!valid.IsSuccess)
                return Result<GetFeedReplayDTO>.Failure(valid.Error!);

            var feedReplay = await feedReplayRepo.GetByIdAsync(feedReplayDTO.Id);
            if (feedReplay == null)
                return Result<GetFeedReplayDTO>.Failure(Error.FeedReplayNotFound);

            var result = feedReplay.Update(feedReplayDTO);

            if (!result.IsSuccess)
                return Result<GetFeedReplayDTO>.Failure(result.Error!);

            feedReplayRepo.Update(feedReplay);
            try
            {
                await feedReplayRepo.SaveAsync();
                var dto = feedReplay.Adapt<GetFeedReplayDTO>();
                return Result<GetFeedReplayDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetFeedReplayDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
