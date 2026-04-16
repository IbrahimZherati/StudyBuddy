using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.PostReplays
{
    public class PostReplayDomainService : IPostReplayDomainService
    {
        private readonly IRepo<PostReplay, Guid> postReplayRepo;
        private readonly IRepo<Post,Guid> postRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public PostReplayDomainService(IRepo<PostReplay,Guid> postReplayRepo
        ,IRepo<Post,Guid> postRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.postReplayRepo = postReplayRepo;
            this.postRepo = postRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(int clientId, CreatePostReplayDTO postReplayDTO)
        {
            
            if (!await postRepo.ExistsAsync(p => p.Id == postReplayDTO.PostId))
                return Result.Failure(Error.PostNotFound);


            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


           
            return Result.Success();
        }

        public async Task<Result> Delete(int clientId ,Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var replay = await postReplayRepo.GetByIdAsync(Id);
            if (replay == null)
                return Result.Failure(Error.PostReplayNotFound);

            if (replay.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId ,UpdatePostReplayDTO postReplayDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await postRepo.ExistsAsync(p => p.Id == postReplayDTO.PostId))
                return Result.Failure(Error.PostNotFound);

            var replay = await postReplayRepo.GetByIdAsync(postReplayDTO.Id);

            if (replay == null)
                return Result.Failure(Error.PostReplayNotFound);

            if (replay.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);





            return Result.Success();
        }
    }
}
