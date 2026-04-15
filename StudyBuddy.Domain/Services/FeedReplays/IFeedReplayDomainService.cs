
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.FeedReplays
{
    public interface IFeedReplayDomainService
    {
        Task<Result> Create(int clientId, CreateFeedReplayDTO feedReplayDTO);
        Task<Result> Update(int clientId, UpdateFeedReplayDTO feedReplayDTO);
        Task<Result> Delete(int clientId, int Id);
    } 
}
