using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.PostReplays
{
    public interface IPostReplayDomainService
    {
        Task<Result> Create(int clientId ,CreatePostReplayDTO postReplayDTO);
        Task<Result> Update(int clientId ,UpdatePostReplayDTO postReplayDTO);
        Task<Result> Delete(int clientId ,Guid Id);
    } 
}
