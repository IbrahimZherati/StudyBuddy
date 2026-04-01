using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.GroupChatDTO
{
    public class GroupChatBaseDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int MajorId { get; set; }

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public string Bio { get; set; } = null!;

        public byte[]? Photo { get; set; }
    }
}
