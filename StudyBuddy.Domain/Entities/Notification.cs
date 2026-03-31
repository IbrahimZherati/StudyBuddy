namespace StudyBuddy.Domain.Entities;

public partial class Notification
{
    public int Id { get; set; }

    public int ToClientUserId { get; set; }

    public int FromClientUserId { get; set; }

    public string Discription { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int NotificationTypeId { get; set; }

    public virtual ClientUser FromClientUser { get; set; } = null!;

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual ClientUser ToClientUser { get; set; } = null!;
}
