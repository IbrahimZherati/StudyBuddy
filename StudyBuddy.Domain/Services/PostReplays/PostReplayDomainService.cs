using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using Microsoft.EntityFrameworkCore;
namespace StudyBuddy.Domain.Services.PostReplys
{
    public class PostReplyDomainService : IPostReplyDomainService
    {
        private readonly IRepo<PostReply, Guid> postReplyRepo;
        private readonly IRepo<Post,Guid> postRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ClientUserLikeReply> clientUserLikeReplyRepo;

        public PostReplyDomainService(IRepo<PostReply,Guid> postReplyRepo
        ,IRepo<Post,Guid> postRepo
        ,IRepo<ClientUser> clientUserRepo
            ,IRepo<ClientUserLikeReply> clientUserLikeReplyRepo
        )
        {
            this.postReplyRepo = postReplyRepo;
            this.postRepo = postRepo;
            this.clientUserRepo = clientUserRepo;
            this.clientUserLikeReplyRepo = clientUserLikeReplyRepo;
        }

        public async Task<Result> Create(int clientId, CreatePostReplyDTO postReplyDTO)
        {
            
            if (!await postRepo.ExistsAsync(p => p.Id == postReplyDTO.PostId))
                return Result.Failure(Error.PostNotFound);


            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


           
            return Result.Success();
        }

        public async Task<Result> Delete(int clientId ,Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var Reply = await postReplyRepo.GetByIdAsync(Id);
            if (Reply == null)
                return Result.Failure(Error.PostReplyNotFound);

            if (Reply.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Like(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            var like = await clientUserLikeReplyRepo.GetQuery()
                .Where(r => r.ClientUserId == clientId && r.PostReplyId == Id)
                .FirstOrDefaultAsync();
            if (like != null)
                return Result.Failure(Error.ClientUserAlreadyLikeThisReply);
            return Result.Success();
        }

        public async Task<Result> UnLike(int clientId, Guid Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            var like = await clientUserLikeReplyRepo.GetQuery()
                .Where(r => r.ClientUserId == clientId && r.PostReplyId == Id)
                .FirstOrDefaultAsync();
            if (like == null)
                return Result.Failure(Error.ClientUserAlreadyUnLikeThisReply);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId ,UpdatePostReplyDTO postReplyDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await postRepo.ExistsAsync(p => p.Id == postReplyDTO.PostId))
                return Result.Failure(Error.PostNotFound);

            var Reply = await postReplyRepo.GetByIdAsync(postReplyDTO.Id);

            if (Reply == null)
                return Result.Failure(Error.PostReplyNotFound);

            if (Reply.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);





            return Result.Success();
        }
    }
}
