using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.MessageDTO
{
    public class MessageBaseDTO
    {
        [Required]
        public string? Text { get; set; }

        [Required]
        public int ToClientUserId { get; set; }

        [Required]
        public int FromClientUserId { get; set; }

        [Required]
        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
