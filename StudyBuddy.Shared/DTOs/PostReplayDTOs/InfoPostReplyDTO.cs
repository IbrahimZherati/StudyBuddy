using StudyBuddy.Shared.DTOs.PostReplyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.PostReplayDTOs
{
    public class InfoPostReplyDTO : GetPostReplyDTO
    {
        public bool IsLiked { get; set; }
    }
}
