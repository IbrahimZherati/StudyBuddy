
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Feeds;
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class FeedService : IFeedService
    {
        private readonly IRepo<Feed> feedRepo;
        private readonly IFeedDomainService feedDomainService;


        public FeedService(IRepo<Feed> feedRepo,
            IFeedDomainService feedDomainService)
        {
            this.feedRepo = feedRepo;
            this.feedDomainService = feedDomainService;

        }

        public async Task<Result<GetFeedDTO>> Create(CreateFeedDTO feedDTO)
        {
            var valid = await feedDomainService.Create(feedDTO);
            if (!valid.IsSuccess)
                return Result<GetFeedDTO>.Failure(valid.Error!);

            var result = Feed.Create(feedDTO);

            if (!result.IsSuccess)
                return Result<GetFeedDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetFeedDTO>.Failure(Error.CreateFailed);

            var feed = result.Value;
            await feedRepo.AddAsync(feed);

            try
            {
                await feedRepo.SaveAsync();
                var dto = feed.Adapt<GetFeedDTO>();
                return Result<GetFeedDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetFeedDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await feedDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var feed = await feedRepo.GetByIdAsync(id);
            if (feed == null)
                return Result.Failure(Error.FeedNotFound);
            feedRepo.Remove(feed);
            try
            {
                await feedRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetFeedDTO>> GetFeedById(int id)
        {
            var feed = await feedRepo.GetByIdAsync(id);
            if (feed == null)
                return Result<GetFeedDTO>.Failure(Error.FeedNotFound);
            var feedDTO = feed.Adapt<GetFeedDTO>();
            return Result<GetFeedDTO>.Success(feedDTO);
        }

        public async Task<Result<DataResponse<GetFeedDTO>>> GetFeeds(int skip, int take)
        {
            var result = feedRepo.GetQuery();

            var query = result.ProjectToType<GetFeedDTO>();

            var data = new DataResponse<GetFeedDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetFeedDTO>>.Success(data);
        }

        public async Task<Result> Like(int clientUserId, int feedId)
        {
            var valid = await feedDomainService.Like(clientUserId , feedId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var feed = await feedRepo.GetQuery()
                .Where(f => f.Id == feedId)
                .Include(f => f.ClientUserLikeFeeds)
                .FirstOrDefaultAsync();

            if (feed == null)
                return Result.Failure(Error.FeedNotFound);

            var newLike = ClientUserLikeFeed.Create(clientUserId , feedId);
            feed.AddLike(newLike);
            feedRepo.Update(feed);
            try
            {
                await feedRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result> Share(int feedId)
        {
            var valid = await feedDomainService.Share(feedId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var feed = await feedRepo.GetQuery()
                .Where(f => f.Id == feedId)
                .FirstOrDefaultAsync();

            if (feed == null)
                return Result.Failure(Error.FeedNotFound);

            feed.Share();
            feedRepo.Update(feed);
            try
            {
                await feedRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result> Unlike(int clientUserId, int feedId)
        {
            var valid = await feedDomainService.UnLike(clientUserId, feedId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var feed = await feedRepo.GetQuery()
                .Where(f => f.Id == feedId)
                .Include(f => f.ClientUserLikeFeeds)
                .FirstOrDefaultAsync();

            if (feed == null)
                return Result.Failure(Error.FeedNotFound);

            var oldLike = feed.ClientUserLikeFeeds.FirstOrDefault(cf => cf.ClientUserId == clientUserId && cf.FeedId == feedId);
            if (oldLike == null)
                return Result.Failure(Error.ClientUserNotLikeThisFeedYet);
            feed.RemoveLike(oldLike);
            feedRepo.Update(feed);
            try
            {
                await feedRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(Error.LikeFailed);
            }
        }

        public async Task<Result<GetFeedDTO>> Update(UpdateFeedDTO feedDTO)
        {
            var valid = await feedDomainService.Update(feedDTO);
            if (!valid.IsSuccess)
                return Result<GetFeedDTO>.Failure(valid.Error!);

            var feed = await feedRepo.GetByIdAsync(feedDTO.Id);
            if (feed == null)
                return Result<GetFeedDTO>.Failure(Error.FeedNotFound);

            var result = feed.Update(feedDTO);

            if (!result.IsSuccess)
                return Result<GetFeedDTO>.Failure(result.Error!);

            feedRepo.Update(feed);
            try
            {
                await feedRepo.SaveAsync();
                var dto = feed.Adapt<GetFeedDTO>();
                return Result<GetFeedDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetFeedDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
