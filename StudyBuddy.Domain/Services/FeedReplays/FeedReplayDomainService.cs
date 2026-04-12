
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.FeedReplays
{
    public class FeedReplayDomainService : IFeedReplayDomainService
    {
        private readonly IRepo<FeedReplay> feedReplayRepo;
        private readonly IRepo<Feed> feedRepo;


        public FeedReplayDomainService(IRepo<FeedReplay> feedReplayRepo
        ,IRepo<Feed> feedRepo
        )
        {
            this.feedReplayRepo = feedReplayRepo;
            this.feedRepo = feedRepo;

        }

        public async Task<Result> Create(CreateFeedReplayDTO feedReplayDTO)
        {
            
            if (!await feedRepo.ExistsAsync(f => f.Id == feedReplayDTO.FeedId))
                return Result.Failure(Error.FeedNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await feedReplayRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.FeedReplayNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateFeedReplayDTO feedReplayDTO)
        { 
            if (!await feedReplayRepo.ExistsAsync(a => a.Id == feedReplayDTO.Id))
                return Result.Failure(Error.FeedReplayNotFound);
            
            if (!await feedRepo.ExistsAsync(f => f.Id == feedReplayDTO.FeedId))
                return Result.Failure(Error.FeedNotFound);


            return Result.Success();
        }
    }
}
