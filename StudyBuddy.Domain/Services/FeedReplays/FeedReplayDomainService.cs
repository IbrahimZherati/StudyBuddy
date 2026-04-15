
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
        private readonly IRepo<ClientUser> clientUserRepo;

        public FeedReplayDomainService(IRepo<FeedReplay> feedReplayRepo
        ,IRepo<Feed> feedRepo
            ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.feedReplayRepo = feedReplayRepo;
            this.feedRepo = feedRepo;
            this.clientUserRepo = clientUserRepo;
        }

        public async Task<Result> Create(int clientId, CreateFeedReplayDTO feedReplayDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (!await feedRepo.ExistsAsync(f => f.Id == feedReplayDTO.FeedId))
                return Result.Failure(Error.FeedNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var replay = await feedReplayRepo.GetByIdAsync(Id);
            if (replay == null)
                return Result.Failure(Error.FeedReplayNotFound);

            if (replay.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId , UpdateFeedReplayDTO feedReplayDTO)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
           
            var replay = await feedReplayRepo.GetByIdAsync(feedReplayDTO.Id);
            if (replay == null)
                return Result.Failure(Error.FeedReplayNotFound);

            if (replay.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);



            return Result.Success();
        }
    }
}
