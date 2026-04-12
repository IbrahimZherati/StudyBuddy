using Mapster;
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
    public interface IFeedService
    {
        Task<Result<GetFeedDTO>> Create(CreateFeedDTO feedDTO);
        Task<Result<GetFeedDTO>> Update(UpdateFeedDTO feedDTO);
        Task<Result<GetFeedDTO>> GetFeedById(int id);
        Task<Result> Like(int clientUserId, int feedId);
        Task<Result> Unlike(int clientUserId, int feedId);
        Task<Result> Share(int feedId);
        Task<Result> Delete(int id);
        Task<Result<DataResponse<GetFeedDTO>>> GetFeeds(int skip, int take);
    }
}

