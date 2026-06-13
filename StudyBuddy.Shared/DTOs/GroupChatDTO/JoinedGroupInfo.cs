using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.GroupChatDTO
{
    public class JoinedGroupInfo : InfoGroupChatDTO
    {
        public int UnReadCount { get; set; }
        public GetGroupMessageDTO? LastMessage { get; set; }
    }
}
