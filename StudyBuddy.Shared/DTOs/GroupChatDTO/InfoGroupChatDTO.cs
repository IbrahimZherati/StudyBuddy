using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.GroupChatDTO
{
    public class InfoGroupChatDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Major { get; set; } = null!;

        [Required]
        public string University { get; set; } = null!;

        [Required]
        public string Bio { get; set; } = null!;

        [Required]
        public int MemberCount { get; set; }

        public byte[]? Photo { get; set; }
    }
}
