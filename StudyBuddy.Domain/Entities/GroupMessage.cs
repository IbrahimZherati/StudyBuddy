namespace StudyBuddy.Domain.Entities;

public partial class GroupMessage
{
    public int Id { get; set; }

    public int GroupChatId { get; set; }

    public int FromClientUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ModifyDate { get; set; }

    public virtual ClientUser FromClientUser { get; set; } = null!;

    public virtual GroupChat GroupChat { get; set; } = null!;
}
