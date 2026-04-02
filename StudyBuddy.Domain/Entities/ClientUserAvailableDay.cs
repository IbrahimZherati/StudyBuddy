using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class ClientUserAvailableDay
    {
        public int Id { get; set; }
        public int ClientUserId { get; set; }
        public int DayId { get; set; }

        public virtual Day Day { get; set; } = null!;
        public virtual ClientUser ClientUser { get; set; } = null!;
    }
}
