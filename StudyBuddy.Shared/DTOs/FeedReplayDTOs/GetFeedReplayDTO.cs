using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Shared.DTOs.FeedReplayDTO;

public class GetFeedReplayDTO : FeedReplayBaseDTO
{
    public int Id { get; set; }
    [Required]
    public int FeedId { get; set; }
}
