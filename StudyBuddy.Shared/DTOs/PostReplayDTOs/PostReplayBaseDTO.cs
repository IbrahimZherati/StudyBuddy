using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.PostReplyDTO;

public class PostReplyBaseDTO
{

        [Required]
        public Guid PostId { get; private set; }
        public string? Text { get; private set; }
}
