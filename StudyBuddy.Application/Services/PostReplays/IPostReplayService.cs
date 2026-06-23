using Mapster;
using StudyBuddy.Shared.DTOs.PostReplayDTOs;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services
{
     public interface IPostReplyService
     {
         Task<Result<GetPostReplyDTO>> Create(int cliendId ,CreatePostReplyDTO postReplyDTO);
         Task<Result<GetPostReplyDTO>> Update(int clientId ,UpdatePostReplyDTO postReplyDTO);
         Task<Result<InfoPostReplyDTO>> GetPostReplyById(int clientId ,Guid id);
         Task<Result> Delete(int clientId ,Guid id);
         Task<Result> Like(int clientId ,Guid id);
         Task<Result> UnLike(int clientId ,Guid id);
     }
}
     
