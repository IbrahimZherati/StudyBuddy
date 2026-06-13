using StudyBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain
{
    public class NoteTopic : EntityBase<int>
    {
        public int TopicId { get; private set; }

        public int NoteId { get; private set; }

        public Topic Topic { get; private set; } = null!;
        public Note Note { get; private set; } = null!;
    }
}
