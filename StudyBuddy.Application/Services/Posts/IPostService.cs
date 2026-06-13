using Mapster;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
    public interface IPostService
    {
        Task<Result<GetPostDTO>> Create(int clientId, CreatePostDTO postDTO);
        Task<Result<GetPostDTO>> Update(int clientId, UpdatePostDTO postDTO);
        Task<Result<GetPostDTO>> GetPostById(int clientId ,Guid id);
        Task<Result> Delete(int clientId, Guid id);
        Task<Result<DataResponse<GetPostDTO>>> GetPosts(int skip, int take);

        Task<Result<DataResponse<GetPostReplyDTO>>> GetPostReplys(Guid id,int skip, int take);
        Task<Result> Like(int clientId, Guid Id);
        Task<Result> UnLike(int clientId, Guid Id);
        Task<Result> Share(int clientId, Guid Id);
    }
}

