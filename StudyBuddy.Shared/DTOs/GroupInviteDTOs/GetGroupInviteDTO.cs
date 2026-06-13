using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.GroupInviteDTOs
{
    public class GetGroupInviteDTO
    {
        public int Id { get; set; }
        public string? From { get; set; }
        public byte[]? FromClientPhoto { get; set; }

    }
}
