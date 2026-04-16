using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.PostReplayDTO;

public class PostReplayBaseDTO
{

        [Required]
        public Guid PostId { get; private set; }
        public string? Text { get; private set; }
}
