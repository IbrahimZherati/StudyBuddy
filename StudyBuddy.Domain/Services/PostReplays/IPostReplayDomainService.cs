using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.PostReplys
{
    public interface IPostReplyDomainService
    {
        Task<Result> Create(int clientId ,CreatePostReplyDTO postReplyDTO);
        Task<Result> Update(int clientId ,UpdatePostReplyDTO postReplyDTO);
        Task<Result> Like(int clientId, Guid Id);
        Task<Result> UnLike(int clientId, Guid Id);
        Task<Result> Delete(int clientId ,Guid Id);
    } 
}
