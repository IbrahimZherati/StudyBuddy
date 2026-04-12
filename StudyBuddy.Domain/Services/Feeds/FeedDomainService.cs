
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Feeds
{
    public class FeedDomainService : IFeedDomainService
    {
        private readonly IRepo<Feed> feedRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ClientUserLikeFeed> clientUserLikeFeedRepo;

        public FeedDomainService(IRepo<Feed> feedRepo
        ,IRepo<ClientUser> clientUserRepo,
            IRepo<ClientUserLikeFeed> clientUserLikeFeedRepo
        )
        {
            this.feedRepo = feedRepo;
            this.clientUserRepo = clientUserRepo;
            this.clientUserLikeFeedRepo = clientUserLikeFeedRepo;
        }

        public async Task<Result> Create(CreateFeedDTO feedDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == feedDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await feedRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.FeedNotFound);
            return Result.Success();
        }

        public async Task<Result> Like(int cliientId, int feedId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == cliientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await feedRepo.ExistsAsync(a => a.Id == feedId))
                return Result.Failure(Error.FeedNotFound);
            if (await clientUserLikeFeedRepo.ExistsAsync(cf => cf.ClientUserId == cliientId && cf.FeedId == feedId))
                return Result.Failure(Error.ClientUserAlreadyLikeThisFeed);
            return Result.Success();
        }

        public async Task<Result> Share(int feedId)
        {
         
            if (!await feedRepo.ExistsAsync(a => a.Id == feedId))
                return Result.Failure(Error.FeedNotFound);
            return Result.Success();
        }

        public async Task<Result> UnLike(int cliientId, int feedId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == cliientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await feedRepo.ExistsAsync(a => a.Id == feedId))
                return Result.Failure(Error.FeedNotFound);

            if (!await clientUserLikeFeedRepo.ExistsAsync(cf => cf.ClientUserId == cliientId && cf.FeedId == feedId))
                return Result.Failure(Error.ClientUserNotLikeThisFeedYet);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateFeedDTO feedDTO)
        { 
            if (!await feedRepo.ExistsAsync(a => a.Id == feedDTO.Id))
                return Result.Failure(Error.FeedNotFound);
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == feedDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }
    }
}
