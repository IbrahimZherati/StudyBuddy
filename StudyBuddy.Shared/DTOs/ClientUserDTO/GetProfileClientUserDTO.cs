using StudyBuddy.Shared.DTOs.GroupChatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ClientUserDTO
{
    public class GetProfileClientUserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string? Major { get; set; }

        public string? University { get; set; }

        public string? Bio { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public bool Gender { get; set; } = true;

        public byte[]? Photo { get; set; }

        //Not in ClientUser
        public List<string> StudyInterests { get; set; } = new();
        public List<string> AvaiableDays { get; set; } = new();
        public int FriendCount { get; set; } 
        public int PostCount { get; set; }
        public List<InfoGroupChatDTO> FavoriteGroups { get; set; } = new();
        public List<InfoClientUserDTO> BestBuddies { get; set; } = new();
    }
}
