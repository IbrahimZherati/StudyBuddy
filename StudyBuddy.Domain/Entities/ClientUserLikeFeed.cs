using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class ClientUserLikeFeed : EntityBase<Guid>
    {
        public int ClientUserId { get; private set; }
        public int FeedId { get; private set; }

        public virtual Feed Feed { get; private set; } = null!;
        public virtual ClientUser ClientUser { get; private set; } = null!;

        private ClientUserLikeFeed() { }

        public static ClientUserLikeFeed Create(int clientUserId , int feedId)
        {
            var newLike = new ClientUserLikeFeed();
            newLike.ClientUserId = clientUserId;
            newLike.FeedId = feedId;
            return newLike;
        }

    }
}
