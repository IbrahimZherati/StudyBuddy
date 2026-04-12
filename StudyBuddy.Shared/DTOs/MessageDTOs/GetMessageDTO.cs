namespace StudyBuddy.Shared.DTOs.MessageDTO;

public class GetMessageDTO : MessageBaseDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;

    public int FromClientUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
}
