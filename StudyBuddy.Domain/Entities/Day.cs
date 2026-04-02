using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ClientUserAvailableDay> ClientUserAvailableDays { get; set; } = new List<ClientUserAvailableDay>();
    }
}
