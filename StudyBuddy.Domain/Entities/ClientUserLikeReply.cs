using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class ClientUserLikeReply : EntityBase<Guid>
    {
        public int ClientUserId { get; private set; }
        public Guid PostReplyId { get; private set; }

        public virtual ClientUser ClientUser { get;private set; } = null!;
        public virtual PostReply PostReply { get; private set; } = null!;

        public static ClientUserLikeReply Create(int clientId, Guid postReplyId)
        {
            var like = new ClientUserLikeReply { ClientUserId = clientId, PostReplyId = postReplyId };
            return like;
        }
    }
}
