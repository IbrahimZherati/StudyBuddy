namespace StudyBuddy.Domain.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int ToClientUserId { get; set; }

    public int FromClientUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ClientUser FromClientUser { get; set; } = null!;

    public virtual ClientUser ToClientUser { get; set; } = null!;
}
