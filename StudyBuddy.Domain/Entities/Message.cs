namespace StudyBuddy.Domain.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int ToClientUserId { get; set; }

    public int FromClientUserid { get; set; }

    public DateOnly CreateDate { get; set; }

    public DateOnly? ModifyDate { get; set; }

    public virtual ClientUser FromClientUser { get; set; } = null!;

    public virtual ClientUser ToClientUser { get; set; } = null!;
}
