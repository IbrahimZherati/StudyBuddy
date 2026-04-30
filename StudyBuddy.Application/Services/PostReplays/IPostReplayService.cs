using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IPostReplyService
     {
         Task<Result<GetPostReplyDTO>> Create(int cliendId ,CreatePostReplyDTO postReplyDTO);
         Task<Result<GetPostReplyDTO>> Update(int clientId ,UpdatePostReplyDTO postReplyDTO);
         Task<Result<GetPostReplyDTO>> GetPostReplyById(Guid id);
         Task<Result> Delete(int clientId ,Guid id);
         Task<Result<DataResponse<GetPostReplyDTO>>> GetPostReplys(int skip, int take);
     }
}
     
