using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.GroupMessageDTO
{
    public class GroupMessageBaseDTO
    {
        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public int GroupChatId { get; set; }

        [Required]
        public int FromClientUserId { get; set; }
    }
}
