using Mapster;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class PostReply : EntityBase<Guid>
    {
        public Guid PostId { get; private set; }
        public int ClientUserId { get; private set; }

        public string? Text { get; private set; }


        public virtual Post Post { get; private set; } = null!;
        public virtual ClientUser ClientUser { get; private set; } = null!;


        public static Result<PostReply> Create(int clientId , CreatePostReplyDTO ReplyDTO)
        {
            var newReply = new PostReply();
            ReplyDTO.Adapt(newReply);
            newReply.ClientUserId = clientId;
            newReply.CreateDate = DateTime.Now;
            return Result<PostReply>.Success(newReply);
        }

        public Result<PostReply> Update(UpdatePostReplyDTO postReplyDTO)
        {
            postReplyDTO.Adapt(this);
            ModifyDate = DateTime.Now;
            return Result<PostReply>.Success(this);
        }

    }
}
