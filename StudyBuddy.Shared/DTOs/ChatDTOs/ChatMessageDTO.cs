using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ChatDTOs
{
    public class ChatMessageDTO
    {
        public string? Text { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
