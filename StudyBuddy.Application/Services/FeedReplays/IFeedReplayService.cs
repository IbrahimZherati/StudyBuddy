using Mapster;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IFeedReplayService
     {
         Task<Result<GetFeedReplayDTO>> Create(CreateFeedReplayDTO feedreplayDTO);
         Task<Result<GetFeedReplayDTO>> Update(UpdateFeedReplayDTO feedreplayDTO);
         Task<Result<GetFeedReplayDTO>> GetFeedReplayById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetFeedReplayDTO>>> GetFeedReplays(int feedId ,int skip, int take);
     }
}
     
