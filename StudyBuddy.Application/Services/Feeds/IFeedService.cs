using Mapster;
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
    public interface IFeedService
    {
        Task<Result<GetFeedDTO>> Create(int clientId ,CreateFeedDTO feedDTO);
        Task<Result<GetFeedDTO>> Update(int clientId ,UpdateFeedDTO feedDTO);
        Task<Result> Delete(int clientId ,int id);
        Task<Result> Like(int clientUserId, int feedId);
        Task<Result> Unlike(int clientUserId, int feedId);
        Task<Result> Share(int feedId);
        Task<Result<GetFeedDTO>> GetFeedById(int id);
        Task<Result<DataResponse<GetFeedDTO>>> GetFeeds(int skip, int take);
    }
}

