namespace StudyBuddy.Shared.DTOs.PostReplyDTO;

public class GetPostReplyDTO : PostReplyBaseDTO
{
    public Guid Id { get; set; }

    public string? ClientUserName { get; set; }
    public byte[]? ClientPhoto { get; set; }

    
}
