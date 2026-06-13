using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.PostReplyDTO;

public class PostReplyBaseDTO
{

        [Required]
        public Guid PostId { get;  set; }
        public string? Text { get;  set; }
}
