
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Feeds
{
    public interface IFeedDomainService
    {
        Task<Result> Create(CreateFeedDTO feedDTO);
        Task<Result> Update(UpdateFeedDTO feedDTO);
        Task<Result> Delete(int Id);
        Task<Result> Like(int cliientId , int feedId);
        Task<Result> UnLike(int cliientId , int feedId);
        Task<Result> Share(int feedId);

    } 
}
