
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Posts
{
    public interface IPostDomainService
    {
        Task<Result> Create(int clientId,CreatePostDTO postDTO);
        Task<Result> Update(int clientId ,UpdatePostDTO postDTO);
        Task<Result> Delete(int clientId ,Guid Id);
        Task<Result> Like(int clientId ,Guid Id);
        Task<Result> UnLike(int clientId ,Guid Id);
        Task<Result> Share(int clientId ,Guid Id);
    } 
}
