using StudyBuddy.Shared.DTOs.MessageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ChatDTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public byte[]? Photo { get; set; }
        public int UnReadMessages { get; set; }

        public ChatMessageDTO? LastMessage { get; set; } 
    }
}
