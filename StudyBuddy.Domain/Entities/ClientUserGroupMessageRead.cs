using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class ClientUserGroupMessageRead : EntityBase<Guid>
    {
        public int ClientUserId { get; private set; }
        public Guid MessageId { get; private set; }

        public GroupMessage Message { get; private set; } = null!;
        public ClientUser ClientUser { get; private set; } = null!;

        public static ClientUserGroupMessageRead Create(int clientId , Guid MessageId)
        {
            var newRead = new ClientUserGroupMessageRead();
            newRead.ClientUserId = clientId;
            newRead.MessageId = MessageId;

            return newRead;
        }
        
    }
}
