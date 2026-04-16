using Mapster;
using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class PostReplay : EntityBase<Guid>
    {
        public Guid PostId { get; private set; }
        public int ClientUserId { get; private set; }

        public string? Text { get; private set; }


        public virtual Post Post { get; private set; } = null!;
        public virtual ClientUser ClientUser { get; private set; } = null!;


        public static Result<PostReplay> Create(int clientId , CreatePostReplayDTO replayDTO)
        {
            var newReplay = new PostReplay();
            replayDTO.Adapt(newReplay);
            newReplay.ClientUserId = clientId;
            newReplay.CreateDate = DateTime.Now;
            return Result<PostReplay>.Success(newReplay);
        }

        public Result<PostReplay> Update(UpdatePostReplayDTO postReplayDTO)
        {
            postReplayDTO.Adapt(this);
            ModifyDate = DateTime.Now;
            return Result<PostReplay>.Success(this);
        }

    }
}
