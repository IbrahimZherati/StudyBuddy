using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class ClientUserLikePost : EntityBase<Guid>
    {
        public int ClientUserId { get; private set; }
        public Guid PostId { get; private set; }

        public virtual ClientUser ClientUser { get; private set; } = null!;
        public virtual Post Post { get; private set; } = null!;

        public static ClientUserLikePost Create(int clientUserId, Guid postId)
        {
            var like = new ClientUserLikePost();
            like.ClientUserId = clientUserId;
            like.PostId = postId;
            return like;
        }
    }
}
