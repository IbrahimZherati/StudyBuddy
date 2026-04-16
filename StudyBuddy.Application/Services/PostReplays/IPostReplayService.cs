using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IPostReplayService
     {
         Task<Result<GetPostReplayDTO>> Create(int cliendId ,CreatePostReplayDTO postreplayDTO);
         Task<Result<GetPostReplayDTO>> Update(int clientId ,UpdatePostReplayDTO postreplayDTO);
         Task<Result<GetPostReplayDTO>> GetPostReplayById(Guid id);
         Task<Result> Delete(int clientId ,Guid id);
         Task<Result<DataResponse<GetPostReplayDTO>>> GetPostReplays(int skip, int take);
     }
}
     
