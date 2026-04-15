using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class PostReplay : EntityBase<Guid>
    {
        public int PostId { get; private set; }
        public int ClientUserId { get; private set; }

        public string? Text { get; private set; }


        public virtual Post Post { get; private set; } = null!;
        public virtual ClientUser ClientUser { get; private set; } = null!;
    }
}
