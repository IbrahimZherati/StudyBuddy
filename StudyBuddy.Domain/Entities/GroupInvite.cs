using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class GroupInvite : EntityBase<int>
    {
        public int ClientUserFromId { get; private set; }
        public ClientUser ClientUserFrom { get; private set; } = null!;
        public int ClientUserToId { get; private set; }
        public ClientUser ClientUserTo { get; private set; } = null!;
        public bool IsAccept { get; private set; }

        public int GroupChatId { get; private set; }
        public GroupChat GroupChat { get; private set; } = null!;

        public static Result<GroupInvite> Create(int fromId, int toId, int groupId)
        {
            var newInvite = new GroupInvite();
            newInvite.ClientUserFromId = fromId;
            newInvite.ClientUserToId = toId;
            newInvite.GroupChatId = groupId;
            return Result<GroupInvite>.Success(newInvite);
        }

    }
}
