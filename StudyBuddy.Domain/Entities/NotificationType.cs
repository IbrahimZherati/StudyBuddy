namespace StudyBuddy.Domain.Entities;

public partial class NotificationType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
