
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.FeedReplays
{
    public interface IFeedReplayDomainService
    {
        Task<Result> Create(CreateFeedReplayDTO feedReplayDTO);
        Task<Result> Update(UpdateFeedReplayDTO feedReplayDTO);
        Task<Result> Delete(int Id);
    } 
}
